using Autoskola.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Data
{
    public class AutoskolaContext : DbContext
    {
        public AutoskolaContext(DbContextOptions<AutoskolaContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
}
