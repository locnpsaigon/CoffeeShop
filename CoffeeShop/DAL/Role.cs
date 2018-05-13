using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DAL
{
    public class Role
    {
        public int RoleId { get; set; }
        
        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}