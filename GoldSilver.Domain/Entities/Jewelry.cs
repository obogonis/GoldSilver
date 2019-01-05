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
        public Jewelry()
        {
            this.Categories = new HashSet<Category>();
            this.Materials = new HashSet<Material>();
            this.Gemstones = new HashSet<Gemstone>();

            this.Popularity = 0;
        }

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

        [Display(Name="Ціна")]
        [Range(0, int.MaxValue, ErrorMessage = "Введіть додатнє число.")]
        public decimal? Price { get; set; }

        [Display(Name = "В наявності")]
        public bool InStock { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Введіть в це поле додатнє ціле число.")]
        public int? Fineness { get; set; }

        [Range(0, int.MaxValue)]
        public int Popularity { get; set; }


        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public string Set { get; set; }

        [NotMapped]
        public int PrevJewelry { get; set; }

        [NotMapped]
        public int NextJewelry { get; set; }

        [Display(Name="URL for YouTube video")]
        public string VideoFromYoutube { get; set; }

        public int? ImageId { get; set; }

        public ICollection<Image> Images { get; set; }

        [Display(Name = "Категорії")]
        public virtual ICollection<Category> Categories { get; set; }
        [Display(Name = "Матеріали")]
        public virtual ICollection<Material> Materials { get; set; }
        [Display(Name = "Вставки")]
        public virtual ICollection<Gemstone> Gemstones { get; set; }

        [NotMapped]
        public IEnumerable<string> CategoriesNames
        {
            get
            {
                return this.Categories.Select(c => c.CategoryName + " ");
            }
        }

        [NotMapped]
        public IEnumerable<string> GemstonesNames
        {
            get
            {
                return this.Gemstones.Select(c => c.GemstoneName + " ");
            }
        }

        [NotMapped]
        public IEnumerable<string> MaterialsNames
        {
            get
            {
                return this.Materials.Select(c => c.MaterialName + " ");
            }
        }

        //[Required(ErrorMessage = "Виберіть матеріал прикраси. Це обов'язкове поле.")]
        //public int? MaterialId { get; set; }
        //public int? GemstoneId { get; set; }

        //[Required(ErrorMessage = "Виберіть категорію прикраси. Це обов'язкове поле.")]
        //[HiddenInput(DisplayValue = false)]
        //[Required(ErrorMessage = "Виберіть категорію прикраси. Це обов'язкове поле.")]
        //public int? CategoryId { get; set; }
    }

    public class Image
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string ImageDescription { get; set; }
        public int JewelryId { get; set; }
        public Jewelry Jewelery { get; set; }
    }

    public class Material
    {
        public Material()
        {
            this.Jewelries = new HashSet<Jewelry>();
        }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public int? Order { get; set; }
        public string UrlPath { get; set; }

        public virtual ICollection<Jewelry> Jewelries { get; set; }
    }

    public class Gemstone
    {
        public Gemstone()
        {
            this.Jewelries = new HashSet<Jewelry>();
        }
        public int GemstoneId { get; set; }
        public string GemstoneName { get; set; }
        public int? Order { get; set; }
        public string UrlPath { get; set; }

        public virtual ICollection<Jewelry> Jewelries { get; set; }
    }

    public class Category
    {
        public Category()
        {
            this.Jewelries = new HashSet<Jewelry>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Order { get; set; }
        public string UrlPath { get; set; }
        public bool? ShowOnMain { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string ImageDescription { get; set; }

        public virtual ICollection<Jewelry> Jewelries { get; set; }
    }

}
