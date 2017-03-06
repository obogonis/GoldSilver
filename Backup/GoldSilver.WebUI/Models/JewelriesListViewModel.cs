using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldSilver.WebUI.Models
{
    public class JewelriesListViewModel
    {
        public IEnumerable<Jewelry> Jewelries { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}