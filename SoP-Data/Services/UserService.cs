﻿using Microsoft.Extensions.Options;
using SoP_Data.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SoP_Data.Models;

namespace SoP_Data.Services
{
    public interface IUserService
    {
        bool Create(User user);
        public bool UserExists(string username);
        User GetByUsername(string username);
        User GetByID(int id);
    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

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
