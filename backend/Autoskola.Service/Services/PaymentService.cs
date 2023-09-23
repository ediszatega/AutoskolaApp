using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Payment>> GetPayments(string? search, int pageNumber, int pageSize)
        {
            return await unitOfWork.Payments.GetPayments(search, pageNumber, pageSize);
        }

        public async Task<Payment> GetById(int key)
        {
            return await unitOfWork.Payments.GetById(key);
        }

        public async Task<int> Add(PaymentAddVM payment)
        {
            var newPayment = new Payment()
            {
                Amount= payment.Amount,
                Description= payment.Description,
                Date = payment.Date,
                CustomerId= payment.CustomerId,
            };
            await unitOfWork.Payments.Add(newPayment);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(PaymentUpdateVM entity)
        {
            var payment = await unitOfWork.Payments.Get(entity.Id);

            if(payment == null)
            {
                throw new HttpException("Payment with requested ID not found", 400);
            }

            payment.Amount = entity.Amount;
            payment.Description = entity.Description;
            payment.Date = entity.Date;
            payment.CustomerId = entity.CustomerId;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var payment = unitOfWork.Payments.Get(key).Result;

            if(payment == null)
            {
                throw new HttpException("Payment with requested ID not found", 400);
            }

            unitOfWork.Payments.Remove(payment);
            return await unitOfWork.Complete();
        }
    }
}
