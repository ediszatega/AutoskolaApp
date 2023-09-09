using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Autoskola.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autoskola.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService service;
        private readonly IWebHostEnvironment environment;

        public QuestionController(IQuestionService service, IWebHostEnvironment environment)
        {
            this.service = service;
            this.environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 100)
        {
            var result = await service.GetAll(page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByType(QuestionType type, int testId)
        {
            var result = await service.GetQuestionsByType(type, testId);
            return Ok(result);
        }

        [HttpGet("{testId}")]
        public async Task<IActionResult> GetByTest(int testId)
        {
            var result = await service.GetQuestionsByTest(testId);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] QuestionAddVM question)
        {
            var result = await service.Add(question);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddAnswer(int id, [FromBody] AnswerAddVM answer)
        {
            var result = await service.AddAnswerToQuestion(id, answer);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddAnswers(int id, [FromBody] AnswerAddVM[] answers)
        {
            var result = await service.AddAnswersToQuestion(id, answers);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            string root = this.environment.WebRootPath;
            var result = await service.UploadImage(id, image, root);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QuestionUpdateVM question)
        {
            var result = await service.Update(question);
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
