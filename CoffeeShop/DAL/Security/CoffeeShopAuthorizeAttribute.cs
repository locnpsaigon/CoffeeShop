using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

namespace CoffeeShop.DAL.Security
{
    public class CoffeeShopAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CoffeeShopPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CoffeeShopPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentUser == null)
            {
                return;
            }

            if (!String.IsNullOrEmpty(Roles))
            {
                if (!CurrentUser.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));

                    // base.OnAuthorization(filterContext); //returns to login url
                }
            }

            if (!String.IsNullOrEmpty(Users))
            {
                if (!Users.Contains(CurrentUser.UserId.ToString()))
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));

                    // base.OnAuthorization(filterContext); //returns to login url
                }
            }
        }

    }
}