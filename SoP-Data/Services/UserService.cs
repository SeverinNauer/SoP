using System;
using System.Linq;
using SoP_Data.Models;

namespace SoP_Data.Services
{
    public class UserService
    {
        public bool Create(User user)
        {
            try
            {
                using var dbContext = new SoPContext();
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UserExists(string username)
        {
            var user = GetByUsername(username);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public User GetByID(int id)
        {
            return Get(u => u.Id == id);
        }

        public User GetByUsername(string username)
        {
            return Get(u => u.Username == username);
        }

        private User Get(Func<User, bool> func) {
            using var dbContext = new SoPContext();
            return dbContext.Users.FirstOrDefault(func);
        }
    }
}
