using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ITestService
    {
        Task<int> Update(Test entity);
        Task<TestGetVM> Add(TestAddVM Test);
        Task<int> Remove(int key);
        Task<TestGetVM> GetById(int key);
        Task<TestGetVM> GetByIdIncludeQuestionsAnswers(int key, QuestionType? questionType);

        Task<IEnumerable<TestGetVM>> GetAll(int page, int pageSize);
        Task<IEnumerable<TestGetVM>> GetAllByCategory(int categoryId);
        Task<IEnumerable<TestGetVM>> GetAllIncludeCategory();

    }
}
