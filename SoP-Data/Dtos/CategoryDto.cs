using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoP_Data.Models;

namespace SoP_Data.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PasswordEntryDto> Passwords { get; set; }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            Title = category.Title;
            Description = category.Description;
            Passwords = category.Passwords.Select(pw => new PasswordEntryDto(pw)).ToList();
        }
    }
}
