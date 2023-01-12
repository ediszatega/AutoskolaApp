using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoskolaContext _context;
        public UnitOfWork(AutoskolaContext context, ICityRepository cities, ICategoryRepository categories, IUserRepository users, ITestRepository tests)
        {
            _context = context;
            Cities = cities;
            Categories = categories;
            Users = users;
            Tests = tests;
        }

        public ICityRepository Cities { get; set; }
        public ICategoryRepository Categories { get; set; }
        public IUserRepository Users { get; set; }
        public ITestRepository Tests { get; set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
