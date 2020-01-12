using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoP_Data.Models;

namespace SoP.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("[controller]/login")]
        public string Login([FromBody]LoginModel model)
        {
            var results = new List<ValidationResult>();
            var test = Validator.TryValidateObject(model, new ValidationContext(model), results);
            return "Test";
        }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required(ErrorMessage = "required btw")]
        public string Password { get; set; }
    }
}
