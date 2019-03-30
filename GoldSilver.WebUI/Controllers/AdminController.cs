using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;
using GoldSilver.WebUI.Models;

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
            var jewelries = repository.Jewelries.Include(j => j.Categories).Include(j => j.Gemstones).Include(j => j.Materials);
            return View(jewelries);
        }

        public ViewResult Categories()
        {
            return View(repository.Categories);
        }

        public ViewResult Edit(int id)
        {
            JewelryViewModel jewelry = new JewelryViewModel();
            jewelry.Jewelry = repository.Jewelries
                .Include("Images")
                .Include(j => j.Categories)
                .Include(j => j.Gemstones)
                .Include(j => j.Materials)
                .FirstOrDefault(j => j.JewelryId == id);

            jewelry = createListItems(jewelry);

            return View(jewelry);
        }

        [HttpPost]
        public ActionResult Edit(Jewelry jewelry, 
            IEnumerable<HttpPostedFileBase> files,
            IEnumerable<int> selectedJewelryCategories,
            IEnumerable<int> selectedJewelryMaterials,
            IEnumerable<int> selectedJewelryGemstones)
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

                if (selectedJewelryCategories != null && selectedJewelryCategories.Count() > 0)
                {
                    jewelry.Categories = repository.Categories.Where(c => selectedJewelryCategories.Contains(c.CategoryId)).ToList();
                }

                if (selectedJewelryMaterials != null && selectedJewelryMaterials.Count() > 0)
                {
                    jewelry.Materials = repository.Materials.Where(c => selectedJewelryMaterials.Contains(c.MaterialId)).ToList();
                }

                if (selectedJewelryGemstones != null && selectedJewelryGemstones.Count() > 0)
                {
                    jewelry.Gemstones = repository.Gemstones.Where(c => selectedJewelryGemstones.Contains(c.GemstoneId)).ToList();
                }

                repository.SaveJewelry(jewelry);
                TempData["message"] = string.Format("{0} has been saved", jewelry.Name);

                ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "CategoryName");
                ViewBag.MaterialId = new SelectList(repository.Materials, "MaterialId", "MaterialName");
                ViewBag.GemstoneId = new SelectList(repository.Gemstones, "GemstoneId", "GemstoneName");

                return RedirectToAction("Index");
            }
            // there is something wrong with the data values
            else
            {
                JewelryViewModel jewelryViewModel = new JewelryViewModel()
                {
                    Jewelry = jewelry,
                    SelectedJewelryCategories = selectedJewelryCategories.ToList(),
                    SelectedJewelryGemstones = selectedJewelryGemstones.ToList(),
                    SelectedJewelryMaterials = selectedJewelryMaterials.ToList()
                };

                jewelryViewModel = createListItems(jewelryViewModel);

                return View(jewelry);
            }
        }

        public ViewResult Create()
        {
            var jewelryViewModel = new JewelryViewModel();
            jewelryViewModel = createListItems(jewelryViewModel);
            return View("Edit", jewelryViewModel);
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

        [HttpGet]
        public JsonResult GenerateNewSet()
        {
            return Json(
                repository.Jewelries.Select(j => j.Set)
                    .AsEnumerable()
                    .Select(str =>
                    {
                        Int32.TryParse(str, out var res);
                        return res;
                    })
                    .Max()
                + 1, JsonRequestBehavior.AllowGet);
        }

        private JewelryViewModel createListItems(JewelryViewModel jewelryViewModel)
        {
            if(jewelryViewModel == null)
            {
                throw new ArgumentNullException();
            }

            jewelryViewModel.AllJewelryCategories = repository.Categories
                .ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                });

            jewelryViewModel.AllJewelryGemstones = repository.Gemstones
                .ToList()
                .Select(g => new SelectListItem
                {
                    Text = g.GemstoneName,
                    Value = g.GemstoneId.ToString()
                });

            jewelryViewModel.AllJewelryMaterials = repository.Materials
                .ToList()
                .Select(m => new SelectListItem
                {
                    Text = m.MaterialName,
                    Value = m.MaterialId.ToString()
                });

            return jewelryViewModel;
        }
    }
}
