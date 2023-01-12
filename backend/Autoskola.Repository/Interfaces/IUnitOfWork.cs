using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        ITestRepository Tests { get; }

        Task<int> Complete();
    }
}
