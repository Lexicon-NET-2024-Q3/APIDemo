using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Identity; ////ToDo Check version

namespace Services.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto registrationDto);
}