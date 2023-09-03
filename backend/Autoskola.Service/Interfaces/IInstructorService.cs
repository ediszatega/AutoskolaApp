using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IInstructorService
    {
        Task<int> Add(InstructorAddVM instructor);
        Task<int> Update(InstructorUpdateVM instructor);
        Task<int> Remove(int key);
        Task<int> Deactivate(int key);
        Task<InstructorGetVM> GetById(int key);
        Task<IEnumerable<InstructorGetVM>> GetAll(int pageNumber, int pageSize);
        Task<IEnumerable<InstructorGetVM>> GetAllIncludeCities(string? search, int pageNumber, int pageSize);
    }
}
