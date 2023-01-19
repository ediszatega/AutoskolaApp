using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<int> Add(UserAddVM user)
        {
            var city = await unitOfWork.Cities.Get(user.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var newUser = new User() { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                CityId = user.CityId };
            await unitOfWork.Users.Add(newUser);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var user = unitOfWork.Users.Get(key).Result;
            if (user == null)
                throw new HttpException("User with requested ID not found", 400);
            unitOfWork.Users.Remove(user);
            return await unitOfWork.Complete();
        }

        public async Task<User> GetById(int key)
        {
            return await unitOfWork.Users.Get(key);
        }

        public async Task<IEnumerable<User>> GetAll(string? search, int pageNumber, int pageSize)
        {
            return await unitOfWork.Users.GetAll(search, pageNumber, pageSize);
        }

        public async Task<int> Update(UserUpdateVM entity)
        {
            var user = unitOfWork.Users.Get(entity.Id).Result;
            if (user == null)
                throw new HttpException("User with requested ID not found", 400);
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Username = entity.Username;
            user.Password = entity.Password;
            user.CityId = entity.CityId;

            return await unitOfWork.Complete();
        }

        public async Task Login(UserLoginVM userLogin)
        {
            if (userLogin == null)
                throw new HttpException("Bad request", 400);
            var user = await unitOfWork.Users
                .SingleOrDefault(u=>u.Username== userLogin.Username && u.Password== userLogin.Password);
            if (user == null)
                throw new HttpException("Login failed", 404);
        }

        public async Task<int> Register(UserRegisterVM userRegister)
        {
            if (userRegister == null)
                throw new HttpException("Bad request", 400);
            var user = new User()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Username = userRegister.Username,
                Password = userRegister.Password,
                CityId = 1
            };
            await unitOfWork.Users.Add(user);
            return await unitOfWork.Complete();
        }
    }
}
