using System;
using System.Collections.Generic;
using System.Text;

namespace SoP_Data.Services
{
    public interface IUserService
    {
        
    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
    }
}
