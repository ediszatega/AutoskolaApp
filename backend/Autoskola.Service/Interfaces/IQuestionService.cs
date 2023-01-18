using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IQuestionService
    {

        Task<int> Add(QuestionAddVM question);
        Task<int> Update(QuestionUpdateVM entity);
        Task<int> Remove(int key);
        Task<Question> GetById(int key);
        Task<IEnumerable<Question>> GetAll(int pageNumber, int pageSize);
        Task<IEnumerable<Question>> GetQuestionsByType(QuestionType type, int testId);
        Task<IEnumerable<QuestionGetVM>> GetQuestionsByTest(int testId);
        Task<int> AddAnswerToQuestion(int quesitonId, AnswerAddVM answer);
        Task<int> UploadImage(int id, IFormFile image, string root);
    }
}
