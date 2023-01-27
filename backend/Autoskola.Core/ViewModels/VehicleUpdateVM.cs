using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class VehicleUpdateVM
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Make { get; set; }
        public string? Registration { get; set; }
        public int CategoryId { get; set; }
    }
}
