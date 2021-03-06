﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoP.ErrorHandling;
using SoP.Extensions;
using SoP.Models;
using SoP_Data.Dtos;
using SoP_Data.Models;
using SoP_Data.Services;
using System;
using System.Linq;

namespace SoP.Controllers
{
    [ApiController]
    public class PasswordEntryController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordEntryService _passwordEntryService;
        private readonly CategoryService _categoryService;

        public PasswordEntryController(UserService userService, PasswordEntryService passwordEntryService, CategoryService categoryService)
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
        [HttpPost]
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

        [Authorize]
        [HttpDelete]
        [Route("[controller]/delete")]
        public IActionResult Delete(int passwordEntryId)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                var passwordEntry = _passwordEntryService.GetForUser(passwordEntryId, user.Id);
                if (passwordEntry != null)
                {
                    _passwordEntryService.Delete(passwordEntry.Id);
                    return Ok(ResultMessages.Delete_Success);
                }
                return NotFound(ResultMessages.PasswordEntry_NotFound);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

        [Authorize]
        [HttpPut]
        [Route("[controller]/update")]
        public IActionResult Update([FromBody]PasswordEntryModel model)
        {
            var user = User.GetUser(_userService);
            if (user != null)
            {
                if (model.Id != null) {
                    var passwordEntry = _passwordEntryService.GetById(model.Id.Value, user.Id);
                    if (passwordEntry != null)
                    {
                        passwordEntry.Username = model.Username;
                        passwordEntry.Password = model.Password;
                        passwordEntry.Title = model.Title;
                        passwordEntry.Description = model.Description;
                        passwordEntry.Url = model.Url;
                        passwordEntry.ExpirationDate = model.ExpirationDate.HasValue ? (DateTime?)null : new DateTime(model.ExpirationDate.Value);
                        _passwordEntryService.Save(passwordEntry);
                        return Ok(ResultMessages.Update_Success);
                    }
                    return NotFound(ResultMessages.PasswordEntry_NotFound);
                }
                return NotFound(ResultMessages.PasswordEntry_IdNotSet);
            }
            return Unauthorized(ResultMessages.User_Authorization_NotOnDb);
        }

    }
}
