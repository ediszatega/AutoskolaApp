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

        public async Task<IEnumerable<Question>> GetQuestionsByType(QuestionType type, int testId)
        {
            return await Find(q => q.QuestionType == type && q.TestId == testId);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTest(int testId)
        {
            return await AutoskolaContext.Questions.Where(q=>q.TestId == testId).Include(q=>q.Answers).ToListAsync();
        }
        public async Task<int> GetLatestOrder(int testId)
        {
            return await AutoskolaContext.Questions.Where(q => q.TestId == testId).MaxAsync(q => (int?)q.Order) ?? 0;
        }
        public void RemoveAnswers(int questionId)
        {
            AutoskolaContext.RemoveRange(AutoskolaContext.Answers.Where(a => a.QuestionId == questionId).ToList());
        }

        public async Task UpdateAnswers(int questionId, List<Answer> answers)
        {
            var existingAnswers = await AutoskolaContext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
            AutoskolaContext.RemoveRange(existingAnswers);
            await AutoskolaContext.AddRangeAsync(answers);
        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }

    }
}
