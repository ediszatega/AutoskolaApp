using AutoMapper;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork unitOfWork;
        public AnswerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Remove(int id)
        {
            var answer = unitOfWork.Answers.Get(id);
            if (answer == null)
                throw new HttpException("Invalid Answer ID", 400);
            unitOfWork.Answers.Remove(answer.Result);
            return await unitOfWork.Complete();
        }
    }
}
