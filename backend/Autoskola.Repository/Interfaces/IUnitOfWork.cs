using Autoskola.Core.Models;
using Autoskola.Repository.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        IQuestionRepository Questions { get; }
        IAnswerRepository Answers { get; }
        IVehicleRepository Vehicles { get; }
        IMotTestRepository MotTests { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IInstructorRepository Instructors { get; }
        ILecturerRepository Lecturers { get; }
        INewsRepository News { get; }
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> Complete();
    }
}
