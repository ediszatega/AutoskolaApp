using Autoskola.Core.Models;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(AutoskolaContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<News>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            return await AutoskolaContext.News.Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(news=>news.User).ToListAsync();
        }
        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }

    }
}
