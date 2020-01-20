using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoP_Data.Helpers;
using SoP_Data.Services;
using SoP.Models;

namespace SoP.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public AccountController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("[controller]/create")]
        public IActionResult Create([FromBody]LoginModel model)
        {
            var exists = _userService.UserExists(model.Username);
            if (exists)
            {
                return BadRequest("User.Creation.AlreadyExists");
            }
            var user = SoP_Data.Models.User.CreateNew(model.Username, model.Password);
            if (_userService.Create(user))
            {
                return Ok("User.Creation.Success");
            }
            return BadRequest("User.Creation.Failed");
        }

        [HttpPost]
        [Route("[controller]/login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var user = _userService.GetByUsername(model.Username);
            if (user != null)
            {
                if(BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Username)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return Ok("Bearer " + tokenHandler.WriteToken(token));
                }
            }
            return Unauthorized("User.Login.Invalid");
        }
    }
}
