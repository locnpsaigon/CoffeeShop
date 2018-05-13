using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.DAL.Security
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CoffeeShopPrincipal User
        {
            get { return base.User as CoffeeShopPrincipal; }
        }
    }
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CoffeeShopPrincipal User
        {
            get { return base.User as CoffeeShopPrincipal; }
        }
    }
}