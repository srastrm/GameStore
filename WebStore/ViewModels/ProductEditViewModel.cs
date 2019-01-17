using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebStore.Models;

namespace WebStore.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public int SelectedCategoryId { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public ProductCategory Category { get; set; }
    }
}