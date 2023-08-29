using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Lecturer : Employee
    {
        public string? Degree { get; set; }
        public Lecturer()
        {
            Role = Role.Lecturer;
            IsActive = true;
        }
    }
}
