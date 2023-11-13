using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AutoskolaContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Employee>> GetAllIncludeCities(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {

                return await AutoskolaContext.Employees.Where(user => string.IsNullOrEmpty(search) ||
                (user.FirstName + " " + user.LastName).Contains(search)).Where(user => user.IsActive).Include(user => user.City)
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
