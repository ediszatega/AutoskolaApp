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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<int> Add(CustomerAddVM entity)
        {
            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);
            var existingUsername = await unitOfWork.Customers.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Customers.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            var newCustomer = new Customer() { 
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                DateOfBirth= entity.DateOfBirth,
                Username = entity.Username,
                Password = PasswordHasher.HashPassword(entity.Password),
                CityId = entity.CityId };
            await unitOfWork.Customers.Add(newCustomer);
            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var customer = unitOfWork.Customers.Get(key).Result;
            if (customer == null)
                throw new HttpException("User with requested ID not found", 400);
            unitOfWork.Customers.Remove(customer);
            return await unitOfWork.Complete();
        }

        public async Task<CustomerGetVM> GetById(int key)
        {
            var customer = await unitOfWork.Customers.Get(key);
            return mapper.Map<CustomerGetVM>(customer);
        }

        public async Task<IEnumerable<CustomerGetVM>> GetAll(int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Customers.GetAll(pageNumber, pageSize);
            return mapper.Map<List<CustomerGetVM>>(users);
        }

        public async Task<IEnumerable<CustomerGetVM>> GetAllIncludeCities(string? search, int pageNumber, int pageSize)
        {
            var users = await unitOfWork.Customers.GetAllIncludeCities(search, pageNumber, pageSize);
            return mapper.Map<List<CustomerGetVM>>(users);
        }

        public async Task<int> Update(CustomerUpdateVM entity)
        {
            var customer = unitOfWork.Customers.Get(entity.Id).Result;
            if (customer == null)
                throw new HttpException("User with requested ID not found", 400);

            var city = await unitOfWork.Cities.Get(entity.CityId);
            if (city == null)
                throw new HttpException("Invalid city ID", 400);

            var existingUsername = await unitOfWork.Customers.SingleOrDefault(u => u.Username == entity.Username);
            if (existingUsername != null)
                throw new HttpException("Username already exists", 400);

            var existingEmail = await unitOfWork.Customers.SingleOrDefault(u => u.Email == entity.Email);
            if (existingEmail != null)
                throw new HttpException("Email already exists", 400);

            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
            customer.Username = entity.Username;
            customer.Email = entity.Email;
            customer.PhoneNumber = entity.PhoneNumber;
            customer.DateOfBirth = entity.DateOfBirth;
            if(!string.IsNullOrEmpty(entity.Password))
                customer.Password = PasswordHasher.HashPassword(entity.Password);
            customer.CityId = entity.CityId;

            return await unitOfWork.Complete();
        }
    }
}
