using AutoMapper;
using FluentValidation.Results;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeModel, EmployeeDto>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                .ReverseMap();
            CreateMap<EmployeeModel, EditEmployeeDto>()
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                    .ReverseMap();
            CreateMap<EmployeeModel, CreateEmployeeDto>()
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                    .ReverseMap();
            CreateMap<ValidationFailure, ErrorResponse>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.PropertyName));
        }
    }
}
