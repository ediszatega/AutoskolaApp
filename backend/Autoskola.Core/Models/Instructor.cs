using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Instructor : Employee
    {
        public string? DrivingLicense { get; set; }
        public Instructor()
        {
            Role = Role.Instructor;
            IsActive = true;
        }
    }
}
