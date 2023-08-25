using Autoskola.Core.Models;
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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AutoskolaContext context) : base(context)
        {
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }

        public async Task<IEnumerable<Question>> GetQuestionsByType(QuestionType type, int testId)
        {
            return await Find(q => q.QuestionType == type && q.TestId == testId);
        }
        
    }
}
