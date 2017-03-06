using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Binders
{
    public class FavoriteModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Favorite fav = (Favorite)controllerContext.HttpContext.Session[sessionKey];

            if (fav == null)
            {
                fav = new Favorite();
                controllerContext.HttpContext.Session[sessionKey] = fav;
            }

            return fav;
        }
    }
}