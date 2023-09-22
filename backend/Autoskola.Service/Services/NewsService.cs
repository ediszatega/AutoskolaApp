using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NewsGetVM>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var news = await unitOfWork.News.GetAll(pageNumber, pageSize);
            return mapper.Map<NewsGetVM[]>(news);
        }

        public async Task<NewsGetVM> Add(NewsAddVM news)
        {
            var user = await unitOfWork.Users.Get(news.UserId);
            if (user == null)
                throw new HttpException("Invalid User ID", 400);

            var newNews = new News()
            {
                Title = news.Title,
                Text = news.Text,
                Date = news.Date,
                UserId = news.UserId
            };
            var addedNews = await unitOfWork.News.Add(newNews);
            await unitOfWork.Complete();
            return mapper.Map<NewsGetVM>(addedNews);
        }

        public async Task<int> Update(NewsUpdateVM entity)
        {
            var news = await unitOfWork.News.Get(entity.Id);
            if (news == null)
                throw new HttpException("News with requested ID not found", 400);
            news.Title= entity.Title;
            news.Text = entity.Text;
            news.Date = entity.Date;
            news.UserId = entity.UserId;

            return await unitOfWork.Complete();
        }

        public async Task<int> Remove(int key)
        {
            var news = unitOfWork.News.Get(key).Result;
            if (news == null)
                throw new HttpException("News with requested ID not found", 400);
            unitOfWork.News.Remove(news);
            return await unitOfWork.Complete();
        }

        public async Task<NewsGetVM> GetById(int key)
        {
            var news = await unitOfWork.News.Get(key);
            return mapper.Map<NewsGetVM>(news);
        }

        public async Task<int> UploadImage(int id, IFormFile image, string root)
        {
            var news = await unitOfWork.News.Get(id);
            if (news == null)
                throw new HttpException("Invalid News ID", 400);
            if (System.IO.Path.GetExtension(image.FileName).ToLower() != ".png")
                throw new HttpException("Invalid file type", 400);
            var imagepath = root + $"/Images/News/image{id}.png";
            if (System.IO.File.Exists(imagepath))
                System.IO.File.Delete(imagepath);
            using (FileStream stream = System.IO.File.Create(imagepath))
            {
                await image.CopyToAsync(stream);
            }
            news.Image = "https://localhost:7099" + $"/Images/News/image{id}.png";
            return await unitOfWork.Complete();
        }
    }
}
