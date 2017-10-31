using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldSilver.Domain.Abstract;
using GoldSilver.Domain.Entities;
using System.Data.Entity;

namespace GoldSilver.Domain.Concrete
{
    public class EFJeweleryRepository: IJewelryRepository
    {
        private EFDbContext context = new EFDbContext();

        public DbSet<Jewelry> Jewelries
        {
            get { return context.Jewelries; }
        }

        public DbSet<Category> Categories
        {
            get { return context.Categories; }
        }

        public DbSet<Gemstone> Gemstones
        {
            get { return context.Gemstones; }
        }

        public DbSet<Material> Materials
        {
            get { return context.Materials; }
        }

        public DbSet<Image> Images
        {
            get { return context.Images; }
        }    

        public void SaveJewelry(Jewelry jewelry)
        {
            if (jewelry.JewelryId == 0)
            {
                context.Jewelries.Add(jewelry);
                context.SaveChanges();
            }
            else
            {
                Jewelry dbJewelry = context.Jewelries.Find(jewelry.JewelryId);
                if (dbJewelry != null)
                {
                    dbJewelry.Name = jewelry.Name;
                    dbJewelry.Description = jewelry.Description;
                    dbJewelry.MaterialId = jewelry.MaterialId;
                    dbJewelry.Weight = jewelry.Weight;
                    dbJewelry.JewelryId = jewelry.JewelryId;
                    dbJewelry.GemstoneId = jewelry.GemstoneId;
                    dbJewelry.CategoryId = jewelry.CategoryId;
                    dbJewelry.Article = jewelry.Article;

                    dbJewelry.Popularity = jewelry.Popularity;
                    dbJewelry.Set = jewelry.Set;

                    if (jewelry.Images != null)
                    {
                        dbJewelry.Images = jewelry.Images;
                        dbJewelry.ImageData = jewelry.ImageData;
                        dbJewelry.ImageMimeType = jewelry.ImageMimeType;
                    }

                    context.SaveChanges();

                }
            }
        }

        public Jewelry DeleteJew(int jewelryId)
        {
            Jewelry dbEntry = context.Jewelries.Find(jewelryId);
            if (dbEntry != null)
            {
                context.Jewelries.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Image DeleteImage(int imageId)
        {
            Image dbEntry = context.Images.Find(imageId);
            if (dbEntry != null)
            {
                context.Images.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;

        }
    }
}
