using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.ViewModels
{
    public class CreateCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}