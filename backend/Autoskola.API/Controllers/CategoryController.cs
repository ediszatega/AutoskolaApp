using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 100)
        {
            return Ok(service.GetAll(page, pageSize));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.GetById(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryAddVM category)
        {
            return Ok(service.Add(category));
        }

        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            return Ok(service.Update(category));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return Ok(service.Remove(id));
        }
    }
}
