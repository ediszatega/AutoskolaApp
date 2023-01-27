using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
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
    }
}
