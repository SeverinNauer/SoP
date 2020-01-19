using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SoP_Data.Models;

namespace SoP_Data.Dtos
{
    public class PasswordEntryDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public PasswordEntryDto(PasswordEntry pw)
        {
            Id = pw.Id;
            Username = pw.Username;
            Password = pw.Password;
            Title = pw.Title;
            Description = pw.Description;
            Url = pw.Url;
            ExpirationDate = pw.ExpirationDate;
        }
    }
}
