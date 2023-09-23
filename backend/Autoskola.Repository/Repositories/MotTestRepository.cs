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
    public class MotTestRepository : Repository<MotTest>, IMotTestRepository
    {
        public MotTestRepository(AutoskolaContext context) : base(context)
        {
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
        public async Task<IEnumerable<MotTest>> GetMotTests(int vehicleId, string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await AutoskolaContext.MotTests.Where(mottest => string.IsNullOrEmpty(search) || mottest.VehicleId == vehicleId).Include(mottest => mottest.Vehicle)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }
    }
}
