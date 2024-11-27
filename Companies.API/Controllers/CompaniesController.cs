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

namespace Companies.API.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompaniesContext _context;
        private readonly IMapper _mapper;

        public CompaniesController(CompaniesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            //var companies = await _context.Company.ToListAsync();
            //var dto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            var companies = await _context.Companies.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync();

            return Ok(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

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

            var existingCompany = await _context.Companies.FindAsync(id);
            if(existingCompany == null) return NotFound();

            _mapper.Map(dto, existingCompany);
            await _context.SaveChangesAsync();

           // return NoContent();
           return Ok(_mapper.Map<CompanyDto>(existingCompany));
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyCreateDto dto)
        {
           
            var company = _mapper.Map<Company>(dto);
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var createdCompany = _mapper.Map<CompanyDto>(company);

            return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.Id }, createdCompany);
        }

        //// DELETE: api/Companies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(int id)
        //{
        //    var company = await _context.Company.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Company.Remove(company);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CompanyExists(int id)
        //{
        //    return _context.Company.Any(e => e.Id == id);
        //}
    }
}
