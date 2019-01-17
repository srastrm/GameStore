using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string CurrentSort { get; set; }

        public ProductCategoryViewModel()
        {
            Products = new List<Product>();
            ProductCategories = new List<ProductCategory>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}