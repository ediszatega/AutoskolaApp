using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IReviewService
    {
        Task<int> Add(ReviewAddVM category);
        Task<int> Update(ReviewUpdateVM entity);
        Task<int> Remove(int key);
        Task<ReviewGetVM> GetById(int key);
        Task<IEnumerable<ReviewGetVM>> GetAll(int pageNumber, int pageSize);
    }
}
