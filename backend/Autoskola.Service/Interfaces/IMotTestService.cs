using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IMotTestService
    {
        Task<IEnumerable<MotTest>> GetMotTests(int vehicleId, string? search, int pageNumber, int pageSize);
        Task<MotTest> GetById(int key);
    }
}
