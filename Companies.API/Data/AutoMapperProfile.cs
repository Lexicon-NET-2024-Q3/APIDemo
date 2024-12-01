﻿using AutoMapper;
using Companies.API.DTOs;
using Companies.API.Entities;
using Companies.Shared.DTOs;

namespace Companies.API.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Address,
            opt => opt.MapFrom(src => $"{src.Address}{(string.IsNullOrEmpty(src.Country) ? string.Empty : ", ")}{src.Country}"));

        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();

        
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
    }
}
