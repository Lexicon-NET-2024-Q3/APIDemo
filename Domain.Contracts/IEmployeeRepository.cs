using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;
public interface IEmployeeRepository
{
    void Update(Employee employee);
    void Create(Employee employee);
    void Delete(Employee employee);
    Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges = false);
    Task<Employee?> GetEmployeeAsync(int companyId, int employeeId, bool trackChanges = false);
}
