using SoP_Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoP_Data.Services
{
    public interface IPasswordEntryService
    {
        List<PasswordEntry> GetForUser(int userId);
    }

    class PasswordEntryService
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
        
    }
}
