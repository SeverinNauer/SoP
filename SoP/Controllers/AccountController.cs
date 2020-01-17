using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SoP_Data;
using SoP_Data.Services;

namespace SoP.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create([FromBody]LoginModel model)
        {
            var user = SoP_Data.Models.User.CreateNew(model.Username, model.Password);
            return Ok(user);
        }
    }
}
