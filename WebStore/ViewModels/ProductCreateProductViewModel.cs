using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class ProductCreateProductViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SelectedCategoryId { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public ProductCategory Category { get; set; }
        

    }
}