using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPayments(string? search, int pageNumber, int pageSize);
        Task<Payment> GetById(int key);
        Task<int> Add(PaymentAddVM payment);
        Task<int> Update(PaymentUpdateVM payment);
        Task<int> Remove(int key);
    }
}
