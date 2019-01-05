using GoldSilver.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldSilver.WebUI.Models
{
    public class JewelryViewModel
    {
        public JewelryViewModel()
        {
            Jewelry = new Jewelry();
            AllJewelryCategories = new List<SelectListItem>();
            AllJewelryMaterials = new List<SelectListItem>();
            AllJewelryGemstones = new List<SelectListItem>();
        }
        public Jewelry Jewelry { get; set; }

        public IEnumerable<SelectListItem> AllJewelryCategories { get; set; }
        public IEnumerable<SelectListItem> AllJewelryMaterials { get; set; }
        public IEnumerable<SelectListItem> AllJewelryGemstones { get; set; }

        private List<int> _selectedJewelryCategories;
        public List<int> SelectedJewelryCategories
        {
            get
            {
                if (_selectedJewelryCategories == null)
                {
                    _selectedJewelryCategories = Jewelry.Categories.Select(c => c.CategoryId).ToList();
                }
                return _selectedJewelryCategories;
            }
            set { _selectedJewelryCategories = value; }
        }

        private List<int> _selectedJewelryMaterials;
        public List<int> SelectedJewelryMaterials
        {
            get
            {
                if (_selectedJewelryMaterials == null)
                {
                    _selectedJewelryMaterials = Jewelry.Materials.Select(m => m.MaterialId).ToList();
                }
                return _selectedJewelryMaterials;
            }
            set { _selectedJewelryMaterials = value; }
        }

        private List<int> _selectedJewelryGemstones;
        public List<int> SelectedJewelryGemstones
        {
            get
            {
                if (_selectedJewelryGemstones == null)
                {
                    _selectedJewelryGemstones = Jewelry.Gemstones.Select(g => g.GemstoneId).ToList();
                }
                return _selectedJewelryGemstones;
            }
            set { _selectedJewelryGemstones = value; }
   
        }
    }
}