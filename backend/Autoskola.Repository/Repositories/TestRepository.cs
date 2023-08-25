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
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(AutoskolaContext context) : base(context)
        {

        }
        public async Task<Test> GetIncludeCategory(int key)
        {
            try
            {
                return await _entities.Include(test => test.Category).FirstOrDefaultAsync(test => test.Id == key);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }
        
        public async Task<IEnumerable<Test>> GetAllByCategory(int categoryId)
        {

            return await _entities.Include(t => t.Category)
                .Where(test => test.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
