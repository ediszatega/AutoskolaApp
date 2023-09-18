using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class VehiclesRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehiclesRepository(AutoskolaContext context) : base(context)
        {
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }

        public async Task<IEnumerable<Vehicle>> GetVehicles(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await _context.Set<Vehicle>().Where(vehicle => string.IsNullOrEmpty(search) || vehicle.Model.StartsWith(search)).Include(vehicle => vehicle.Category).OrderBy(v => v.Model)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }
    }
}
