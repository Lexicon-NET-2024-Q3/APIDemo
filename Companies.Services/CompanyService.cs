using Domain.Contracts;

namespace Companies.Services;

public class CompanyService
{
    private IUnitOfWork uow;

    public CompanyService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}