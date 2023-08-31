using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAll(string? search, int pageNumber, int pageSize);
        Task<IEnumerable<User>> GetAllIncludeCities(string? search, int pageNumber, int pageSize);
        Task<IEnumerable<User>> GetAdmins(string? search, int pageNumber, int pageSize);


    }
}
