using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Services;
public class ServiceManager
{
    private readonly Lazy<CompanyService> companyService;
    private readonly Lazy<EmployeeService> employeeService;

    public CompanyService CompanyService => companyService.Value;
    public EmployeeService EmployeeService => employeeService.Value;

    public ServiceManager(IUnitOfWork uow)
    {
        ArgumentNullException.ThrowIfNull(nameof(uow));

        companyService = new Lazy<CompanyService>(() => new CompanyService(uow));
        employeeService = new Lazy<EmployeeService>(() => new EmployeeService(uow));
    }
}
