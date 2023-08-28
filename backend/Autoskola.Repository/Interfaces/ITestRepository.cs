using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<Test> GetIncludeCategory(int key);
        Task<IEnumerable<Test>> GetAllByCategory(int categoryId);
    }
}
