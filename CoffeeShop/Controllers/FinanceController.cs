using System;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.DAL;
using CoffeeShop.DAL.Security;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class FinanceController : Controller
    {
        DataContext Context = new DataContext();

        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            var date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var date1 = date2.AddDays(-7);

            FinanceReportViewModel model = new FinanceReportViewModel();
            model.FromDate = date1.ToString("d/M/yyyy");
            model.ToDate = date2.ToString("d/M/yyyy");
            model.ReportData = Context.Database.SqlQuery<FinanceReportByDateResult>(
                "EXEC dbo.usp_getFinanceReportByDate @FromDate, @ToDate",
                new SqlParameter { ParameterName = "FromDate", Value = date1 },
                new SqlParameter { ParameterName = "ToDate", Value = date2 })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Index(FinanceReportViewModel model)
        {
            try
            {
                // get date range
                DateTime date1 = DateTime.ParseExact(model.FromDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime date2 = DateTime.ParseExact(model.ToDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                // get report data
                model.ReportData = Context.Database.SqlQuery<FinanceReportByDateResult>(
                    "EXEC dbo.usp_getFinanceReportByDate @FromDate, @ToDate", new 
                    SqlParameter { ParameterName = "FromDate", Value = date1 }, new 
                    SqlParameter { ParameterName = "ToDate", Value = date2 })
                    .ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }
            return View(model);
        }

        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Add()
        {
            FinanceAddViewModel model = new FinanceAddViewModel();
            model.Payments.Add(new FinanceEditRow());
            model.Receipts.Add(new FinanceEditRow());
            return View(model);
        }


        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Add(FinanceAddViewModel model)
        {
            try
            {
                // parse date
                DateTime dt = DateTime.ParseExact(model.Date, "d/M/yyyy", CultureInfo.InvariantCulture);

                // add payments
                foreach (FinanceEditRow item in model.Payments)
                {
                    Payment pm = new Payment();
                    pm.Date = dt;
                    pm.Description = item.Description;
                    pm.Amount = item.Amount;
                    Context.Payments.Add(pm);
                }

                // add receipts
                foreach (FinanceEditRow item in model.Receipts)
                {
                    Receipt rc = new Receipt();
                    rc.Date = dt;
                    rc.Description = item.Description;
                    rc.Amount = item.Amount;
                    Context.Receipts.Add(rc);
                }

                Context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }
            return View(model);
        }

        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);

            // Initialize model
            FinanceAddViewModel model = new FinanceAddViewModel();
            model.Date = dt.ToString("dd/MM/yyyy");

            // Initialize payment models
            var payments = Context.Payments.Where(p => p.Date == dt).ToList<Payment>();
            if (payments != null && payments.Count > 0)
            {
                foreach (Payment item in payments)
                {
                    FinanceEditRow editRow = new FinanceEditRow();
                    editRow.Description = item.Description;
                    editRow.Amount = item.Amount;
                    model.Payments.Add(editRow);
                }
            }
            else
            {
                model.Payments.Add(new FinanceEditRow());
            }

            // Initialize receipts models
            var receipts = Context.Receipts.Where(r => r.Date == dt).ToList();
            if (receipts != null && receipts.Count > 0)
            {
                foreach (Receipt item in receipts)
                {
                    FinanceEditRow editRow = new FinanceEditRow();
                    editRow.Description = item.Description;
                    editRow.Amount = item.Amount;
                    model.Receipts.Add(editRow);
                }
            }
            else
            {
                model.Receipts.Add(new FinanceEditRow());
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Edit(FinanceAddViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(model.Date, "d/M/yyyy", CultureInfo.InvariantCulture);

                    // Remove old payments & receipts
                    var currentPayments = Context.Payments.Where(p => p.Date == dt).ToList<Payment>();
                    if (currentPayments != null && currentPayments.Count > 0)
                    {
                        foreach (Payment item in currentPayments)
                        {
                            Context.Payments.Remove(item);
                        }
                    }

                    var currentReceipts = Context.Receipts.Where(r => r.Date == dt).ToList<Receipt>();
                    if (currentReceipts != null && currentReceipts.Count > 0)
                    {
                        foreach (Receipt item in currentReceipts)
                        {
                            Context.Receipts.Remove(item);
                        }
                    }

                    // Add updated payments & receipts
                    foreach (FinanceEditRow item in model.Payments)
                    {
                        Payment pm = new Payment();
                        pm.Date = dt;
                        pm.Description = item.Description;
                        pm.Amount = item.Amount;
                        Context.Payments.Add(pm);
                    }

                    foreach (FinanceEditRow item in model.Receipts)
                    {
                        Receipt rc = new Receipt();
                        rc.Date = dt;
                        rc.Description = item.Description;
                        rc.Amount = item.Amount;
                        Context.Receipts.Add(rc);
                    }

                    // Save changes
                    Context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            }

            return View(model);
        }

        [Authorize]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        public ActionResult Delete(int year, int month, int day)
        {
            DateTime dt = new DateTime(year, month, day);

            // Remove old payments & receipts
            var currentPayments = Context.Payments.Where(p => p.Date == dt).ToList<Payment>();
            if (currentPayments != null && currentPayments.Count > 0)
            {
                foreach (Payment item in currentPayments)
                {
                    Context.Payments.Remove(item);
                }
            }

            var currentReceipts = Context.Receipts.Where(r => r.Date == dt).ToList<Receipt>();
            if (currentReceipts != null && currentReceipts.Count > 0)
            {
                foreach (Receipt item in currentReceipts)
                {
                    Context.Receipts.Remove(item);
                }
            }

            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
