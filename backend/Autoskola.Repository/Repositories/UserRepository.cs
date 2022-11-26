using Autoskola.Core.Models;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
