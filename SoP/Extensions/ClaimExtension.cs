using SoP_Data.Models;
using SoP_Data.Services;
using System.Security.Claims;

namespace SoP.Extensions
{
    public static class ClaimExtension
    {
        public static User GetUser(this ClaimsPrincipal claim, UserService service)
        {
            var username = claim.Identity.Name;
            return service.GetByUsername(username);
        }
    }
}
