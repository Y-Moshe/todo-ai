using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/account")]
public class AccountController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountRepository, IMapper mapper)
    {
        _accountService = accountRepository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AppUserDto>> GetCurrentLoggedInUser()
    {
        var user = await _accountService.GetUserByClaimsAsync(User);
        return _mapper.Map<AppUser, AppUserDto>(user);
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> CheckEmailExistence([FromQuery] string email)
    {
        return await _accountService.GetUserByEmailAsync(email) != null;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginCredentialsDto credentials)
    {
        try
        {
            var user = await _accountService.LoginUserAsync(credentials.Email, credentials.Password);
            var token = _accountService.GenerateToken(user);
            return new LoginResponseDto
            {
                User = _mapper.Map<AppUser, AppUserDto>(user),
                Token = token
            };
        }
        catch (Exception ex)
        {
            return Unauthorized(new ApiErrorResponse(401, ex.Message));
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register(RegisterFieldsDto fields)
    {
        try
        {
            var user = await _accountService.RegisterUserAsync(fields);
            var token = _accountService.GenerateToken(user);
            return new LoginResponseDto
            {
                User = _mapper.Map<AppUser, AppUserDto>(user),
                Token = token
            };
        }
        catch (Exception ex)
        {
            return Unauthorized(new ApiErrorResponse(401, ex.Message));
        }
    }
}
