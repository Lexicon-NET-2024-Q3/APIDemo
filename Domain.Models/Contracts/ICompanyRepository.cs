﻿using Domain.Models.Entities;

namespace Domain.Models.Contracts;
public interface ICompanyRepository
{
    void Update(Company company);
    void Create(Company company);
    void Delete(Company company);
    Task<IEnumerable<Company>> GetCompaniesAsync(bool includeEmployees = false, bool trackChanges = false);
    Task<Company?> GetCompanyAsync(int id, bool trackChanges = false);
    Task<bool> CompanyExistsAsync(int id);
}