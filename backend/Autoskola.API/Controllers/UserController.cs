using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Interfaces;
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

        [HttpGet]
        public IActionResult GetAll(string? search, int start = 1, int range = 100)
        {
            return Ok(service.GetAll(search, start, range));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.GetById(id));
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserAddVM user)
        {
            return Ok(service.Add(user));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserUpdateVM user)
        {
            return Ok(service.Update(user));
        }
    }
}
