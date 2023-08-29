using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Microsoft.AspNetCore.Http;
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
        Task<UserGetVM> GetById(int key);
        Task<IEnumerable<UserGetVM>> GetAll(string? search, int pageNumber, int pageSize);
        Task<IEnumerable<UserGetVM>> GetAllIncludeCities(string? search, int pageNumber, int pageSize);

        Task<object> Login(UserLoginVM user);
        Task<int> Register(UserRegisterVM user);
        Task<int> UploadProfileImage(int userId, IFormFile? file);
    }
}
