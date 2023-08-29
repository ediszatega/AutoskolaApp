using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<int> Add(CustomerAddVM customer);
        Task<int> Update(CustomerUpdateVM customer);
        Task<int> Remove(int key);
        Task<CustomerGetVM> GetById(int key);
        Task<IEnumerable<CustomerGetVM>> GetAll(int pageNumber, int pageSize);
    }
}
