using Domain.Contracts;
using Services.Contracts;

namespace Companies.Services;

public class CompanyService : ICompanyService
{
    private IUnitOfWork uow;

    public CompanyService(IUnitOfWork uow)
    {
        this.uow = uow;
    }
}