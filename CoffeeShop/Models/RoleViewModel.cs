using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage="Tên vai trò không được rỗng")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string RoleName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string Description { get; set; }
    }
}