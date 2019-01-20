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
                Jewelry dbJewelry = context.Jewelries
                    .Include(j => j.Categories)
                    .Include(j => j.Gemstones)
                    .Include(j => j.Materials)
                    .Single(j => j.JewelryId == jewelry.JewelryId);

                if (dbJewelry != null)
                {
                    dbJewelry.Name = jewelry.Name;
                    dbJewelry.InStock = jewelry.InStock;
                    dbJewelry.Price = jewelry.Price;
                    dbJewelry.Description = jewelry.Description;
                    dbJewelry.Fineness = jewelry.Fineness;
                    dbJewelry.Weight = jewelry.Weight;
                    dbJewelry.JewelryId = jewelry.JewelryId;
                    dbJewelry.Article = jewelry.Article;
                    dbJewelry.Popularity = jewelry.Popularity;
                    dbJewelry.Set = jewelry.Set;
                    dbJewelry.VideoFromYoutube = jewelry.VideoFromYoutube;

                    if (jewelry.Categories != null && jewelry.Categories.Count > 0)
                    {
                        var categoriesToUpdate = jewelry.Categories
                            .Select(cat => cat.CategoryId)
                            .ToList();

                        var newCategories = context.Categories
                            .Where(c => categoriesToUpdate.Contains(c.CategoryId));

                        dbJewelry.Categories.Clear();
                        foreach (var newCat in newCategories)
                        {
                            dbJewelry.Categories.Add(newCat);
                        }
                    }
                    else
                    {
                        dbJewelry.Categories.Clear();
                    }

                    if (jewelry.Gemstones != null && jewelry.Gemstones.Count > 0)
                    {
                        var gemstonesToUpdate = jewelry.Gemstones
                        .Select(gem => gem.GemstoneId)
                        .ToList();

                        var newGemstones = context.Gemstones
                            .Where(g => gemstonesToUpdate.Contains(g.GemstoneId));

                        dbJewelry.Gemstones.Clear();
                        foreach (var newGemstone in newGemstones)
                        {
                            dbJewelry.Gemstones.Add(newGemstone);
                        }
                    }
                    else
                    {
                        dbJewelry.Gemstones.Clear();
                    }

                    if (jewelry.Materials != null && jewelry.Materials.Count > 0)
                    {
                        var materialsToUpdate = jewelry.Materials
                        .Select(mat => mat.MaterialId)
                        .ToList();
                        var newMaterials = context.Materials
                            .Where(m => materialsToUpdate.Contains(m.MaterialId));
                        dbJewelry.Materials.Clear();
                        foreach (var newMaterial in newMaterials)
                        {
                            dbJewelry.Materials.Add(newMaterial);
                        }
                    }
                    else
                    {
                        dbJewelry.Materials.Clear();
                    }

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
