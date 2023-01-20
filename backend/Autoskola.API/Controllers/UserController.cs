﻿using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginVM user)
        {
            var result = await service.Login(user);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterVM user)
        {
            await service.Register(user);
            return Ok(new { StatusCode=200, Message ="Registration successful" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(string? search, int start = 1, int range = 100)
        {
            var result = await service.GetAll(search, start, range);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddVM user)
        {
            var result = await service.Add(user);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateVM user)
        {
            var result = await service.Update(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await service.Remove(id);
            return Ok(result);
        }
    }
}
