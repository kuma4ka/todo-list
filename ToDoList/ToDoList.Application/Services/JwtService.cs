using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Application.DTOs.UserRelated;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Options;

namespace ToDoList.Application.Services;

public class JwtService(IOptions<JwtOptions> options) : IJwtService
{
    private readonly JwtOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    
    public string GenerateJwtToken(UserDto userDto)
    {
        ArgumentNullException.ThrowIfNull(userDto);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Key);
        var securityKey = new SymmetricSecurityKey(key);
        
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userDto.UserName),
            new (ClaimTypes.NameIdentifier, userDto.Id),
            new (ClaimTypes.Email, userDto.Email),
        };
        
        claims.AddRange(userDto.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_options.TokenExpiryMinutes),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}