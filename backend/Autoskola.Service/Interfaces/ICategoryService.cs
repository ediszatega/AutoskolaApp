using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ICategoryService : IBaseService<Category, int>
    {
        public Category Add(CategoryAddVM category);
    }
}
