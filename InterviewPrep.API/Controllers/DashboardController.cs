using InterviewPrep.Application.DTOs;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;

    public DashboardController(InterviewPrepDbContext context)
    {
        _context = context;
    }

    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetDashboard(Guid userId)
    {
        var totalQuestions =
            await _context.Questions.CountAsync();

        var completedQuestions =
            await _context.UserProgresses.CountAsync(
                x => x.UserId == userId &&
                     x.IsCompleted);

        var dashboard = new DashboardDto
        {
            TotalQuestions = totalQuestions,
            CompletedQuestions = completedQuestions,
            RemainingQuestions =
                totalQuestions - completedQuestions,
            CompletionPercentage =
                totalQuestions == 0
                    ? 0
                    : (completedQuestions * 100.0) /
                      totalQuestions
        };

        return Ok(dashboard);
    }
}