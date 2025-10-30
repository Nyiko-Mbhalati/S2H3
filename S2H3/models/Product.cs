using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2H3.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int BrandId { get; set; }
        public string BrandName { get; set; }
		public string CategoryId { get; set; }
        public string CategoryName { get; set; }
		public decimal ModelYear { get; set; }
        public decimal ListPrice { get; set; }
		
    }
}