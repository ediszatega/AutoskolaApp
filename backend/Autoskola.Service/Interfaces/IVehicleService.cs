using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehicles(string? search, int page, int pageSize);
        Task<Vehicle> GetById(int key);
        Task<int> Add(VehicleAddVM vehicle);
        Task<int> Update(VehicleUpdateVM entity);
        Task<int> Remove(int key);
    }
}
