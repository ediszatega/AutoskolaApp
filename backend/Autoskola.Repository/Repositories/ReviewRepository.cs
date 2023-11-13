using Autoskola.Core.Models;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AutoskolaContext context) : base(context)
        {
  
        }

        public async Task<IEnumerable<Review>> GetByEmployee(int id)
        {
            return await _entities.Where(review => review.EmployeeId == id).ToListAsync();
        }
    }
}
