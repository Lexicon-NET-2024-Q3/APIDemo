﻿using AutoMapper;
using Companies.Shared.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Services;
public class AuthService : IAuthService
{
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto registrationDto)
    {
        if (registrationDto is null)
        {
            throw new ArgumentNullException(nameof(registrationDto));
        }

        var roleExists = await roleManager.RoleExistsAsync(registrationDto.Role);
        if(!roleExists)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
        }

        var user = mapper.Map<ApplicationUser>(registrationDto);

        var result = await userManager.CreateAsync(user, registrationDto.Password!);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, registrationDto.Role);
        }

        return result;
    }
}
