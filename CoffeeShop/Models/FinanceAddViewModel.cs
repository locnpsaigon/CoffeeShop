using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class FinanceAddViewModel
    {
        [Required(ErrorMessage="Ngày nhập liệu không được rỗng")]
        public string Date { get; set; }
        public IList<FinanceEditRow> Receipts { get; set; }
        public IList<FinanceEditRow> Payments { get; set; }
        
        public FinanceAddViewModel()
        {
            Payments = new List<FinanceEditRow>();
            Receipts = new List<FinanceEditRow>();
        }
    }
}