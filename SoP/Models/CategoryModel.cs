using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoP.Models
{
    public class CategoryModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
