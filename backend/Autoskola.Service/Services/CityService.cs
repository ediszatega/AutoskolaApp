using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class CityService : BaseService<City, int>, ICityService
    {
        private readonly IMapper mapper;
        public CityService(ICityRepository repository, IMapper mapper)  : base(repository)
        {
            this.mapper = mapper;
        }

        public IEnumerable<City> GetCities(string? search, int page, int pageSize)
        {
            //return repository.GetCities(search, page, pageSize);
            return repository.GetAll(page, pageSize).Where(c => string.IsNullOrWhiteSpace(search) || c.Name.StartsWith(search));
        }

        public City Add(CityAddVM city)
        {
            City newCity = new City(){ Name = city.Name, PostalCode=city.PostalCode};
            repository.Add(newCity);
            return newCity;
        }
    }
}
