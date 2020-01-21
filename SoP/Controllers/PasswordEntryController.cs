using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoP.ErrorHandling;
using SoP.Extensions;
using SoP.Models;
using SoP_Data.Dtos;
using SoP_Data.Models;
using SoP_Data.Services;
using System.Linq;

namespace SoP.Controllers
{
    [ApiController]
    public class PasswordEntryController : ControllerBase
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
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/get")]
        public IActionResult Get(int passwordEntryId)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var passwordEntry = _passwordEntryService.GetForUser(passwordEntryId, user.Id);
                if (passwordEntry != null)
                {
                    return Ok(new PasswordEntryDto(passwordEntry));
                }
                return NotFound(ResultMessages.PasswordEntry_NotFound);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/create")]
        public IActionResult Create([FromBody]PasswordEntryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var cat = _categoryService.GetById(model.CategoryId, user.Id);
                if (cat != null)
                {
                    var passwordEntry = PasswordEntry.CreateNew(model.Username, model.Password, model.Title, model.Description, model.Url, model.ExpirationDate, cat.Id);
                    _passwordEntryService.Save(passwordEntry);
                    return Ok(ResultMessages.Creation_Success);
                }
                return BadRequest(ResultMessages.Creation_Failed);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }
    }
}
