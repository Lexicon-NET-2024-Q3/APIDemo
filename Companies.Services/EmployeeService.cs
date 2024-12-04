using Domain.Contracts;

namespace Companies.Services;

public class EmployeeService
{
    private IUnitOfWork uow;

    public EmployeeService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}