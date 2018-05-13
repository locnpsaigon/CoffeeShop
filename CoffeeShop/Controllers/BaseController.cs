using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.DAL.Security;

namespace CoffeeShop.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CoffeeShopPrincipal User
        {
            get { return HttpContext.User as CoffeeShopPrincipal; }
        }
    }
}
