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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<int> Add(EmployeeAddVM entity)
        {
            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var existingUsername = await unitOfWork.Employees.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Employees.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var newEmployee = new Employee() { 
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
            await unitOfWork.Employees.Add(newEmployee);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var employee = unitOfWork.Employees.Get(key).Result;
            if (employee == null)
                throw new HttpException("User with requested ID not found", 400);
            unitOfWork.Employees.Remove(employee);
            return await unitOfWork.Complete();
        }

        public async Task<EmployeeGetVM> GetById(int key)
        {
            var employee = await unitOfWork.Employees.Get(key);
            return mapper.Map<EmployeeGetVM>(employee);
        }

        public async Task<IEnumerable<EmployeeGetVM>> GetAll(int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Employees.GetAll(pageNumber, pageSize);
            return mapper.Map<List<EmployeeGetVM>>(users);
        }

        public async Task<int> Update(EmployeeUpdateVM entity)
        {
            var employee = unitOfWork.Employees.Get(entity.Id).Result;
            if (employee == null)
                throw new HttpException("User with requested ID not found", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Employees.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Employees.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            employee.FirstName = entity.FirstName;
            employee.LastName = entity.LastName;
            employee.Username = entity.Username;
            employee.Email = entity.Email;
            employee.PhoneNumber = entity.PhoneNumber;
            employee.DateOfBirth = entity.DateOfBirth;
            if(!string.IsNullOrEmpty(entity.Password))
                employee.Password = PasswordHasher.HashPassword(entity.Password);
            employee.CityId = entity.CityId;
            employee.HireDate = entity.HireDate;
            employee.EndDate = entity.EndDate;
            employee.License = entity.License;


            return await unitOfWork.Complete();
        }
    }
}
