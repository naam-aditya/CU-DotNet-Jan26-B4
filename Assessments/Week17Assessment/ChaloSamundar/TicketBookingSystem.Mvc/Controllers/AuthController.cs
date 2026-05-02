using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using TicketBookingSystem.Mvc.DTOs;
using TicketBookingSystem.Mvc.Services;

namespace TicketBookingSystem.Mvc.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("Login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem("User", "User"),
                new SelectListItem("Admin", "Admin")
            };

        return View(new RegisterDto());
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "User", Text = "User" }
            };

        if (!ModelState.IsValid)
            return View(register);

        var success = await _authService.RegisterAsync(register);
        if (!success)
        {
            ViewBag.Error = "Registration failed!";
            return View(register);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto login, string? returnUrl = null)
    {
        
        var token = await _authService.LoginAsync(login);
        if (token == null)
        {
            ViewBag.Error = "Invalid credentials";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        HttpContext.Session.SetString("JWT", token);
        if(!string.IsNullOrEmpty(returnUrl)) //&& Url.IsLocalUrl(returnUrl)
        {
            return Redirect(returnUrl);
        }

        // ── Extract userId from JWT and store in session ──
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var userId = jwt.Claims.FirstOrDefault(c =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        )?.Value ?? string.Empty;

        HttpContext.Session.SetString("userId", userId);
        // ──────────────────────────────────────────────────

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("JWT");
        return RedirectToAction(nameof(Login));
    }
}
