using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoldSilver.Domain.Entities
{
    public class Jewelry
    {
        [HiddenInput(DisplayValue = false)]
        public int JewelryId { get; set; }

        [Required(ErrorMessage = "Введіть назву прикраси. Це обов'язкове поле.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введіть артикул прикраси. Це обов'язкове поле.")]
        public string Article { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Введіть в це поле додатнє число.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Виберіть матеріал прикраси. Це обов'язкове поле.")]
        public int? MaterialId { get; set; }
        public int? GemstoneId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Введіть в це поле додатнє ціле число.")]
        public int? Fineness { get; set; }

        [Range(0, int.MaxValue)]
        public int Popularity { get; set; }

        //[Required(ErrorMessage = "Виберіть категорію прикраси. Це обов'язкове поле.")]
        [HiddenInput(DisplayValue=false)]
        [Required(ErrorMessage = "Виберіть категорію прикраси. Це обов'язкове поле.")]
        public int? CategoryId  { get; set; }

        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string Set { get; set; }

        [NotMapped]
        public int PrevJewelry { get; set; }

        [NotMapped]
        public int NextJewelry { get; set; }

        public int? ImageId { get; set; }

        public ICollection<Image> Images { get; set; }
        public Category Category { get; set; }
        public Material Material { get; set; }
        public Gemstone Gemstone { get; set; }

        public Jewelry()
        {
            Popularity = 0;
        }
    }

    public class Image
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set;}
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string ImageDescription { get; set; }
        public int JewelryId { get; set; }
        public Jewelry Jewelery { get; set; }
    }

    public class Material
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public int? Order { get; set; }
        public string UrlPath { get; set; }
    }

    public class Gemstone
    {
        public int GemstoneId { get; set; }
        public string GemstoneName { get; set; }
        public int? Order { get; set; }
        public string UrlPath { get; set; }
    }

    public class Category
    {
        public int CategoryId {get;set;}
        public string CategoryName { get; set; }
        public int? Order { get; set; }
        public string UrlPath { get; set; }

        public bool? ShowOnMain { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string ImageDescription { get; set; }

    }

}
