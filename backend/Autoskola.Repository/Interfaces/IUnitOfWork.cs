﻿using System;
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
<<<<<<< Updated upstream

=======
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
>>>>>>> Stashed changes
        Task<int> Complete();
    }
}
