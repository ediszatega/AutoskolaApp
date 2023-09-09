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
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.Web.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Autoskola.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Question>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            return await unitOfWork.Questions.GetAll(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByType(QuestionType type, int testId)
        {
            var test = await unitOfWork.Tests.Get(testId);
            if (test == null)
                throw new HttpException("Invalid Test ID", 400);
            return await unitOfWork.Questions.GetQuestionsByType(type, testId);
        }
        public async Task<IEnumerable<QuestionGetVM>> GetQuestionsByTest(int testId)
        {
            var test = await unitOfWork.Tests.Get(testId);
            if (test == null)
                throw new HttpException("Invalid Test ID", 400);
            var questions = await unitOfWork.Questions.GetQuestionsByTest(testId);
            return mapper.Map<QuestionGetVM[]>(questions);
        }

        public async Task<QuestionGetVM> Add(QuestionAddVM question)
        {
            var test = await unitOfWork.Tests.Get(question.TestId);
            if (test == null)
                throw new HttpException("Invalid Test ID", 400);

            var latestOrder = await unitOfWork.Questions.GetLatestOrder(question.TestId);
            var newQuestion = new Question() { 
                Text = question.Text, 
                Points = question.Points,
                QuestionType = question.QuestionType,
                TestId = question.TestId,
                Order = latestOrder+1 
            };
            var addedQuestion = await unitOfWork.Questions.Add(newQuestion);
            await unitOfWork.Complete();
            return mapper.Map<QuestionGetVM>(addedQuestion);
        }
        public async Task<int> AddAnswerToQuestion(int quesitonId, AnswerAddVM answer)
        {
            var question = await unitOfWork.Questions.Get(quesitonId);
            if (question == null)
                throw new HttpException("Invalid Question ID", 400);
            var newAnswer = new Answer
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                QuestionId = quesitonId
            };
            await unitOfWork.Answers.Add(newAnswer);
            return await unitOfWork.Complete();
        }

        public async Task<int> AddAnswersToQuestion(int questionId, AnswerAddVM[] answers)
        {
            var question = await unitOfWork.Questions.Get(questionId);
            if (question == null)
                throw new HttpException("Invalid Question ID", 400);

            List<Answer> newAnswers = new List<Answer>();
            foreach (var answer in answers)
            {
                var newAnswer = new Answer
                {
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect,
                    QuestionId = questionId
                };
                newAnswers.Add(newAnswer);
            }
            await unitOfWork.Questions.UpdateAnswers(questionId, newAnswers);
            return await unitOfWork.Complete();
        }

        public async Task<int> Update(QuestionUpdateVM entity)
        {
            var question = await unitOfWork.Questions.Get(entity.Id);
            if (question == null)
                throw new HttpException("Question with requested ID not found", 400);
            question.Text = entity.Text;
            question.Points = entity.Points;
            question.QuestionType = entity.QuestionType;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var question = unitOfWork.Questions.Get(key).Result;
            if (question == null)
                throw new HttpException("Question with requested ID not found", 400);
            unitOfWork.Questions.RemoveAnswers(key);
            unitOfWork.Questions.Remove(question);
            return await unitOfWork.Complete();
        }

        public async Task<Question> GetById(int key)
        {
            return await unitOfWork.Questions.Get(key);
        }

        public async Task<int> UploadImage(int id, IFormFile image, string root)
        {
            var question = await unitOfWork.Questions.Get(id);
            if (question == null)
                throw new HttpException("Invalid Question ID", 400);
            if (System.IO.Path.GetExtension(image.FileName).ToLower() != ".png")
                throw new HttpException("Invalid file type", 400);
            var imagepath = root + $"/Images/Questions/image{id}.png";
            if (System.IO.File.Exists(imagepath))
                System.IO.File.Delete(imagepath);
            using(FileStream stream = System.IO.File.Create(imagepath))
            {
                await image.CopyToAsync(stream);
            }
            question.Image = "https://localhost:7099"+ $"/Images/Questions/image{id}.png";
            return await unitOfWork.Complete();
        }
    }
}
