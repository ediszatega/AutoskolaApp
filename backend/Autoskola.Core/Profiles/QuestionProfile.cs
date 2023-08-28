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
    public class QuestionProfile : Profile
    {
        public QuestionProfile() {
            CreateMap<Question, QuestionGetVM>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
        }
    }
}
