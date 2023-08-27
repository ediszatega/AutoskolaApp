using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Helpers;
using Autoskola.Service.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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


        public async Task<int> Add(UserAddVM entity)
        {
            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var existingUsername = await unitOfWork.Users.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Users.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var newUser = new User() { 
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Username = entity.Username,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Password = PasswordHasher.HashPassword(entity.Password),
                Role = entity.Role,
                DateOfBirth= entity.DateOfBirth,
                CityId = entity.CityId };
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

        public async Task<UserGetVM> GetById(int key)
        {
            var user = await unitOfWork.Users.Get(key);
            return mapper.Map<UserGetVM>(user);
        }

        public async Task<IEnumerable<UserGetVM>> GetAll(string? search, int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Users.GetAll(search, pageNumber, pageSize);
            return mapper.Map<List<UserGetVM>>(users);
        }

        public async Task<IEnumerable<UserGetVM>> GetAllIncludeCities(string? search, int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Users.GetAllIncludeCities(search, pageNumber, pageSize);
            return mapper.Map<List<UserGetVM>>(users);
        }
        public async Task<int> Update(UserUpdateVM entity)
        {
            var user = unitOfWork.Users.Get(entity.Id).Result;
            if (user == null)
                throw new HttpException("User with requested ID not found", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Users.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Users.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Username = entity.Username;
            user.Email = entity.Email;
            if(!string.IsNullOrEmpty(entity.Password))
                user.Password = PasswordHasher.HashPassword(entity.Password);
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

        public async Task<int> Register(UserRegisterVM entity)
        {
            if (entity == null)
                throw new HttpException("Bad request", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Users.SingleOrDefault(u=>u.Username== entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Users.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var user = new User()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                DateOfBirth = entity.DateOfBirth,
                Username = entity.Username,
                Password = PasswordHasher.HashPassword(entity.Password),
                CityId = entity.CityId,
            };
            await unitOfWork.Users.Add(user);
            return await unitOfWork.Complete();
        }

        public async Task<int> UploadProfileImage(int userId, IFormFile? file)
        {

            if (file == null || file.Length == 0)
                throw new HttpException("No file uploaded", 400);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();
                var imageString = ImageHelper.ToBase64(imageData);
                
                var user = this.unitOfWork.Users.Get(userId).Result;
                user.ProfileImage = imageString;

                return await this.unitOfWork.Complete();
            }
        }
    }
}
