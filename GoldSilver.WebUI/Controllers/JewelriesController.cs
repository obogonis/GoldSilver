using GoldSilver.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldSilver.WebUI.Models;
using GoldSilver.Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Web.Helpers;
using GoldSilver.WebUI.Infrastructure;
using System.IO;
using System.Threading;

namespace GoldSilver.WebUI.Controllers
{

    public class JewelriesController : Controller
    {
        //
        // GET: /Jewelries/
        IJewelryRepository repository;
        public int pageSize = 12;

        public JewelriesController(IJewelryRepository jewelRepository)
        {
            this.repository = jewelRepository;
        }

        public ActionResult Details(int? id = null, string category = null, int page = 1, string sortBy = null, string sortDirection = null)
        {
            Func<Jewelry, Object> orderByFunc = null;

            if (sortBy == "Name")
                orderByFunc = item => item.Name;
            else if (sortBy == "Weight")
                orderByFunc = item => item.Weight;
            else if (sortBy == "Popularity")
                orderByFunc = item => -item.Popularity;
            else
                orderByFunc = item => item.JewelryId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Jewelry jew = repository
                .Jewelries
                .Include("Gemstone")
                .Include("Category")
                .Include("Images")
                .Where(j => j.JewelryId == id)
                .First();

            if (jew == null)
            {
                return HttpNotFound();
            }

            /*TODO: need to refactor*/
            try { jew.PrevJewelry = repository.Jewelries.Find(id - 1).JewelryId; }
            catch { }
            try { jew.NextJewelry = repository.Jewelries.Find(id + 1).JewelryId; }
            catch { }


            return View(jew);
        }

        public ViewResult List(string category = null, int page = 1, string sortBy = null, string sortDirection = null)
        {

            Func<Jewelry, Object> orderByFunc = null;

            if (sortBy == "Name")
                orderByFunc = item => item.Name;
            else if (sortBy == "Weight")
                orderByFunc = item => item.Weight;
            else if (sortBy == "Popularity")
                orderByFunc = item => -item.Popularity;
            else
                orderByFunc = item => item.JewelryId;



            JewelriesListViewModel model = new JewelriesListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    page = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                                    repository.Jewelries.Count() :
                                    repository.Jewelries.Where(e => (e.Category.UrlPath == category)
                                        || (e.Material.UrlPath == category)
                                        || (e.Gemstone.UrlPath == category)).Count()
                },

                CurrentCategory = category
            };

            if (((sortDirection != null) && (sortDirection.ToLower() == "asc"))
                || (sortDirection == null))
            {
                model.Jewelries = repository.Jewelries
                    .Include("Images")
                    .Include("Material")
                    .Include("Category")
                    .Where(j => category == null
                        || j.Category.UrlPath == category
                        || j.Material.UrlPath == category
                        || j.Gemstone.UrlPath == category)
                    .OrderBy(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
            else if (sortDirection.ToLower() == "desc")
            {
                model.Jewelries = repository.Jewelries
                    .Include("Images")
                    .Include("Material")
                    .Include("Category")
                    .Where(j => category == null
                        || j.Category.UrlPath == category
                        || j.Material.UrlPath == category
                        || j.Gemstone.UrlPath == category)
                    .OrderByDescending(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }

            return View(model);
        }

        public ActionResult GetPage(string category = null, int page = 2, string sortBy = null, string sortDirection = null)
        {

            Func<Jewelry, Object> orderByFunc = null;

            if (sortBy == "Name")
                orderByFunc = item => item.Name;
            else if (sortBy == "Weight")
                orderByFunc = item => item.Weight;
            else if (sortBy == "Popularity")
                orderByFunc = item => -item.Popularity;
            else
                orderByFunc = item => item.JewelryId;



            JewelriesListViewModel model = new JewelriesListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    page = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                                    repository.Jewelries.Count() :
                                    repository.Jewelries.Where(e => (e.Category.UrlPath == category)
                                        || (e.Material.UrlPath == category)
                                        || (e.Gemstone.UrlPath == category)).Count()
                },

                CurrentCategory = category
            };

            if (((sortDirection != null) && (sortDirection.ToLower() == "asc"))
                || (sortDirection == null))
            {
                model.Jewelries = repository.Jewelries
                    .Include("Material")
                    .Include("Category")
                    .Where(j => category == null
                        || j.Category.UrlPath == category
                        || j.Material.UrlPath == category
                        || j.Gemstone.UrlPath == category)
                    .OrderBy(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
            else if (sortDirection.ToLower() == "desc")
            {
                model.Jewelries = repository.Jewelries
                    .Include("Material")
                    .Include("Category")
                    .Where(j => category == null
                        || j.Category.UrlPath == category
                        || j.Material.UrlPath == category
                        || j.Gemstone.UrlPath == category)
                    .OrderByDescending(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }

            Thread.Sleep(3000);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetJewelryBySet(string set)
        {
            List<Jewelry> jews = repository
                .Jewelries
                .Include("Gemstone")
                .Include("Category")
                .Where(j => j.Set == set)
                .ToList();

            return Json(jews, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetImageTeaserById(int imageId)
        {
            Image image = repository.Images.Find(imageId);

            if ((image != null) && (image.ImageData != null))
            {
                WebImage resizedImg = new WebImage(image.ImageData)
                                                .Resize(200, 200)
                                                .Crop(1, 1);

                return File(resizedImg.GetBytes(), resizedImg.ImageFormat);
            }

            return null;
        }

        public FileContentResult GetImageById(int imageId)
        {
            Image image = repository.Images.Find(imageId);

            if ((image != null) && (image.ImageData != null))
            {
                return File(ImageHelper.AddWatermark(image.ImageData), image.ImageMimeType);
            }
            else
                return null;

        }


        public FileContentResult GetMainImageById(int jewelryId)
        {
            Jewelry jew = repository.Jewelries
    .Include("Images")
    .FirstOrDefault(j => j.JewelryId == jewelryId);

            if (jew != null)
            {
                if (jew.Images.Count > 0)
                {
                    Image img = jew.Images.OrderBy(i => i.Id).First();

                    if (img.ImageData != null)
                        return File(ImageHelper.AddWatermark(img.ImageData), img.ImageMimeType);
                }

            }

            return null;
        }

        public FileContentResult GetImage(int jewelryId, int imageOrder = 1)
        {
            Jewelry jew = repository.Jewelries
                .Include("Images")
                .FirstOrDefault(j => j.JewelryId == jewelryId);

            if (jew != null)
            {
                if (jew.Images.Count > 0)
                {
                    Image img = jew.Images.OrderBy(i => i.Id).First();
                    return File(img.ImageData, img.ImageMimeType);
                }
                else
                    return null;
            }
            else
                return null;
        }

        /* Get resized image for jew teazer 200x200 */
        public FileContentResult GetImageForTeaser(int jewelryId)
        {
            Jewelry jew = repository.Jewelries
                .Include("Images")
                .FirstOrDefault(j => j.JewelryId == jewelryId);

            if (jew != null)
            {
                if (jew.Images.Count > 0)
                {
                    Image img = jew.Images.OrderBy(i => i.Id).First();

                    if (img.ImageData != null)
                    {
                        WebImage resizedImg = new WebImage(img.ImageData)
                                                    .Resize(200, 200)
                                                    .Crop(1, 1);
                        return File(resizedImg.GetBytes(), resizedImg.ImageFormat);

                    }
                }
            }

            return null;
        }

    }
}
