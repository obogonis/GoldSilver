using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;

namespace GoldSilver.WebUI.Controllers
{
    [Authorize]
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
            return View(repository.Jewelries.Include(j => j.Category).Include(j => j.Gemstone).Include(j => j.Material));
        }

        public ViewResult Categories()
        {
            return View(repository.Categories);
        }

        public ViewResult Edit(int id)
        {
            Jewelry jewel = repository.Jewelries
                .Include("Images")
                .FirstOrDefault(j => j.JewelryId == id);

            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "CategoryName", jewel.CategoryId);
            ViewBag.MaterialId = new SelectList(repository.Materials, "MaterialId", "MaterialName", jewel.MaterialId);
            ViewBag.GemstoneId = new SelectList(repository.Gemstones, "GemstoneId", "GemstoneName", jewel.GemstoneId);

            return View(jewel);
        }

        [HttpPost]
        public ActionResult Edit(Jewelry jewelry, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                if ((files != null) && (files.Count() > 0))
                {
                    foreach (var image in files)
                    {
                        if (image != null)
                        {
                            Image img = new Image();

                            if (repository.Images.Count() > 0)
                                img.Id = repository.Images.Max(i => i.Id) + 1;
                            else
                                img.Id = 1;

                            if (image != null)
                            {
                                img.ImageMimeType = image.ContentType;
                                img.ImageData = new byte[image.ContentLength];
                                image.InputStream.Read(img.ImageData, 0, image.ContentLength);
                            }

                            if (jewelry.Images == null)
                                jewelry.Images = new List<Image>();

                            jewelry.Images.Add(img);
                        }
                    }
                }

                repository.SaveJewelry(jewelry);
                TempData["message"] = string.Format("{0} has been saved", jewelry.Name);

                ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "CategoryName");
                ViewBag.MaterialId = new SelectList(repository.Materials, "MaterialId", "MaterialName");
                ViewBag.GemstoneId = new SelectList(repository.Gemstones, "GemstoneId", "GemstoneName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "CategoryName");
                ViewBag.MaterialId = new SelectList(repository.Materials, "MaterialId", "MaterialName");
                ViewBag.GemstoneId = new SelectList(repository.Gemstones, "GemstoneId", "GemstoneName");

                // there is something wrong with the data values
                return View(jewelry);
            }
        }

        public ViewResult Create()
        {
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "CategoryName");
            ViewBag.MaterialId = new SelectList(repository.Materials, "MaterialId", "MaterialName");
            ViewBag.GemstoneId = new SelectList(repository.Gemstones, "GemstoneId", "GemstoneName");

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

        [HttpPost]
        public ActionResult DeleteImg(Image img)
        {
            Image deletedImg = repository.DeleteImage(img.Id);
            var msg = String.Empty;
            if (deletedImg != null)
            {
                msg = string.Format("{0} was deleted", deletedImg.Id);
            }
            return RedirectToAction("Edit", new { id = deletedImg.JewelryId });
        }
    }
}
