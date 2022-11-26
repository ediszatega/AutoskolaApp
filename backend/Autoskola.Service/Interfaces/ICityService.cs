using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ICityService : IBaseService<City,int>
    {
        public IEnumerable<City> GetCities(string? search, int page, int pageSize);
        public City Add(CityAddVM city);
    }
}
