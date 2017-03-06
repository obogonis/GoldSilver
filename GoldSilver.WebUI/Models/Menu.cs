using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoldSilver.Domain.Entities;

namespace GoldSilver.WebUI.Models
{
    public class Menu
    {
        public List<Category> Categories { get; set; }
        public List<Gemstone> Gemstones { get; set; }
        public List<Material> Materials { get; set; }

        public string CurrentItem { get; set; }
    }
}