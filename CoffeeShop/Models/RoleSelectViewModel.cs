using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class RoleSelectViewModel
    {
        [Required]
        public bool IsSeleced { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}