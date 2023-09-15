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
            return await AutoskolaContext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        public async void RemoveByQuestion(int questionId)
        {
            var answers = await AutoskolaContext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
            _context.Set<Answer>().RemoveRange(answers);
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
