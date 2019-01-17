using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.ViewModels
{
    public class ProductSearchResultsViewModel
    {
        public string SearchProduct { get; set; }
        public string SearchDescription { get; set; }

        public ProductSearchResultsViewModel()
        {
            Products = new List<ProductListViewModel>();
        }

        public class ProductListViewModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
        
        public List<ProductListViewModel> Products { get; set; }

        public string CurrentSort { get; set; }
    }
}