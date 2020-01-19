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
using SoP_Data.Dtos;
using SoP_Data.Helpers;
using SoP_Data.Models;
using SoP_Data.Services;

namespace SoP.Controllers
{
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

        [HttpPost]
        [Authorize]
        [Route("[controller]/create")]
        public IActionResult Create([FromBody]CategoryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var category = Category.CreateNew(model.Title,model.Description, user.Id);
                var result = _categoryService.Create(category);
                if (result)
                {
                    return Ok("Category.Creation.Success");
                }
                return BadRequest("Category.Creation.Failed");
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }

        [Authorize]
        [HttpPut]
        [Route("[controller]/update")]
        public IActionResult Update([FromBody]CategoryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var cat = _categoryService.GetById(model.CategoryId, user.Id);
                if(cat != null)
                {
                    cat.Title = model.Title;
                    cat.Description = model.Description;
                    _categoryService.Save(cat);
                    return Ok("Category.Update.Success");
                }
                return NotFound("User.Category.NotFound");
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }

        [Authorize]
        [HttpDelete]
        [Route("[controller]/delete")]
        public IActionResult Delete(int categoryId)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var cat = _categoryService.GetById(categoryId, user.Id);
                if (cat != null)
                {
                    if (cat.Passwords.Any())
                    {
                        return BadRequest("Catergoy.Delete.Error.Has.References");
                    }
                    if (_categoryService.Delete(categoryId))
                    {
                        return Ok("Category.Delete.Success");
                    }
                }
                return NotFound("User.Category.NotFound");
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/get")]
        public IActionResult Get(int? categoryId = null)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                if (categoryId == null)
                {
                    var categories = _categoryService.GetForUser(user.Id);
                    return Ok(categories.Select(cat => new CategoryDto(cat)).ToList());
                }
                var category = _categoryService.GetById(categoryId.Value, user.Id);
                if (category != null)
                {
                    return Ok(new CategoryDto(category));
                }
                return NotFound("User.Category.NotFound");
            }
            return Unauthorized("User.Authorization.NotOnDb");
        }
    }
}