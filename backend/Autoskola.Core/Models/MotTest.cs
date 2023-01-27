using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class MotTest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Mileage { get; set; }
        [ForeignKey(nameof(Vehicle))]
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
