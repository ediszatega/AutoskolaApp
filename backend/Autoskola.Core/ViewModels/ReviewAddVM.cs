using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class ReviewAddVM
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public float Score { get; set; }
        public string? Comment { get; set; }
    }
}
