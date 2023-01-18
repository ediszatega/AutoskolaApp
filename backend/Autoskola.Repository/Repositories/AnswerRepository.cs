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
    public class AnswerRepository : Repository<Answer>, IAnswerRepository 
    {
        public AnswerRepository(AutoskolaContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Answer>>GetByQuestion(int questionId)
        {
            return await _context.Set<Answer>().Where(a => a.QuestionId == questionId).OrderBy(a=>a.Id).Take(4).ToListAsync();
        }
        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
