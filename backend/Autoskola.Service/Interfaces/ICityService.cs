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
    public interface ICityService
    {
        Task<int> Add(CityAddVM city);
        Task<int> Update(City entity);
        Task<int> Remove(int key);
        Task<City> GetById(int key);
        Task<IEnumerable<City>> GetAll(string? search, int page, int pageSize);
    }
}
