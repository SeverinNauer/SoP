using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoP.Extensions;
using SoP_Data.Dtos;
using SoP_Data.Services;
using System.Linq;

namespace SoP.Controllers
{
    [ApiController]
    public class PasswordEntryController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordEntryService _passwordEntryService;
        private readonly ICategoryService _categoryService;

        public PasswordEntryController(IUserService userService, IPasswordEntryService passwordEntryService, ICategoryService categoryService) 
        {
            _userService = userService;
            _passwordEntryService = passwordEntryService;
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/getAll")]
        public IActionResult GetAll()
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var passwordEntries = _passwordEntryService.GetForUser(user.Id);
                return Ok(passwordEntries.Select(pass => new PasswordEntryDto(pass)).ToList());
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }
    }
}
