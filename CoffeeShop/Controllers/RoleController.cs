using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.Models;
using CoffeeShop.DAL;
using CoffeeShop.DAL.Security;

namespace CoffeeShop.Controllers
{
    public class RoleController : Controller
    {

        DataContext Context = new DataContext();

        //
        // GET: /Role/
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            IEnumerable<Role> roles = Context.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Role newRole = new Role();
                newRole.RoleName = model.RoleName;
                newRole.Description = model.Description;
                Context.Roles.Add(newRole);
                Context.SaveChanges();

                return RedirectToAction("Index", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Thông tin vai trò không hợp lệ");
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Delete(int id)
        {
            var role = Context.Roles.Where(r => r.RoleId == id).FirstOrDefault();
            if (role != null)
            {
                // first, remove all user's permission belong to this role
                if (role.Users.Count > 0)
                {
                    var users = role.Users.ToList();
                    foreach (User item in users)
                    {
                        role.Users.Remove(item);
                    }
                }

                // remove role
                Context.Roles.Remove(role);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound(String.Format("Vai trò #{0} không tồn tại!", id));
            }
        }

        [HttpGet]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(int id = 0)
        {
            var role = Context.Roles.Where(r => r.RoleId == id).FirstOrDefault();
            if (role != null)
            {
                RoleViewModel model = new RoleViewModel();
                model.RoleId = role.RoleId;
                model.RoleName = role.RoleName;
                model.Description = role.Description;
                return View(model);
            }
            else
            {
                return HttpNotFound(String.Format("Vai trò #{0} không tồn tại", id));
            }
        }


        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = Context.Roles.Where(r => r.RoleId == model.RoleId).FirstOrDefault();
                if (role != null)
                {
                    role.RoleName = model.RoleName;
                    role.Description = model.Description;
                    Context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound(String.Format("Vai trò [{0}] không tồn tại", model.RoleName));
                }
            }
            else
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ");
                return View();
            }
        }
    }
}
