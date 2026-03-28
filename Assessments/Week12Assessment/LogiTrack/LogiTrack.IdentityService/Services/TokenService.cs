using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LogiTrack.IdentityService.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration configuration) { _config = configuration; }

    public string CreateToken(IdentityUser user, IList<string> roles)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        ];

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
