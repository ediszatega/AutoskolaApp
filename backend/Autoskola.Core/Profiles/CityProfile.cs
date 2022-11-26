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
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityAddVM>().ForMember(dest =>
            dest.Name,
            opt => opt.MapFrom(src => src.Name))
        .ForMember(dest =>
            dest.PostalCode,
            opt => opt.MapFrom(src => src.PostalCode))
        .ReverseMap();
        }
    }
}
