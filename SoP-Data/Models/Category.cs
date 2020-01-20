using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoP_Data.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public List<PasswordEntry> Passwords { get; set; } 
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public Category()
        {
            Passwords = new List<PasswordEntry>();
        }

        public static Category CreateNew(string title, string description, int userId)
        {
            var category = new Category()
            {
                Title = title,
                Description = description,
                UserId = userId
            };
            return category;
        }
    }
}
