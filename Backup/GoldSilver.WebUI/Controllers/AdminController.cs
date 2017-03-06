using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private IJewelryRepository repository;
        public AdminController(IJewelryRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Jewelries);
        }

        public ViewResult Edit(int id)
        {
            Jewelry jewel = repository.Jewelries
            .FirstOrDefault(j => j.JewelryId == id);
            return View(jewel);
        }

        [HttpPost]
        public ActionResult Edit(Jewelry jewelry)
        {
            if (ModelState.IsValid)
            {
                repository.SaveJewelry(jewelry);
                TempData["message"] = string.Format("{0} has been saved", jewelry.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(jewelry);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Jewelry());
        }

        [HttpPost]
        public ActionResult Delete(int jewelryId)
        {
            Jewelry deletedJew = repository.DeleteJew(jewelryId);
            if (deletedJew != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedJew.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
