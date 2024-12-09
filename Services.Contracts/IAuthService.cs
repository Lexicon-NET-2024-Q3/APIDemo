using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Identity; ////ToDo Check version

namespace Services.Contracts;

public interface IAuthService
{
    Task<string> CreateTokenAsync();
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto registrationDto);
    Task<bool> ValidateUserAsync(UserForAuthDto userForAuthDto);
}