using SoP_Data.Models;
using SoP_Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoP.Extensions
{
    public static class ClaimExtension
    {
        public static User GetUser(this ClaimsPrincipal claim, IUserService service)
        {
            var username = claim.Identity.Name;
            return service.GetByUsername(username);
        }
    }
}
