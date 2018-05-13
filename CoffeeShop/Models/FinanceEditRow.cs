using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class FinanceEditRow
    {
        [Required(ErrorMessage="Diễn giải không được rỗng")]
        public string Description { get; set; }

        [Required(ErrorMessage="Số tiền không được rỗng")]
        [DataType(DataType.Currency, ErrorMessage = "Vui lòng nhập số")]
        public Decimal Amount { get; set; }
    }
}