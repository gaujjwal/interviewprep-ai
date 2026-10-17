using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {



        private readonly InterviewPrepDbContext _context;

        public RolesController(InterviewPrepDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _context.Roles.ToListAsync();

            return Ok(roles);
        }

    }
}
