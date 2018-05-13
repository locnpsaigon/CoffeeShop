using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.DAL
{
    public class FinanceReportByDateResult
    {
        public DateTime Date { get; set; }
        public Decimal PAmount { get; set; }
        public Decimal RAmount { get; set; }
    }
}