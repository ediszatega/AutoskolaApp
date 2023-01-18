using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> Add(CityAddVM city)
        {
            var newCity = new City(){ Name = city.Name, PostalCode=city.PostalCode};
            unitOfWork.Cities.Add(newCity);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(City entity)
        {
            var city = await unitOfWork.Cities.Get(entity.Id);
            if (city == null)
                throw new HttpException("City with requested ID not found", 400);
            city.Name = entity.Name;
            city.PostalCode = entity.PostalCode;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var city = unitOfWork.Cities.Get(key);
            if (city == null)
                throw new HttpException("City with requested ID not found", 400);
            unitOfWork.Cities.Remove(city.Result);
            return await unitOfWork.Complete();
        }

        public async Task<City> GetById(int key)
        {
            return await unitOfWork.Cities.Get(key);
        }

        public async Task<IEnumerable<City>> GetAll(string? search, int pageNumber = 1, int pageSize = 100)
        {
            return await unitOfWork.Cities.GetAll(search, pageNumber, pageSize);
        }
    }
}
