using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldSilver.Domain.Entities;
using System.Data.Entity;

namespace GoldSilver.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(): base()
        {
            Database.SetInitializer<EFDbContext>(new EFDbInitializer());
        }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gemstone> Gemstones { get; set; }
        public DbSet<Material> Materials { get; set; }

        public DbSet<Image> Images { get; set; }


    }

    /*Test data*/
    public class EFDbInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            IList<Jewelry> Jeweleries = new List<Jewelry>();
            IList<Material> Materials = new List<Material>();
            IList<Category> Categories = new List<Category>();
            IList<Gemstone> Gemstones = new List<Gemstone>();

            Material gold = new Material() { MaterialId = 1, MaterialName = "Gold", Order = 1, UrlPath = "gold"};
            Material silver = new Material() {MaterialId = 2, MaterialName = "Silver", Order = 2, UrlPath = "silver"};
            Material platinum = new Material() {MaterialId = 3, MaterialName = "Platinum", Order = 3, UrlPath = "platinum"};

            Materials.Add(gold);
            Materials.Add(silver);
            Materials.Add(platinum);

            Category ring = new Category() { CategoryId = 1, CategoryName = "Rings", Order = 1, UrlPath = "ring"};
            Category earring = new Category() {CategoryId = 2, CategoryName = "Earrings", Order = 2, UrlPath = "earring"};

            Categories.Add(ring);
            Categories.Add(earring);

            Gemstone diamond = new Gemstone() { GemstoneId = 1, GemstoneName = "Diamond", Order = 1, UrlPath = "diamond"};
            Gemstone ruby = new Gemstone() {GemstoneId = 2, GemstoneName = "Ruby", Order = 2, UrlPath = "ruby"};

            Gemstones.Add(diamond);
            Gemstones.Add(ruby);


            //Add jeweleries
            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 1,
                Name = "New Jew 1",
                Article = "111",
                Description = "New description. . Popularity = 3",
                Weight = 1.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond,
                Popularity = 3
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 2,
                Name = "New Jew 2",
                Article = "112",
                Description = "New description for jew 2. Popularity = 2",
                Weight = 0.15M,
                Fineness = 555,
                MaterialId = 2,
                Material = silver,
                CategoryId = 2,
                Category = earring,
                GemstoneId = 1,
                Gemstone = ruby,
                Popularity = 2
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 3,
                Name = "New Jew 3",
                Article = "112",
                Description = "New description for jew 2. Popularity = 1",
                Weight = 3.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond,
                Popularity = 1
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 4,
                Name = "New Jew 4",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 4.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 5,
                Name = "New Jew 5",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 12.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 6,
                Name = "New Jew 6",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 22.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 6,
                Name = "New Jew 6",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 23.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 7,
                Name = "New Jew 7",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 8,
                Name = "New Jew 8",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 9,
                Name = "New Jew 9",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 10,
                Name = "New Jew 10",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 11,
                Name = "New Jew 11",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 12,
                Name = "New Jew 12",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 13,
                Name = "New Jew 13",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 2.15M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 14,
                Name = "New Jew 14",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 0.11M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 14,
                Name = "New Jew 15",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 0.11M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 14,
                Name = "New Jew 15",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 0.11M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            Jeweleries.Add(new Jewelry()
            {
                JewelryId = 14,
                Name = "New Jew 16",
                Article = "112",
                Description = "New description for jew 2",
                Weight = 0.11M,
                Fineness = 555,
                MaterialId = 1,
                Material = gold,
                CategoryId = 1,
                Category = ring,
                GemstoneId = 1,
                Gemstone = diamond
            });

            foreach (Category cat in Categories)
                context.Categories.Add(cat);

            foreach (Material mat in Materials)
                context.Materials.Add(mat);

            foreach (Gemstone gem in Gemstones)
                context.Gemstones.Add(gem);

            foreach (Jewelry jew in Jeweleries)
                context.Jewelries.Add(jew);

            base.Seed(context);
        }
    }
}
