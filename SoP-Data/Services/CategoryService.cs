using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoP_Data.Models;

namespace SoP_Data.Services
{
    public interface ICategoryService
    {
        bool Create(Category category);
        Category GetById(int catId, int userId);
        void Save(Category category);
        bool Delete(int categoryId);
        List<Category> GetForUser(int userId);
    }
    public class CategoryService : ICategoryService
    {
        public bool Create(Category category)
        {
            try
            {
                using var context = new SoPContext();
                context.Categories.Add(category);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save(Category category)
        {
            using var context = new SoPContext();
            context.Update(category);
            context.SaveChanges();
        }

        public bool Delete(int categoryId)
        {
            using var context = new SoPContext();
            var cat = context.Categories.FirstOrDefault(cat => cat.Id == categoryId);
            if (cat != null)
            {
                context.Categories.Remove(cat);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Category GetById(int catId, int userId)
        {
            return Get(cat => cat.Id == catId && cat.UserId == userId);
        }

        public List<Category> GetForUser(int userId)
        {
            using var context = new SoPContext();
            return context.Categories.Where(cat => cat.UserId == userId).Include(cat => cat.Passwords).ToList();
        }

        private Category Get(Func<Category, bool> func)
        {
            using var context = new SoPContext();
            return context.Categories.Include(cat => cat.Passwords).FirstOrDefault(func);
        }
    }
}
