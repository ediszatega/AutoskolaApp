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
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AutoskolaContext context) : base(context)
        {

        }

        public async Task<IEnumerable<City>> GetAll(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await AutoskolaContext.Cities.Where(city => string.IsNullOrEmpty(search) || city.Name.StartsWith(search)).OrderBy(c => c.Name)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
