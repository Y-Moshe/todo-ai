using System.Security.Claims;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService
    )
    {
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public string GenerateToken(AppUser user)
    {
        return _tokenService.CreateUserToken(user);
    }

    public async Task<AppUser> GetUserByClaimsAsync(ClaimsPrincipal userClaims)
    {
        var email = userClaims.FindFirstValue(ClaimTypes.Email);
        return await _userManager.Users
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<AppUser> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<AppUser> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("Invalid email or password!");

        var passwordMatchResult = await _signInManager
            .CheckPasswordSignInAsync(user, password, false);

        if (!passwordMatchResult.Succeeded)
            throw new Exception("Invalid email or password!");

        return user;
    }

    public async Task<AppUser> RegisterUserAsync(dynamic userFields)
    {
        var isEmailExists = (await _userManager.FindByEmailAsync(userFields.Email)) != null;
        if (isEmailExists) throw new Exception("Account already exists with email " + userFields.Email);

        var user = new AppUser
        {
            FirstName = userFields.FirstName,
            LastName = userFields.LastName,
            Email = userFields.Email,
            UserName = userFields.Email
        };

        var result = await _userManager.CreateAsync(user, userFields.Password);
        if (!result.Succeeded) throw new Exception("Something went wrong");

        return user;
    }

    public async Task<IdentityResult> UpdateUserAsync(dynamic user)
    {
        return await _userManager.UpdateAsync(user);
    }
}