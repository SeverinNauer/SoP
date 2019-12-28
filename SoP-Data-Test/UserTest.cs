using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoP_Data;
using SoP_Data.Models;

namespace SoP_Data_Test
{
    [TestClass]
    public class UserTest
    {
        [TestInitialize]
        public void Initialize()
        {
            using (var db = new SoPContext())
            {
                //Remove users
                var userList = db.Users.ToList();
                db.Users.RemoveRange(userList);
                db.SaveChanges();

                //Add Admin User
                var adminUser = new User("admin", "admin");
                db.Users.Add(adminUser);
                db.SaveChanges();
            }
        }
        
        [TestMethod]
        public void GetUsers()
        {
            using (var db = new SoPContext())
            {
                var user = db.Users.FirstOrDefault();
                Assert.AreEqual(user.Username, "admin");
            }
        }

        [TestMethod]
        public void AddUser()
        {
            var user = new User("testuser", "password");
            using (var db = new SoPContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
