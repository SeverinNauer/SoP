using System;
using System.Collections.Generic;
using System.Text;
using SoP_Data.Models;

namespace SoP_Data.Services
{
    public interface ICategoryService
    {
        bool Create(Category category);
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
    }
}
