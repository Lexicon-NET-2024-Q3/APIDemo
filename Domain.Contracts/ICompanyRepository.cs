

using Domain.Models.Entities;

namespace Domain.Contracts;
public interface ICompanyRepository
{
    void Add(Company company);
    void Delete(Company company);
    Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false);
    Task<Company?> GetCompanyAsync(int id);
}