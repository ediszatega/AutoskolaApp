using Autoskola.Core.Models;
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
    }
}
