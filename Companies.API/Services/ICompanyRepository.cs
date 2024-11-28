using Companies.API.Entities;

namespace Companies.API.Services;
public interface ICompanyRepository
{
    void Add(Company company);
    void Delete(Company company);
    Task<IEnumerable<Company>> GetCompanies(bool includeEmployees = false);
    Task<Company?> GetCompanyAsync(int id);
}