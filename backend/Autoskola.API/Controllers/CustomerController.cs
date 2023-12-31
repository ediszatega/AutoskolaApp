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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(int start = 1, int range = 100)
        {
            var result = await service.GetAll(start, range);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerAddVM customer)
        {
            var result = await service.Add(customer);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerUpdateVM customer)
        {
            var result = await service.Update(customer);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await service.Remove(id);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await service.Deactivate(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIncludeCities(string? search, int start = 1, int range = 100)
        {
            var result = await service.GetAllIncludeCities(search, start, range);
            return Ok(result);
        }
    }
}
