using Companies.Infrastructure.Data;
using Domain.Contracts;

namespace Companies.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CompaniesContext context;
    private readonly Lazy<ICompanyRepository> companyRepository;
    private readonly Lazy<IEmployeeRepository> employeeRepository;

    public ICompanyRepository CompanyRepository => companyRepository.Value;
    public IEmployeeRepository EmployeeRepository => employeeRepository.Value;

    //Add More Repos

    public UnitOfWork(CompaniesContext context)
    {
        this.context = context;
        companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
        employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
    }

    public async Task CompleteASync()
    {
        await context.SaveChangesAsync();
    }
}
