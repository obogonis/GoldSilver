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
using GoldSilver.WebUI.Infrastructure.Abstract;

namespace GoldSilver.WebUI.Controllers
{

    public class JewelriesController : Controller
    {
        //
        // GET: /Jewelries/
        IJewelryRepository repository;
        public int pageSize = 12;
        private decimal exchangeRate;
        private IAppSettings appSettings;

        public JewelriesController(IJewelryRepository _jewelRepository, IAppSettings _appSettings)
        {
            this.repository = _jewelRepository;
            this.appSettings = _appSettings;
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
                .Include("Gemstones")
                .Include("Categories")
                .Include("Materials")
                .Include("Images")
                .Where(j => j.JewelryId == id)
                .First();

            if (jew.Price != null)
            {
                jew.PriceConverted = (decimal)jew.Price * appSettings.ExchangeRate;
            }

            if (jew == null)
            {
                return HttpNotFound();
            }

            /* TODO: need to refactor */
            jew.PrevJewelry = _getPrevId((int)id, jew.Categories);
            jew.NextJewelry = _getNextId((int)id, jew.Categories);

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

            ViewBag.Sort = sortBy;

            JewelriesListViewModel model = new JewelriesListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    page = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                                    repository.Jewelries.Count(j => j.InStock) :
                                    repository.Jewelries.Where(e => (e.Categories.Any(c => c.UrlPath == category ) && e.InStock)
                                        || (e.Materials.Any(m => m.UrlPath == category))
                                        || (e.Gemstones.Any(g => g.UrlPath == category)))
                                    .Count()
                },

                CurrentCategory = category
            };

            if (((sortDirection != null) && (sortDirection.ToLower() == "asc"))
                || (sortDirection == null))
            {
                model.Jewelries = repository.Jewelries
                    .Include("Images")
                    .Include("Materials")
                    .Include("Categories")
                    .Include("Gemstones")
                    .Where(j => j.InStock &&
                        (category == null
                        || j.Categories.Any(c => c.UrlPath == category)
                        || j.Materials.Any(c => c.UrlPath == category)
                        || j.Gemstones.Any(c => c.UrlPath == category)))
                    .OrderBy(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }
            else if (sortDirection.ToLower() == "desc")
            {
                model.Jewelries = repository.Jewelries
                    .Include("Images")
                    .Include("Materials")
                    .Include("Categories")
                    .Include("Gemstones")
                    .Where(j => j.InStock &&
                        (category == null
                        || j.Categories.Any(c => c.UrlPath == category)
                        || j.Materials.Any(c => c.UrlPath == category)
                        || j.Gemstones.Any(c => c.UrlPath == category)))
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
                                    repository.Jewelries.Count(j => j.InStock) :
                                    repository.Jewelries.Where(e => (e.Categories.Any(c => c.UrlPath == category))
                                        || (e.Materials.Any(m => m.UrlPath == category))
                                        || (e.Gemstones.Any(g => g.UrlPath == category)))
                                    .Count()
                },

                CurrentCategory = category
            };

            if (((sortDirection != null) && (sortDirection.ToLower() == "asc"))
                || (sortDirection == null))
            {
                model.JewelriesJson = repository.Jewelries
                    .Include("Materials")
                    .Include("Categories")
                    .Include("Gemstones")
                    .Where(j => j.InStock
                        && (category == null
                        || j.Categories.Any(c => c.UrlPath == category)
                        || j.Materials.Any(c => c.UrlPath == category)
                        || j.Gemstones.Any(c => c.UrlPath == category)))
                    .OrderBy(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(j => new
                    {
                        JewelryId = j.JewelryId,
                        Category = new
                        {
                            CategoryName = string.Join(",", j.Categories.Select(c => c.CategoryName).ToList())
                        },
                        Material = new
                        {
                            MaterialName = string.Join(",", j.Materials.Select(c => c.MaterialName).ToList())
                        },
                        Weight = j.Weight,
                        Article = j.Article,
                        PriceConverted = j.PriceConverted
                    })
                    .ToList();
            }
            else if (sortDirection.ToLower() == "desc")
            {
                model.JewelriesJson = repository.Jewelries
                    .Include("Materials")
                    .Include("Categories")
                    .Include("Gemstones")
                    .Where(j => j.InStock
                        && (category == null
                        || j.Categories.Any(c => c.UrlPath == category)
                        || j.Materials.Any(c => c.UrlPath == category)
                        || j.Gemstones.Any(c => c.UrlPath == category)))
                    .OrderByDescending(orderByFunc)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(j => new
                    {
                        JewelryId = j.JewelryId,
                        Category = new
                        {
                            CategoryName = string.Join(",", j.Categories.Select(c => c.CategoryName).ToList())
                        },
                        Material = new
                        {
                            MaterialName = string.Join(",", j.Materials.Select(c => c.MaterialName).ToList())
                        },
                        Weight = j.Weight,
                        Article = j.Article,
                        PriceConverted = j.PriceConverted
                    })
                    .ToList();
            }

            var jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize };
            var jsonModel = JsonConvert.SerializeObject(model, Formatting.Indented, jss);

            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetJewelryBySet(string set, string jewId)
        {
            var jews = repository
                .Jewelries
                .Include("Gemstones")
                .Include("Categories")
                .Include("Materials")
                .ToList()
                .Select(jew => new {
                    JewelryId = jew.JewelryId,
                    Set = jew.Set,
                    Category = new { CategoryName = string.Join(",", jew.Categories.Select(c => c.CategoryName).ToList()) },
                    Material = new { MaterialName = string.Join(",", jew.Materials.Select(c => c.MaterialName).ToList()) },
                    Weight = jew.Weight,
                    Article = jew.Article
                })
                .Where(j => j.Set == set && j.JewelryId != int.Parse(jewId))
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
        //[OutputCache(Duration = int.MaxValue, VaryByParam = "jewelryId")]
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

        public string GetListOfImages(int jewelryId)
        {
            var list = repository.Images.Where(i => i.JewelryId == jewelryId).Select(i => i.Id);
            return JsonConvert.SerializeObject(list);
        }

        #region private methods
        
        int _getPrevId(int id, IEnumerable<Category> cats)
        {
            var jews = repository.Jewelries.ToList().Where(j => (j.Categories.Contains(cats.FirstOrDefault())));

            int maxId = jews.Max(j => j.JewelryId);
            var jew = jews.FirstOrDefault(j => j.JewelryId == id - 1);

            if (jew != null)
                return jew.JewelryId;
            else if ((id - 1) < 1)
                return maxId;
            else
                return _getPrevId(id - 1, cats);
        }

        int _getNextId(int id, IEnumerable<Category> cats)
        {
            var jews = repository.Jewelries.ToList().Where(j => (j.Categories.Contains(cats.FirstOrDefault())));

            int minId = jews.Min(j => j.JewelryId);
            var jew = jews.FirstOrDefault(j => j.JewelryId == id + 1);

            if (jew != null)
                return jew.JewelryId;
            else if ((id + 1) > repository.Jewelries.Max(j => j.JewelryId))
                return minId;
            else
                return _getNextId(id + 1, cats);
        }
        #endregion
    }
}
