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
        Task<int> Add(TestAddVM Test);
        Task<int> Remove(int key);
        Task<Test> GetById(int key);
        Task<IEnumerable<Test>> GetAll(int page, int pageSize);
}
