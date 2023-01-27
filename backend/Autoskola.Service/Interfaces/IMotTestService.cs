using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
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
        Task<int> Add(MotTestAddVM vehicle);
        Task<int> Update(MotTestUpdateVM vehicle);
    }
}
