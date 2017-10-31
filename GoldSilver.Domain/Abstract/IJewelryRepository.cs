using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldSilver.Domain.Entities;
using System.Data.Entity;

namespace GoldSilver.Domain.Abstract
{
    public interface IJewelryRepository
    {
        DbSet<Jewelry> Jewelries { get; }
        DbSet<Category> Categories { get; }
        DbSet<Gemstone> Gemstones { get; }
        DbSet<Material> Materials { get; }
        DbSet<Image> Images { get; }

        void SaveJewelry(Jewelry jewelry);
        Jewelry DeleteJew(int jewelryId);
        Image DeleteImage(int imageId);
    }
}
