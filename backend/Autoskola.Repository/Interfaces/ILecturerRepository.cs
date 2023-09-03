using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface ILecturerRepository : IRepository<Lecturer>
    {
        Task<IEnumerable<Lecturer>> GetAllIncludeCities(string? search, int pageNumber, int pageSize);
    }
}
