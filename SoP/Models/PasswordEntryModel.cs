using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoP.Models
{
    public class PasswordEntryModel
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string Url { get; set; }
        public long? ExpirationDate { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
