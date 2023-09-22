using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService service;

        public NewsController(INewsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 100)
        {
            var result = await service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewsAddVM vehicle)
        {
            var result = await service.Add(vehicle);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NewsUpdateVM vehicle)
        {
            var result = await service.Update(vehicle);
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
