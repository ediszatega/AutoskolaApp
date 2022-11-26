using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService service;

        public CityController(ICityService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, int page = 1, int pageSize = 100)
        {
            return Ok(service.GetCities(search, page, pageSize));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.GetById(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CityAddVM city)
        {
            return Ok(service.Add(city));
        }

        [HttpPut]
        public IActionResult Update([FromBody] City city)
        {
            return Ok(service.Update(city));
        }
    }
}
