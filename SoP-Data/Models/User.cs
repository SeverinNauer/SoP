using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoP_Data.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        public string Password { get; private set; }
        public List<Category> Categories { get; set; }

        public static User CreateNew(string username, string password)
        {
            return new User()
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
        }
    }
}
