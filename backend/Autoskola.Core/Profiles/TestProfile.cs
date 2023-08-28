using AutoMapper;
using Autoskola.Core.Models;
using Autoskola.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Core.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile() {
            CreateMap<Test, TestGetVM>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
        }
    }
}
