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
    public interface INewsService
    {
        Task<NewsGetVM> Add(NewsAddVM news);
        Task<int> Update(NewsUpdateVM entity);
        Task<int> Remove(int key);
        Task<NewsGetVM> GetById(int key);
        Task<IEnumerable<NewsGetVM>> GetAll(int pageNumber, int pageSize);
        Task<int> UploadImage(int id, IFormFile image, string root);
    }
}

