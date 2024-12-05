using Companies.Infrastructure.Data;
using Domain.Models.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{

    public CompanyRepository(CompaniesContext context) : base(context){}

    public async Task<Company?> GetCompanyAsync(int id, bool trackChanges = false)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(bool includeEmployees = false, bool trackChanges = false)
    {
        return includeEmployees ? await FindAll(trackChanges).Include(c => c.Employees).ToListAsync() :
                                  await FindAll(trackChanges).ToListAsync();
    }

    public async Task<bool> CompanyExistsAsync(int id)
    {
        return await Context.Companies.AnyAsync(c => c.Id == id);
    }
}
