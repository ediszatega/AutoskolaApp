using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.Identity.Client.Utils.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<int> Add(TestAddVM test)
        {
            var category = await unitOfWork.Categories.Get(test.CategoryId);
            if (category == null)
                throw new HttpException("Invalid Category ID", 400);
            var newTest = new Test() { Description = test.Description, CategoryId = test.CategoryId };
            unitOfWork.Tests.Add(newTest);
            return await unitOfWork.Complete();
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

        public async Task<Test> GetById(int key)
        {
            return await unitOfWork.Tests.Get(key);
        }

        public async Task<IEnumerable<Test>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            return await unitOfWork.Tests.GetAll(pageNumber, pageSize);
        }
    }
}
