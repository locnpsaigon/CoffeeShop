using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.DAL.Security;

namespace CoffeeShop.Controllers
{
    public class ReportController : Controller
    {
        
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult FinanceByDate()
        {
            return View();
        }

        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult FinanceByYear()
        {
            List<int> model = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 3; i++)
            {
                model.Add(currentYear - i);
            }
            return View(model);
        }


    }
}
