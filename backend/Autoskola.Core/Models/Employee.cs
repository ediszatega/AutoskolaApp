using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? License { get; set; }
        public Employee()
        {
            Role = Role.Employee;
            IsActive = true;
        }
    }
}
