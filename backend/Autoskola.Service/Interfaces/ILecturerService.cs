using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ILecturerService
    {
        Task<int> Add(LecturerAddVM lecturer);
        Task<int> Update(LecturerUpdateVM lecturer);
        Task<int> Remove(int key);
        Task<LecturerGetVM> GetById(int key);
        Task<IEnumerable<LecturerGetVM>> GetAll(int pageNumber, int pageSize);
    }
}
