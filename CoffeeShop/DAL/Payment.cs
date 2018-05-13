using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DAL
{
    public class Payment
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public Decimal Amount { get; set; }

        public string Description { get; set; }

    }
}