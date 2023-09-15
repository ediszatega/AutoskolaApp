using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Utils.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Autoskola.Service.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TestGetVM> Add(TestAddVM test)
        {
            var category = await unitOfWork.Categories.Get(test.CategoryId);
            if (category == null)
                throw new HttpException("Invalid Category ID", 400);
            var newTest = new Test() { Description = test.Description, CategoryId = test.CategoryId };
            newTest = await unitOfWork.Tests.Add(newTest);
            await unitOfWork.Complete();

            return mapper.Map<TestGetVM>(newTest);

        }

        public async Task<int> Update(Test entity)
        {
            var test = await unitOfWork.Tests.Get(entity.Id);
            if (test == null)
                throw new HttpException("Test with selectdd ID not found", 400);
            test.Description = entity.Description;
            test.CategoryId = entity.CategoryId;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var test = await unitOfWork.Tests.Get(key);
            if (test == null)
                throw new HttpException("Test with selected ID not found", 400);
            unitOfWork.Tests.Remove(test);
            return await unitOfWork.Complete();
        }

        public async Task<TestGetVM> GetById(int key)
        {
            var test = await unitOfWork.Tests.Get(key);
            return mapper.Map<TestGetVM>(test);
        }
        public async Task<TestGetVM> GetByIdIncludeQuestionsAnswers(int key, QuestionType? questionType)
        {
            var test = await unitOfWork.Tests.GetIncludeCategory(key);

            await unitOfWork.Entry(test)
                    .Collection(t => t.Questions)
                    .LoadAsync();

            if (test.Questions != null)
            {
                if (questionType.HasValue)
                {
                    test.Questions = test.Questions.Where(question => question.QuestionType == questionType).ToList();
                }
                foreach (var question in test.Questions)
                {
                    await unitOfWork.Entry(question)
                        .Collection(q => q.Answers)
                        .LoadAsync();
                }
            }

            return mapper.Map<TestGetVM>(test);
        }
        public async Task<IEnumerable<TestGetVM>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var tests = await unitOfWork.Tests.GetAll(pageNumber, pageSize);
            return mapper.Map<List<TestGetVM>>(tests);
        }
        public async Task<IEnumerable<TestGetVM>> GetAllIncludeCategory()
        {
            var tests = await unitOfWork.Tests.GetAllIncludeCategory();
            return mapper.Map<List<TestGetVM>>(tests);
        }
        public async Task<IEnumerable<TestGetVM>> GetAllByCategory(int categoryId)
        {
            var tests = await unitOfWork.Tests.GetAllByCategory(categoryId);
            return mapper.Map<List<TestGetVM>>(tests);
        }
    }
}
