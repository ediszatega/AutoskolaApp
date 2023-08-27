using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AutoskolaContext context) : base(context)
        {

        }

        public async Task<IEnumerable<User>> GetAll(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await AutoskolaContext.Users.Where(user => string.IsNullOrEmpty(search) ||
                (user.FirstName + " " + user.LastName).Contains(search))
                .OrderBy(u => u.FirstName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludeCities(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await AutoskolaContext.Users.Where(user => string.IsNullOrEmpty(search) ||
                (user.FirstName + " " + user.LastName).Contains(search)).Include(user => user.City)
                .OrderBy(u => u.FirstName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
