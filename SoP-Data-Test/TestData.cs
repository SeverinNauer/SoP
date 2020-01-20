using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoP_Data;
using SoP_Data.Models;

namespace SoP_Data_Test
{
    [TestClass]
    public class TestData
    {
        [TestMethod]
        public void AddData()
        {
            using var db = new SoPContext();
            var dbUser = db.Users.FirstOrDefault(u => u.Username == "admin");
            var id = 0;
            if (dbUser != null)
            {
                id = dbUser.Id;
            }
            else
            {
                var user = User.CreateNew("admin", "admin"); 
                id = db.Users.Add(user).Entity.Id;
            }
            var password1 = new PasswordEntry()
            {
                Description = "Password 1 for category 1",
                Password = "Password not encrypted",
                Title = "Password 1"
            };
            var password2 = new PasswordEntry()
            {
                Description = "Password 2 for category 1",
                Password = "Password not encrypted",
                Title = "Password 2"
            };
            var password3 = new PasswordEntry()
            {
                Description = "Password 3 for category 2",
                Password = "Password not encrypted",
                Title = "Password 3"
            };
            var cat1 = Category.CreateNew("TestCategory 1", "Category for testing 1", id);
            cat1.Passwords.Add(password1);
            cat1.Passwords.Add(password2);
            var cat2 = Category.CreateNew("TestCategory 2", "Category for testing 2", id);
            cat2.Passwords.Add(password3);
            db.Categories.Add(cat1);
            db.Categories.Add(cat2);
            db.SaveChanges();
        }
    }
}
