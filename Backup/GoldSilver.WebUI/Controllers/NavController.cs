using GoldSilver.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        IJewelryRepository repository;
        public int pageSize = 4;

        public NavController(IJewelryRepository jewelRepository)
        {
            this.repository = jewelRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Jewelries
                .Select(x => x.CategoryId)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }

    }
}
