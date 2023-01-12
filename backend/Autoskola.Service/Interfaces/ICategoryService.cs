using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<int> Add(CategoryAddVM category);
        Task<int> Update(Category entity);
        Task<int> Remove(int key);
        Task<Category> GetById(int key);
        Task<IEnumerable<Category>> GetAll(int pageNumber, int pageSize);
    }
}
