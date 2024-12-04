using AutoMapper;
using Domain.Contracts;
using Services.Contracts;

namespace Companies.Services;

public class EmployeeService : IEmployeeService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public EmployeeService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }
}