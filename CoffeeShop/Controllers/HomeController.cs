using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Price()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}
