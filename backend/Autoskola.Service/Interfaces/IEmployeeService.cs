using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> Add(EmployeeAddVM employee);
        Task<int> Update(EmployeeUpdateVM employee);
        Task<int> Remove(int key);
        Task<int> Deactivate(int key);

        Task<EmployeeGetVM> GetById(int key);
        Task<IEnumerable<EmployeeGetVM>> GetAll(int pageNumber, int pageSize);
        Task<IEnumerable<EmployeeScoreVM>> GetAllWithScore();
    }
}
