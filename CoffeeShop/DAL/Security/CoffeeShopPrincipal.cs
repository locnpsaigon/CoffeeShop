using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace CoffeeShop.DAL.Security
{
    public class CoffeeShopPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }


        public CoffeeShopPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public Boolean IsInRole(string roleName)
        {
            if (Roles.Any(r => Roles.Contains(roleName)))
            {
                return true;
            }
            return false;
        }

        public int UserId { get; set;  }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
    }
}