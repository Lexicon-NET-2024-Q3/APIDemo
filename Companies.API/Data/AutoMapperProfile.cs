using AutoMapper;
using Companies.API.DTOs;
using Companies.API.Entities;

namespace Companies.API.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Company, CompanyDto>();
    }
}
