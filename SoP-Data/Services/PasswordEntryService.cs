using SoP_Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SoP_Data.Services
{
    public class PasswordEntryService
    {
        public List<PasswordEntry> GetForUser(int userId)
        {
            using var context = new SoPContext();
            var categories = context.Categories
                .Where(cat => cat.UserId == userId)
                .Select(cat => cat.Id)
                .ToList();
            return context.PasswordEntries
                .Where(pass => categories.Contains(pass.CategoryId))
                .ToList();
        }
        public PasswordEntry GetForUser(int passwordEntryId, int userId)
        {
            using var context = new SoPContext();
            return context.PasswordEntries
                    .Include(pass => pass.Category)
                    .FirstOrDefault(pass => pass.Id == passwordEntryId && pass.Category.UserId == userId);
        }

        public void Save(PasswordEntry passwordEntry) 
        {
            using var context = new SoPContext();
            context.Update(passwordEntry);
            context.SaveChanges();
        }

        public void Delete(int passwordEntryId) 
        {
            using var context = new SoPContext();
            var pass = context.PasswordEntries.FirstOrDefault(pass => pass.Id == passwordEntryId);
            if (pass != null)
            {
                context.PasswordEntries.Remove(pass);
                context.SaveChanges();
            }
        }

        public PasswordEntry GetById(int passwordEntryId, int userId)
        {
            using var context = new SoPContext();
            return context.PasswordEntries
                .Include(pass => pass.Category)
                .FirstOrDefault(pass => pass.Id == passwordEntryId && pass.Category.UserId == userId);
        }
    }
}
