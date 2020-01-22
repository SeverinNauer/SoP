using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoP.ErrorHandling;
using SoP.Extensions;
using SoP.Models;
using SoP_Data.Dtos;
using SoP_Data.Models;
using SoP_Data.Services;

namespace SoP.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;

        public CategoryController(UserService userService, CategoryService categoryService)
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
                var category = Category.CreateNew(model.Title, model.Description, user.Id);
                var result = _categoryService.Create(category);
                if (result)
                {
                    return Ok(ResultMessages.Creation_Success);
                }
                return BadRequest(ResultMessages.Creation_Failed);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpPut]
        [Route("[controller]/update")]
        public IActionResult Update([FromBody]CategoryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var cat = _categoryService.GetById(model.Id, user.Id);
                if (cat != null)
                {
                    cat.Title = model.Title;
                    cat.Description = model.Description;
                    _categoryService.Save(cat);
                    return Ok(ResultMessages.Update_Success);
                }
                return NotFound(ResultMessages.Category_NotFound);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
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
                        return BadRequest(ResultMessages.Catergoy_Delete_Error_Has_References);
                    }
                    _categoryService.Delete(categoryId);
                    return Ok(ResultMessages.Delete_Success);
                }
                return NotFound(ResultMessages.Category_NotFound);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/get")]
        public IActionResult GetById(int categoryId)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var category = _categoryService.GetById(categoryId, user.Id);
                if (category != null)
                {
                    return Ok(new CategoryDto(category));
                }
                return NotFound(ResultMessages.Category_NotFound);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/getAll")]
        public IActionResult Get()
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var categories = _categoryService.GetForUser(user.Id);
                return Ok(categories.Select(cat => new CategoryDto(cat)).ToList());
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }
    }
}