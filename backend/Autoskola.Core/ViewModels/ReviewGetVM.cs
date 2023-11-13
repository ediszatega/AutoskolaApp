using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class ReviewGetVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerGetVM? Customer { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeGetVM? Employee {get; set; }
        public float Score { get; set; }
        public string? Comment { get; set; }
    }
}
