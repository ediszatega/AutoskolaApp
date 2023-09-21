using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
