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
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public InstructorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<int> Add(InstructorAddVM entity)
        {
            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var existingUsername = await unitOfWork.Instructors.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Instructors.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var newInstructor = new Instructor() { 
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
            await unitOfWork.Instructors.Add(newInstructor);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var instructor = unitOfWork.Instructors.Get(key).Result;
            if (instructor == null)
                throw new HttpException("User with requested ID not found", 400);
            unitOfWork.Instructors.Remove(instructor);
            return await unitOfWork.Complete();
        }

        public async Task<InstructorGetVM> GetById(int key)
        {
            var instructor = await unitOfWork.Instructors.Get(key);
            return mapper.Map<InstructorGetVM>(instructor);
        }

        public async Task<IEnumerable<InstructorGetVM>> GetAll(int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Instructors.GetAll(pageNumber, pageSize);
            return mapper.Map<List<InstructorGetVM>>(users);
        }

        public async Task<int> Update(InstructorUpdateVM entity)
        {
            var instructor = unitOfWork.Instructors.Get(entity.Id).Result;
            if (instructor == null)
                throw new HttpException("User with requested ID not found", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Instructors.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Instructors.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            instructor.FirstName = entity.FirstName;
            instructor.LastName = entity.LastName;
            instructor.Username = entity.Username;
            instructor.Email = entity.Email;
            instructor.PhoneNumber = entity.PhoneNumber;
            instructor.DateOfBirth = entity.DateOfBirth;
            if(!string.IsNullOrEmpty(entity.Password))
                instructor.Password = PasswordHasher.HashPassword(entity.Password);
            instructor.CityId = entity.CityId;
            instructor.HireDate = entity.HireDate;
            instructor.EndDate = entity.EndDate;
            instructor.License = entity.License;


            return await unitOfWork.Complete();
        }
    }
}
