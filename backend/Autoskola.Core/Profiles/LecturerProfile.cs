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
    public class LecturerProfile : Profile
    {
        public LecturerProfile()
        {
            CreateMap<Lecturer, LecturerAddVM>();
            CreateMap<Lecturer, LecturerUpdateVM>();
            CreateMap<Lecturer, LecturerGetVM>();

        }
    }
}
