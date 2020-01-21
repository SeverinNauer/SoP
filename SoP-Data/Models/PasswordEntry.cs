using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoP_Data.Models
{
    public class PasswordEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime? ExpirationDate { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public static PasswordEntry CreateNew(string username, string password, string title, string description, string url, long? expirationDate, int categoryId) 
        {
            return new PasswordEntry()
            {
                Username = username,
                Password = password,
                Title = title,
                Description = description,
                Url = url,
                ExpirationDate = expirationDate.HasValue ? (DateTime?)null : new DateTime(expirationDate.Value),
                CategoryId = categoryId
            };
        }

    }
}
