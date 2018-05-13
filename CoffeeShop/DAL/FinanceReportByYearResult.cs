using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.DAL
{
    public class FinanceReportByYearResult
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public Decimal PAmount { get; set; }
        public Decimal RAmount { get; set; }
    }
}