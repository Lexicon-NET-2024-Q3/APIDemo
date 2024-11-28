using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using Companies.API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Companies.Shared.DTOs;
using Companies.API.Services;

namespace Companies.API.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        //private readonly CompaniesContext _context;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository companyRepo;

        public CompaniesController(CompaniesContext context, IMapper mapper, ICompanyRepository companyRepo)
        {
           // _context = context;
            _mapper = mapper;
            this.companyRepo = companyRepo;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany(bool includeEmployees)
        {
            //var companies = await _context.Company.ToListAsync();
            //var dto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            //var companies = includeEmployees ? await _context.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync() :
            //                                   await _context.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync(); 

            //var companies = includeEmployees ? await GetCompanies(true) :
                                              // await GetCompanies();

            var companies = includeEmployees ? _mapper.Map<IEnumerable<CompanyDto>>(await companyRepo.GetCompanies(true) ) :
                                               _mapper.Map<IEnumerable<CompanyDto>>(await companyRepo.GetCompanies());

            return Ok(companies);
        }

      

        // GET: api/Companies/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            Company? company = await companyRepo.GetCompanyAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<CompanyDto>(company);

            return Ok(dto);
        }

      

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var existingCompany = await companyRepo.GetCompanyAsync(id);
            if(existingCompany == null) return NotFound();

            _mapper.Map(dto, existingCompany);
            await _context.SaveChangesAsync();

           // return NoContent();
           return Ok(_mapper.Map<CompanyDto>(existingCompany));
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCompany(CompanyCreateDto dto)
        {
           
            var company = _mapper.Map<Company>(dto);
            companyRepo.Add(company);
            await _context.SaveChangesAsync();

            var createdCompany = _mapper.Map<CompanyDto>(company);

            return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.Id }, createdCompany);
        }

        //// DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await companyRepo.GetCompanyAsync(id);
            if (company == null) return NotFound();
            
            companyRepo.Delete(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool CompanyExists(int id)
        //{
        //    return _context.Company.Any(e => e.Id == id);
        //}
    }
}
