using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InterviewPrep.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace InterviewPrep.Application.Services;

public class JwtTokenService
{
    public string GenerateToken(User user, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
{
    new Claim(
        ClaimTypes.NameIdentifier,
        user.Id.ToString()),

    new Claim(
        ClaimTypes.Name,
        user.FullName),

    new Claim(
        ClaimTypes.Email,
        user.Email),

    new Claim(
        ClaimTypes.Role,
        user.Role)
}),

            Expires = DateTime.UtcNow.AddHours(2),

            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}