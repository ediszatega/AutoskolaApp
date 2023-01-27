﻿using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MotTestController : ControllerBase
    {
        private readonly IMotTestService service;

        public MotTestController(IMotTestService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int vehicleId, string? search, int pageNumber = 1, int pageSize = 100)
        {
            var result = await service.GetMotTests(vehicleId, search, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }
    }
}
