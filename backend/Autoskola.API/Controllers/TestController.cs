using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService service;

        public TestController(ITestService service)
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
        public IActionResult Add([FromBody] TestAddVM category)
        {
            try
            {
                return Ok(service.Add(category));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Test test)
        {
            return Ok(service.Update(test));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return Ok(service.Remove(id));
        }
    }
}
