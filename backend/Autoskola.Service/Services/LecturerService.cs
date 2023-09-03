using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Helpers;
using Autoskola.Service.Interfaces;
using Azure.Core;
using Castle.Core.Resource;
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
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public LecturerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<int> Add(LecturerAddVM entity)
        {
            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var existingUsername = await unitOfWork.Lecturers.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Lecturers.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var newLecturer = new Lecturer() { 
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                DateOfBirth= entity.DateOfBirth,
                Username = entity.Username,
                Password = PasswordHasher.HashPassword(entity.Password),
                CityId = entity.CityId,
                HireDate = entity.HireDate,
                EndDate = entity.EndDate,
                License = entity.License
            };
            await unitOfWork.Lecturers.Add(newLecturer);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var lecturer = unitOfWork.Lecturers.Get(key).Result;
            if (lecturer == null)
                throw new HttpException("User with requested ID not found", 400);
            unitOfWork.Lecturers.Remove(lecturer);
            return await unitOfWork.Complete();
        }

        public async Task<int> Deactivate(int key)
        {
            var user = unitOfWork.Users.Get(key).Result;
            if (user == null)
                throw new HttpException("User with requested ID not found", 400);
            user.IsActive = false;
            return await unitOfWork.Complete();
        }

        public async Task<LecturerGetVM> GetById(int key)
        {
            var lecturer = await unitOfWork.Lecturers.Get(key);
            return mapper.Map<LecturerGetVM>(lecturer);
        }

        public async Task<IEnumerable<LecturerGetVM>> GetAll(int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Lecturers.GetAll(pageNumber, pageSize);
            return mapper.Map<List<LecturerGetVM>>(users);
        }

        public async Task<int> Update(LecturerUpdateVM entity)
        {
            var lecturer = unitOfWork.Lecturers.Get(entity.Id).Result;
            if (lecturer == null)
                throw new HttpException("User with requested ID not found", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Lecturers.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Lecturers.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            lecturer.FirstName = entity.FirstName;
            lecturer.LastName = entity.LastName;
            lecturer.Username = entity.Username;
            lecturer.Email = entity.Email;
            lecturer.PhoneNumber = entity.PhoneNumber;
            lecturer.DateOfBirth = entity.DateOfBirth;
            if(!string.IsNullOrEmpty(entity.Password))
                lecturer.Password = PasswordHasher.HashPassword(entity.Password);
            lecturer.CityId = entity.CityId;
            lecturer.HireDate = entity.HireDate;
            lecturer.EndDate = entity.EndDate;
            lecturer.License = entity.License;


            return await unitOfWork.Complete();
        }
    }
}
