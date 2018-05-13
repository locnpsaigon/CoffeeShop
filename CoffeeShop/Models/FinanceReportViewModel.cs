using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CoffeeShop.DAL;

namespace CoffeeShop.Models
{
    public class FinanceReportViewModel
    {
        [Required(ErrorMessage="Ngày bắt đầu không được rỗng")]
        public string FromDate { get; set; }

        [Required(ErrorMessage="Ngày kết thúc không được rỗng")]
        public string ToDate { get; set; }

        public IList<FinanceReportByDateResult> ReportData { get; set; }
    
    }
}