using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TicketBookingSystem.AuthService.Exceptions;
using TicketBookingSystem.AuthService.Models;

namespace TicketBookingSystem.AuthService.Services;

public class JwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(User user, IList<string> roles)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
        ];

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        string jwtSecretKey = _config["Jwt:Key"] ?? throw new NotFoundException("JWT secret key not available.");
        string issuer = _config["Jwt:Issuer"] ?? throw new NotFoundException("JWT issuer key not available.");
        string audience = _config["Jwt:Audience"] ?? throw new NotFoundException("JWT audience key not available.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
