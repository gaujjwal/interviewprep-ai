using InterviewPrep.Application.DTOs;
using InterviewPrep.Application.Services;
using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace InterviewPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(InterviewPrepDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password),
            ExperienceYears = dto.ExperienceYears,
            Role = "Candidate"
        };
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var hashedPassword = HashPassword(dto.Password);

        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Email == dto.Email &&
                x.PasswordHash == hashedPassword);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var jwtService = new JwtTokenService();

        var token = jwtService.GenerateToken(
            user,
            _configuration["Jwt:Key"]!);

        return Ok(new
        {
            Token = token
        });
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();

        var bytes = sha.ComputeHash(
            Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(bytes);
    }
}