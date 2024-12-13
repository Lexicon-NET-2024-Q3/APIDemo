using Companies.API.DTOs;
using Companies.Shared.Request;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Text.Json;

namespace Companies.Presemtation.Controllers;

[Route("api/simple")]
[ApiController]
[Authorize]
public class SimpleController : ControllerBase
{


    public SimpleController()
    {

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
    { 
        
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return Ok("is auth");
        }
        else
        {
            return BadRequest("is not auth");
        }
    }
}