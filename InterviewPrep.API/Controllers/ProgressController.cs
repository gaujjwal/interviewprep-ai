using InterviewPrep.Application.DTOs;
using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InterviewPrep.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProgressController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;

    public ProgressController(InterviewPrepDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SaveProgress(
        ProgressDto dto)
    {
        var progress = new UserProgress
        {
            UserId = dto.UserId,
            QuestionId = dto.QuestionId,
            IsCompleted = dto.IsCompleted,
            LastVisited = DateTime.UtcNow
        };

        _context.UserProgresses.Add(progress);

        await _context.SaveChangesAsync();

        return Ok(progress);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetProgress(
    Guid userId)
    {
        var progress = await _context.UserProgresses
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return Ok(progress);
    }
}