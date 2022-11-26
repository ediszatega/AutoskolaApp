using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IUserService : IBaseService<User, int>
    {
        public IEnumerable<User> GetAll(string? search, int page, int pageSize);
        public User Add(UserAddVM user);
        public User Update(UserUpdateVM user);
    }
}
