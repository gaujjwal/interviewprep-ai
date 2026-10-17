using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly InterviewPrepDbContext _context;

    public CategoriesController(
        InterviewPrepDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories =
            await _context.Categories.ToListAsync();

        return Ok(categories);
    }
}