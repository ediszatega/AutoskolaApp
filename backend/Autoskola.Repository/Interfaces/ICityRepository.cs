using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface ICityRepository : IRepository<City, int>
    {
        public IEnumerable<City> GetCities(string? search, int page, int pageSize);
    }
}
