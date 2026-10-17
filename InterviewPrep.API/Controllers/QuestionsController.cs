using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;

    public QuestionsController(
        InterviewPrepDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(
    int? roleId,
    string? experience,
    string? category,
    string? search)
    {
        var query = _context.Questions.AsQueryable();

        if (roleId.HasValue)
        {
            query = query.Where(x => x.RoleId == roleId);
        }

        if (!string.IsNullOrEmpty(experience))
        {
            query = query.Where(x =>
                x.ExperienceLevel == experience);
        }

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(x =>
                x.Category == category);
        }

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x =>
                x.Title.Contains(search));
        }

        return Ok(await query.ToListAsync());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
    Question question)
    {
        _context.Questions.Add(question);

        await _context.SaveChangesAsync();

        return Ok(question);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    long id,
    Question question)
    {
        var existing =
            await _context.Questions.FindAsync(id);

        if (existing == null)
            return NotFound();

        existing.Title = question.Title;
        existing.Answer = question.Answer;
        existing.Category = question.Category;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var question =
            await _context.Questions.FindAsync(id);

        if (question == null)
            return NotFound();

        _context.Questions.Remove(question);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}