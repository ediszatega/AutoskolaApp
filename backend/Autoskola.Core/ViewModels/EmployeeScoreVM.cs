using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class EmployeeScoreVM
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual City? City { get; set; }
        public string? ProfileImage { get; set; }
        public float Score { get; set; }
        public string? Role { get; set; }
    }
}
