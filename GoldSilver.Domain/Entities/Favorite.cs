using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSilver.Domain.Entities
{
    public class Favorite
    {
        private List<Jewelry> favLine = new List<Jewelry>();

        public void AddItem(Jewelry jew)
        {
            Jewelry jewelry = favLine.Where(p => p.JewelryId == jew.JewelryId).FirstOrDefault();

            if (jewelry == null)
            {
                favLine.Add(jew);
            }

        }

        public void RemoveLine(Jewelry jew)
        {
            favLine.RemoveAll(l => l.JewelryId == jew.JewelryId);
        }

        public decimal TotalUserFavs()
        {
            return favLine.Count();
        }

        public void Clear()
        {
            favLine.Clear();
        }

        public IEnumerable<Jewelry> Lines
        {
            get { return favLine; }
        }
    }

}
