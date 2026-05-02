using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.AuthService.Dtos;
using TicketBookingSystem.AuthService.Exceptions;
using TicketBookingSystem.AuthService.Models;
using TicketBookingSystem.AuthService.Services;

namespace TicketBookingSystem.AuthService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<User> _usermanager;
    private readonly JwtTokenService _tokenservice;

    public AuthController(ILogger<AuthController> logger,
        UserManager<User> userManager,
        JwtTokenService tokenService)
    {
        _logger = logger;
        _usermanager = userManager;
        _tokenservice = tokenService;
    }

    [HttpPost("Register")]

    public async Task<IActionResult> Register(RegisterDto register)
    {
        var user = new User
        {
            UserName = register.Email,
            Email = register.Email,
        };

        var result = await _usermanager.CreateAsync(
            user, 
            register.Password ?? throw new NotFoundException("Password is required."));
        
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _usermanager.AddToRoleAsync(user, register.Role ?? throw new NotFoundException("Role is required."));
        return Ok("User Registered SuccessFully");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var user = await _usermanager.FindByEmailAsync(login.Email ?? throw new NotFoundException("Email is required"));
        if (user == null || !await _usermanager.CheckPasswordAsync(user, login.Password ?? throw new NotFoundException("Password is required")))
            return Unauthorized("Invalid Credentials");
        
        var roles = await _usermanager.GetRolesAsync(user);
        var token = _tokenservice.CreateToken(user, roles);
        
        return Ok(new { token });
    }
}
