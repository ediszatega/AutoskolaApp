using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Autoskola.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            return await unitOfWork.Categories.GetAll(pageNumber, pageSize);
        }

        public async Task<int> Add(CategoryAddVM category)
        {
            var newCategory = new Category() { Name = category.Name, Description = category.Description};
            await unitOfWork.Categories.Add(newCategory);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(Category entity)
        {
            var category = await unitOfWork.Categories.Get(entity.Id);
            if (category == null)
                throw new HttpException("Category with requested ID not found", 400);
            category.Name = entity.Name;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var category = unitOfWork.Categories.Get(key).Result;
            if (category == null)
                throw new HttpException("Category with requested ID not found", 400);
            unitOfWork.Categories.Remove(category);
            return await unitOfWork.Complete();
        }

        public async Task<Category> GetById(int key)
        {
            return await unitOfWork.Categories.Get(key);
        }
    }
}
