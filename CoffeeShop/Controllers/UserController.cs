using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoffeeShop.Models;
using CoffeeShop.DAL;
using CoffeeShop.DAL.Security;
using Newtonsoft.Json;

namespace CoffeeShop.Controllers
{
    public class UserController : BaseController
    {

        DataContext Context = new DataContext();

        //
        // GET: /User/
        [Authorize]
        [CoffeeShopAuthorize(Roles="Administrators")]
        public ActionResult Index()
        {
            var users = Context.Users.ToList();
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new UserLoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLoginViewModel model)
        {
            // Get user info
            var user = Context.Users
                .Where(u => u.Username == model.Username)
                .FirstOrDefault();

           

            // Verify password
            if (user != null && SaltedHash.Verify(user.Salt, user.Password, model.Password))
            {
                var roles = user.Roles.Select(r => r.RoleName).ToArray();
                CoffeeShopPrincipalSerializeModel serializeModel = new CoffeeShopPrincipalSerializeModel();
                serializeModel.UserId = user.UserId;
                serializeModel.FirstName = user.FirstName;
                serializeModel.LastName = user.LastName;
                serializeModel.Roles = roles;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    user.Username,
                    DateTime.Now,
                    DateTime.Now.AddHours(24),
                    model.RememberMe,
                    userData);

                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index", "Finance");
            }
            else
            {
                ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu!");
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();

            // create selected role model
            model.RoleSelectViewModels = new List<RoleSelectViewModel>();
            var roles = Context.Roles.ToList();
            foreach (Role role in roles)
            {
                RoleSelectViewModel childModel = new RoleSelectViewModel();
                childModel.IsSeleced = false;
                childModel.RoleId = role.RoleId;
                childModel.RoleName = role.RoleName;
                model.RoleSelectViewModels.Add(childModel);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // check user exists
                var newUser = Context.Users.Where(u => u.Username.CompareTo(model.Username) == 0).FirstOrDefault();
                var userExisted = (newUser != null) ? true : false;

                if (!userExisted)
                {
                    // add user info
                    newUser = new User();
                    newUser.Username = model.Username;
                    SaltedHash sh = new SaltedHash(model.Username);
                    newUser.Password = sh.Hash;
                    newUser.Salt = sh.Salt;
                    newUser.FirstName = model.FirstName;
                    newUser.LastName = model.LastName;
                    newUser.IsActive = model.IsActive;
                    newUser.Email = model.Email;
                    newUser.CreateDate = DateTime.Now;
                    newUser.Roles = new List<Role>();
                    Context.Users.Add(newUser);

                    // add user's roles
                    foreach (RoleSelectViewModel item in model.RoleSelectViewModels)
                    {
                        if (item.IsSeleced)
                        {
                            var role = Context.Roles.Where(r => r.RoleId == item.RoleId).FirstOrDefault();
                            if (role != null)
                            {
                                newUser.Roles.Add(role);
                            }
                        }
                    }

                    // save changes
                    Context.SaveChanges();

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    // user exists
                    ModelState.AddModelError("",
                        String.Format("Tài khoản {0} đã tồn tại trong hệ thống", model.Username));
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin tài khoản không hợp lệ!");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(int id)
        {
            var user = Context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null)
                return HttpNotFound(String.Format("Tài khoản #{0} không tồn tại", id));

            UserViewModel model = new UserViewModel();
            model.UserId = user.UserId;
            model.Username = user.Username;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.IsActive = user.IsActive;
            model.Email = user.Email;

            // add select role model
            model.RoleSelectViewModels = new List<RoleSelectViewModel>();
            var roles = Context.Roles.ToArray();
            foreach (Role item in roles)
            {
                RoleSelectViewModel childModel = new RoleSelectViewModel();
                childModel.RoleId = item.RoleId;
                childModel.RoleName = item.RoleName;
                childModel.IsSeleced = user.Roles.Contains(item);
                model.RoleSelectViewModels.Add(childModel);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Context.Users.Where(u => u.UserId == model.UserId).FirstOrDefault();
                if (user == null)
                    return HttpNotFound(String.Format("Tài khoản {0} không tồn tại", model.Username));

                // update user info
                SaltedHash sh = new SaltedHash(model.Password);
                user.Password = sh.Hash;
                user.Salt = sh.Salt;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.IsActive = model.IsActive;

                // remove current user's roles
                if (user.Roles.Count > 0)
                {
                    var userRoles = user.Roles.ToList();
                    foreach (Role role in userRoles)
                    {
                        user.Roles.Remove(role);
                    }
                }
                
                // update user's roles
                foreach (RoleSelectViewModel item in model.RoleSelectViewModels)
                {
                    if (item.IsSeleced)
                    {
                        var role = Context.Roles.Where(r => r.RoleId == item.RoleId).FirstOrDefault();
                        if (role != null)
                        {
                            user.Roles.Add(role);
                        }
                    }
                }

                Context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Thông tin cập nhật tài khoản không hợp lệ");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Delete(int id)
        {
            var user = Context.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                // first, remove all user's roles
                if (user.Roles.Count > 0)
                {
                    var roles = user.Roles.ToList();
                    foreach (Role item in roles)
                    {
                        user.Roles.Remove(item);
                    }
                }
                
                // remove user
                Context.Users.Remove(user);
                Context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound(String.Format("Tài khoản #{0} không tồn tại", id));
            }
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Details()
        {
            // Get current user info
            var currentUser = Context.Users.Where(u => u.UserId == User.UserId).FirstOrDefault();
            if (currentUser == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
           
            // Create view model
            UserProfileViewModel model = new UserProfileViewModel();
            model.UserId = currentUser.UserId;
            model.Username = currentUser.Username;
            model.FullName = currentUser.FirstName + " " + currentUser.LastName;
            model.Email = currentUser.Email;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // get user info
                var user = Context.Users.Where(u => u.UserId == model.UserId).FirstOrDefault();
                if (user == null)
                    return HttpNotFound(String.Format("Tài khoản #{0} không tồn tại", model.UserId));

                // verify current password
                if (!SaltedHash.Verify(user.Salt, user.Password, model.CurrentPassword))
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không hợp lệ");
                    return View(model);
                }
                
                // update user password
                SaltedHash sh = new SaltedHash(model.Password);
                user.Password = sh.Hash;
                user.Salt = sh.Salt;
                Context.SaveChanges();

                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "User");
            }
            else
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ");
                return View(model);
            }
        }
    }
}
