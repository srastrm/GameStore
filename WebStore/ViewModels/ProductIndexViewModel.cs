using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.ViewModels
{
    public class ProductIndexViewModel
    {
        public string SearchProduct { get; set;}
        public string SearchDescription { get; set; }

        public ProductIndexViewModel()
        {
            Categories = new List<ProductCategoryListViewModel>();
            Products = new List<ProductListViewModel>();
        }

        public class ProductCategoryListViewModel
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
        }

        public class ProductListViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }

        public List<ProductCategoryListViewModel> Categories { get; set; }
        public List<ProductListViewModel> Products { get; set; }

        public string CurrentSort { get; set; }
    }
}