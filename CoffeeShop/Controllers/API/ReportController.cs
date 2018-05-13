using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoffeeShop.DAL;
using CoffeeShop.Models;
using CoffeeShop.DAL.Security;

namespace CoffeeShop.Controllers.API
{
    public class ReportController : ApiController
    {
        DataContext Context = new DataContext();


        [HttpPost]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        [ActionName("FinanceReportByDate")]
        public List<FinanceReportByDateResult> FinanceReportByDate([FromBody]DateRangeParam value)
        {
            var result = new List<FinanceReportByDateResult>();
            try
            {
                DateTime dateFrom = DateTime.ParseExact(value.DateFrom, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime dateTo = DateTime.ParseExact(value.DateTo, "d/M/yyyy", CultureInfo.InvariantCulture);

                result = Context.Database.SqlQuery<FinanceReportByDateResult>(
                    "EXEC dbo.usp_getFinanceReportByDate @FromDate, @ToDate",
                    new SqlParameter { ParameterName = "FromDate", Value = dateFrom },
                    new SqlParameter { ParameterName = "ToDate", Value = dateTo })
                    .OrderBy(r => r.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        [HttpPost]
        [CoffeeShopAuthorize(Roles = "Administrators")]
        [ActionName("FinanceReportByYear")]
        public List<FinanceReportByYearResult> FinanceReportByYear([FromBody]YearParam value)
        {
            var result = new List<FinanceReportByYearResult>();
            try
            {
                result = Context.Database.SqlQuery<FinanceReportByYearResult>(
                    "EXEC dbo.usp_getFinanceReportByYear @Year",
                    new SqlParameter { ParameterName = "Year", Value = value.Year })
                    .OrderBy(r => r.Month)
                    .ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
    }

    public class DateRangeParam
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }

    public class YearParam
    {
        public string Year { get; set; }
    }
}
