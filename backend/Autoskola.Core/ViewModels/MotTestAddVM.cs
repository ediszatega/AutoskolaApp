using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class MotTestAddVM
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Mileage { get; set; }
        public int VehicleId { get; set; }
    }
}
