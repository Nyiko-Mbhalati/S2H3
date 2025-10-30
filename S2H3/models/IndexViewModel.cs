using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2H3.Models
{
	public class IndexViewModel
	{
		public List<Product> ProductList { get; set; }
        public List<Customer> CustomerList { get; set; }
        public List<Staff> StaffList { get; set; }
        public List<SelectListItem> Brands { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public IndexViewModel()
        {
            ProductList = new List<Product>();
            CustomerList = new List<Customer>();
            StaffList = new List<Staff>();

        }

        // Optional: Properties for maintaining filter state
        public int? SelectedBrandId { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}