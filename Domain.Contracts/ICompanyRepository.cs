

using Domain.Models.Entities;

namespace Domain.Contracts;
public interface ICompanyRepository
{
    void Create(Company company);
    void Delete(Company company);
    Task<IEnumerable<Company>> GetCompaniesAsync(bool includeEmployees = false, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(int id, bool trackChanges = false);
}