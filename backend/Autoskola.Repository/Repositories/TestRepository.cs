using Autoskola.Core.Models;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
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

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
