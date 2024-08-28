using System;
using Memory.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Memory.Services.JWTService;

public class JWTService : IJWTService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryDuration;

    public JWTService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _expiryDuration = int.Parse(configuration["Jwt:ExpiryDuration"]); // in minutes
    }

    public string GenerateToken(UserDto userDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userDto.Id.ToString()),
                new Claim("name", userDto.Name),
                new Claim("countNumber", userDto.UserNumber.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expiryDuration),
            Issuer = _issuer, // Додаємо Issuer
            Audience = _audience, // Додаємо Audience
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
