using AutoMapper;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.Models;
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
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReviewGetVM>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var reviews = await unitOfWork.Reviews.GetAll(pageNumber, pageSize);
            return mapper.Map<ReviewGetVM[]>(reviews);
        }

        public async Task<int> Add(ReviewAddVM review)
        {
            var newReview = new Review() { 
                CustomerId = review.CustomerId,
                EmployeeId= review.EmployeeId,
                Score = review.Score,
                Comment = review.Comment,
            };
            await unitOfWork.Reviews.Add(newReview);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(ReviewUpdateVM entity)
        {
            var review = await unitOfWork.Reviews.Get(entity.Id);
            if (review == null)
                throw new HttpException("Review with requested ID not found", 400);
            review.Score = entity.Score;
            review.Comment = entity.Comment;
            review.CustomerId = entity.CustomerId;
            review.EmployeeId = entity.EmployeeId;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var review = unitOfWork.Reviews.Get(key).Result;
            if (review == null)
                throw new HttpException("Review with requested ID not found", 400);
            unitOfWork.Reviews.Remove(review);
            return await unitOfWork.Complete();
        }

        public async Task<ReviewGetVM> GetById(int key)
        {
            var review = await unitOfWork.Reviews.Get(key);
            return mapper.Map<ReviewGetVM>(review);
        }
    }
}
