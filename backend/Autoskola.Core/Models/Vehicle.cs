using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Registration { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
