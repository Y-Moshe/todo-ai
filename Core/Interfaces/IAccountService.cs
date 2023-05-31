using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<AppUser> GetUserByEmailAsync(string email);
    Task<AppUser> GetUserByClaimsAsync(ClaimsPrincipal userClaims);
    Task<AppUser> LoginUserAsync(string email, string password);
    Task<AppUser> RegisterUserAsync(dynamic userFields);
    Task<IdentityResult> UpdateUserAsync(dynamic user);
    string GenerateToken(AppUser user);
}
