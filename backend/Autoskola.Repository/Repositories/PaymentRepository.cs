using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AutoskolaContext context) : base(context)
        {

        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }

        public async Task<IEnumerable<Payment>> GetPayments(string? search, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await _context.Set<Payment>()
                    .Where(payment => string.IsNullOrEmpty(search) || payment.Description.StartsWith(search))
                    .Include(customer => customer.Customer)
                    .Include(customer => customer.Customer.City)
                    .OrderBy(payment => payment.Description)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async Task<Payment> GetById(int id)
        {
            try
            {
                return await _context.Set<Payment>()
                    .Include(payment => payment.Customer)
                    .Include(payment => payment.Customer.City)
                    .SingleOrDefaultAsync(payment => payment.Id == id);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }
    }
}
