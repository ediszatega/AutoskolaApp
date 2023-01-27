using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class MotTestService : IMotTestService
    {
        private readonly IUnitOfWork unitOfWork;
        public MotTestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<MotTest> GetById(int key)
        {
            return await unitOfWork.MotTests.Get(key);
        }

        public async Task<IEnumerable<MotTest>> GetMotTests(int vehicleId, string? search, int pageNumber, int pageSize)
        {
            return await unitOfWork.MotTests.GetMotTests(vehicleId, search, pageNumber, pageSize);
        }

        public async Task<int> Add(MotTestAddVM mottest)
        {
            var newMotTest = new MotTest()
            {
                Description = mottest.Description,
                Date = mottest.Date,
                Mileage = mottest.Mileage,
                VehicleId = mottest.VehicleId,
            };
            await unitOfWork.MotTests.Add(newMotTest);
            return await unitOfWork.Complete();
        }
        
        public async Task<int> Update(MotTestUpdateVM entity)
        {
            var motTest = await unitOfWork.MotTests.Get(entity.Id);
            if(motTest == null)
            {
                throw new HttpException("MotTest with requested ID not found", 400);
            }
            motTest.Description = entity.Description;
            motTest.Date = entity.Date;
            motTest.Mileage = entity.Mileage;
            motTest.VehicleId = entity.VehicleId;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var motTest = unitOfWork.MotTests.Get(key).Result;
            if(motTest == null)
            {
                throw new HttpException("MotTest with requested ID not found", 400);
            }
            unitOfWork.MotTests.Remove(motTest);
            return await unitOfWork.Complete();
        }
    }
}
