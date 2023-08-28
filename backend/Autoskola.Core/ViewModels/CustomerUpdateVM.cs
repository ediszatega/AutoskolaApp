using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class CustomerUpdateVM : UserUpdateVM
    {
        public bool IsActive { get; set; }
    }
}
