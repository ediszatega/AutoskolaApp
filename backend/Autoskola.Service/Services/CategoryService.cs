using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class CategoryService : BaseService<Category, int>, ICategoryService
    {
        public CategoryService(IRepository<Category, int> repository) : base(repository)
        {
        }

        public Category Add(CategoryAddVM Category)
        {
            Category newCategory = new Category() { Name = Category.Name, Description = Category.Description};
            repository.Add(newCategory);
            return newCategory;
        }
    }
}
