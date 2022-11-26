using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class UserService : BaseService<User, int>, IUserService
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository repository, ICityRepository cityRepository, IMapper mapper) : base(repository)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public User Add(UserAddVM user)
        {
            var city = cityRepository.GetById(user.CityId);
            if (city == null)
                throw new HttpRequestException("Invalid CityId", null, HttpStatusCode.BadRequest);
            User newUser = mapper.Map<UserAddVM, User>(user);
            repository.Add(newUser);
            return newUser;
        }

        public User Update(UserUpdateVM user)
        {
            var city = cityRepository.GetById(user.CityId);
            if (city == null)
                throw new HttpRequestException("Invalid CityId", null, HttpStatusCode.BadRequest);
            User newUser = mapper.Map<UserUpdateVM, User>(user);
            repository.Update(newUser);
            return newUser;
        }

        public IEnumerable<User> GetAll(string? search, int page, int pageSize)
        {
            return repository.GetAll(page, pageSize).Where(s => string.IsNullOrEmpty(search) || string.Concat(s.FirstName, " ", s.LastName).StartsWith(search));
        }
    }
}
