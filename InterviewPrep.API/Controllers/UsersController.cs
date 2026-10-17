using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;

    public UsersController(InterviewPrepDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(user);
    }
}
