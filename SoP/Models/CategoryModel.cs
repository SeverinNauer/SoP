using System.ComponentModel.DataAnnotations;

namespace SoP.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
