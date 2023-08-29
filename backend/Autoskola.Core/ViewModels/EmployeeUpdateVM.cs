using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class EmployeeUpdateVM : UserUpdateVM
    {
        public DateTime HireDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? License { get; set; }
    }
}
