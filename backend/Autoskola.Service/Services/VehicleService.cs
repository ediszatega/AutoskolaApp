using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork unitOfWork;
        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Add(VehicleAddVM vehicle)
        {
            var newVehicle = new Vehicle()
            {
                Model = vehicle.Model,
                Make = vehicle.Make,
                Registration= vehicle.Registration,
                CategoryId= vehicle.CategoryId,
            };
            await unitOfWork.Vehicles.Add(newVehicle);
            return await unitOfWork.Complete();
        }

        public async Task<Vehicle> GetById(int key)
        {
            return await unitOfWork.Vehicles.Get(key);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(string? search, int page, int pageSize)
        {
            return await unitOfWork.Vehicles.GetVehicles(search, page, pageSize);
        }

        public async Task<int> Remove(int key)
        {
            var vehicle = unitOfWork.Vehicles.Get(key).Result;
            if(vehicle == null)
            {
                throw new HttpException("Vehicle with requested ID not found", 400);
            }
            unitOfWork.Vehicles.Remove(vehicle);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(VehicleUpdateVM entity)
        {
            var vehicle = await unitOfWork.Vehicles.Get(entity.Id);
            if(vehicle == null)
            {
                throw new HttpException("Vehicle with requested ID not found", 400);
            }
            vehicle.Model = entity.Model;
            vehicle.Make = entity.Make;
            vehicle.Registration = entity.Registration;
            vehicle.CategoryId = entity.CategoryId;

            return await unitOfWork.Complete();
        }
    }
}
