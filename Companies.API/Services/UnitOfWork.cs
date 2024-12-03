

using Companies.Infrastructure.Data;

namespace Companies.API.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly CompaniesContext context;

    public ICompanyRepository CompanyRepository { get; }
    //Add More Repos

    public UnitOfWork(CompaniesContext context)
    {
        this.context = context;
        CompanyRepository = new CompanyRepository(context);
    }

    public async Task CompleteASync()
    {
        await context.SaveChangesAsync();
    }
}
