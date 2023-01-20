using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IUserService
    {
        Task<int> Add(UserAddVM User);
        Task<int> Update(UserUpdateVM user);
        Task<int> Remove(int key);
        Task<User> GetById(int key);
        Task<IEnumerable<User>> GetAll(string? search, int pageNumber, int pageSize);
        Task<object> Login(UserLoginVM user);
        Task<int> Register(UserRegisterVM user);
    }
}
