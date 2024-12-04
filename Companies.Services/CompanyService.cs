using AutoMapper;
using Companies.API.DTOs;
using Domain.Contracts;
using Services.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public CompanyService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(bool includeEmployees, bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<CompanyDto>>(await uow.CompanyRepository.GetCompaniesAsync(includeEmployees, trackChanges));
    }
}