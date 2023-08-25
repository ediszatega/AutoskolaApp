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
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var result = await service.GetAll(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByCategory(int categoryId)
        {
            var result = await service.GetAllByCategory(categoryId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdIncludeQuestionsAnswers(int id, QuestionType? questionType)
        {
            var result = await service.GetByIdIncludeQuestionsAnswers(id, questionType);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TestAddVM test)
        {
            var result = await service.Add(test);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Test test)
        {
            var result = await service.Update(test);
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
