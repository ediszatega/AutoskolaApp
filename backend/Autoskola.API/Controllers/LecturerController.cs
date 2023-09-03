using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService service;

        public LecturerController(ILecturerService service)
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
        public async Task<IActionResult> Add([FromBody] LecturerAddVM lecturer)
        {
            var result = await service.Add(lecturer);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LecturerUpdateVM lecturer)
        {
            var result = await service.Update(lecturer);
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
