using GoldSilver.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldSilver.WebUI.Models;
using GoldSilver.Domain.Entities;

namespace GoldSilver.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        IJewelryRepository repository;
        public int pageSize = 12;

        public NavController(IJewelryRepository jewelRepository)
        {
            this.repository = jewelRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            Menu menu = new Menu();

            menu.CurrentItem = category;

            menu.Categories = repository.Categories.OrderBy(j => j.Order).ToList();
            menu.Materials = repository.Materials.OrderBy(j => j.Order).ToList();
            menu.Gemstones = repository.Gemstones.OrderBy(j => j.Order).ToList();

            return PartialView(menu);
        }

    }
}
