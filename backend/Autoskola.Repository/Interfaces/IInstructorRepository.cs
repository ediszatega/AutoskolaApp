﻿using Autoskola.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Task<IEnumerable<Instructor>> GetAllIncludeCities(string? search, int pageNumber, int pageSize);
    }
}
