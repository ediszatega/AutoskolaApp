﻿using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.ViewModels
{
    public class LecturerAddVM : EmployeeAddVM
    {
        public string? Degree { get; set; }
    }
}
