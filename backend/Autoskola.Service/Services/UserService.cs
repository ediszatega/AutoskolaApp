using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Helpers;
using Autoskola.Service.Interfaces;
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

        public async Task<object> Login(UserLoginVM userLogin)
        {
            if (userLogin == null)
                throw new HttpException("Bad request", 400);
            var user = await unitOfWork.Users
                .SingleOrDefault(u=>u.Username== userLogin.Username);
            if(user==null)
                throw new HttpException("Invalid username or password", 404);
            bool validPassword = PasswordHasher.VerifyPassword(userLogin.Password, user.Password);
            if (!validPassword)
                throw new HttpException("Invalid username or password", 404);

            user.Token = JwtHelper.CreateToken(user);
            await unitOfWork.Complete();

            return new { StatusCode = 200, Message = "Login successful", Token = user.Token };
        }

        public async Task<int> Register(UserRegisterVM userRegister)
        {
            if (userRegister == null)
                throw new HttpException("Bad request", 400);

            var existingUsername = await unitOfWork.Users.SingleOrDefault(u=>u.Username== userRegister.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Users.SingleOrDefault(u => u.Email == userRegister.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var user = new User()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Email = userRegister.Email,
                Username = userRegister.Username,
                Password = PasswordHasher.HashPassword(userRegister.Password),
                CityId = 1
            };
            await unitOfWork.Users.Add(user);
            return await unitOfWork.Complete();
        }
    }
}
