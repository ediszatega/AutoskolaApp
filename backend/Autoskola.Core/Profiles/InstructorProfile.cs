﻿using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Profiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorAddVM>();
            CreateMap<Instructor, InstructorUpdateVM>();
            CreateMap<Instructor, InstructorGetVM>();

        }
    }
}
