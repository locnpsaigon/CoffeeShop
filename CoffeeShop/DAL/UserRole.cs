﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DAL
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
    }
}