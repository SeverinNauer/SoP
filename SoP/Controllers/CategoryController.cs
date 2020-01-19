using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoP.Extensions;
using SoP.Models;
using SoP_Data.Helpers;
using SoP_Data.Models;
using SoP_Data.Services;

namespace SoP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public CategoryController(IUserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create([FromBody]CategoryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var category = new Category()
                {
                    Description = model.Description,
                    Title = model.Title,
                    UserId = user.Id
                };
                var result = _categoryService.Create(category);
                if (result)
                {
                    return Ok("Category.Creation.Success");
                }
                return BadRequest("Category.Creation.Failed");
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }
    }
}