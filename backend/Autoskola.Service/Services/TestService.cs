using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class TestService : BaseService<Test, int>, ITestService
    {
        public TestService(IRepository<Test, int> repository) : base(repository)
        {
        }

        public Test Add(TestAddVM test)
        {
            Test newTest;
            try
            {
                newTest = new Test() { Description = test.Description, CategoryId = test.CategoryId };
                return repository.Add(newTest);
            }
            catch
            {
                throw new Exception("Invalid City ID");
            }
        }
    }
}
