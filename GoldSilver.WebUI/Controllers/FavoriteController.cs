using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private IJewelryRepository repository;

        public FavoriteController(IJewelryRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Favorite fav)
        {
            var favorites = repository.Jewelries
                    .Include("Images")
                    .Include("Material")
                    .Include("Category")
                    .AsEnumerable()
                    .Where(j => fav.Lines.Any(l => l.JewelryId == j.JewelryId));

            return View(favorites);
        }

        public PartialViewResult Summary(Favorite fav)
        {
            return PartialView(fav);
        }

        public RedirectResult AddToFavorites(Favorite fav, int JewelryId, string returnUrl)
        {

            Jewelry jew = repository.Jewelries
                .FirstOrDefault(p => p.JewelryId == JewelryId);

            if (jew != null)
            {
                fav.AddItem(jew);
            }

            return Redirect(returnUrl); //RedirectToAction("List", "Jewelries", new { returnUrl });
        }

        public RedirectResult RemoveFromFavorites(Favorite fav, int JewelryId, string returnUrl)
        {
            Jewelry jew = repository.Jewelries
                .FirstOrDefault(p => p.JewelryId == JewelryId);

            if (jew != null)
            {
                fav.RemoveLine(jew);
            }
            return Redirect(returnUrl);
        }

        //private Favorite GetFavorites()
        //{
        //    Favorite fav = (Favorite)Session["Favorite"];

        //    if (fav == null)
        //    {
        //        fav = new Favorite();
        //        Session["Favorite"] = fav;
        //    }

        //    return fav;
        //}
    }
}