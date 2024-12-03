using Companies.Infrastructure.Data;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Infrastructure.Repositories;
public class EmployeeRepository : RepositoryBase<Employee>
{
    public EmployeeRepository(CompaniesContext context) :base(context){}

}
