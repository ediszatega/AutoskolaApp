﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Models
{
    public class Customer : User
    {
        public Customer()
        {
            Role = Role.Customer;
            IsActive = true;
        }
    }
}
