using Autoskola.Core.Models;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class CityRepository : BaseRepository<City, int>, ICityRepository
    {
        public CityRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<City> GetCities(string? search, int page, int pageSize)
        {
            return dbSet.Where(c => string.IsNullOrWhiteSpace(search) || c.Name.StartsWith(search)).ToPagedList(page, pageSize);
        }
    }
}
