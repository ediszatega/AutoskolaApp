using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class InstructorAddVM : EmployeeAddVM
    {
        public string? DrivingLicense { get; set; }
    }
}
