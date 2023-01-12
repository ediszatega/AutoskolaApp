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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AutoskolaContext context) : base(context)
        {

        }

        public AutoskolaContext AutoskolaContext { get { return _context as AutoskolaContext; } }
    }
}
