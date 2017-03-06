using GoldSilver.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldSilver.WebUI.Models;

namespace GoldSilver.WebUI.Controllers
{

    public class JewelriesController : Controller
    {
        //
        // GET: /Jewelries/
        IJewelryRepository repository;
        public int pageSize = 4;

        public JewelriesController(IJewelryRepository jewelRepository)
        {
            this.repository = jewelRepository;
        }

        public ViewResult List(string category ,int page = 1)
        {
            JewelriesListViewModel model = new JewelriesListViewModel()
            {
                Jewelries = repository.Jewelries
                .Where(j => category == null || j.CategoryId == category )
                .OrderBy(j => j.JewelryId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo()
                {
                    page = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                                    repository.Jewelries.Count() :
                                    repository.Jewelries.Where(e => e.CategoryId == category).Count()
                }
            };

            return View(model);
        }

    }
}
