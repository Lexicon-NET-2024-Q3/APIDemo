using Domain.Contracts;
using Services.Contracts;

namespace Companies.Services;

public class EmployeeService : IEmployeeService
{
    private IUnitOfWork uow;

    public EmployeeService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}