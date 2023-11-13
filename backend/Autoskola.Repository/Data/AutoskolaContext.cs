using Autoskola.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<MotTest> MotTests { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}
