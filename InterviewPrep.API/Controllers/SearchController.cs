using InterviewPrep.Application.Contracts.Search;
using InterviewPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPrep.API.Controllers;

[ApiController]
[Route("api/search")]
[Authorize]
public sealed class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(
        ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost("questions")]
    [ProducesResponseType(
        typeof(SearchQuestionsResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status400BadRequest)]
    [ProducesResponseType(
        StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<SearchQuestionsResponse>>
        SearchQuestions(
            [FromBody] SearchQuestionsRequest request,
            CancellationToken cancellationToken)
    {
        try
        {
            var result =
                await _searchService.SearchQuestionsAsync(
                    request,
                    cancellationToken);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }
    [HttpGet("options/roles")]
    [ProducesResponseType(
    typeof(List<SearchOptionResult>),
    StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SearchOptionResult>>>
    GetRoles(
        CancellationToken cancellationToken)
    {
        var roles =
            await _searchService.GetRolesAsync(
                cancellationToken);

        return Ok(roles);
    }
    [HttpGet("options/experience-levels")]
    [ProducesResponseType(
    typeof(List<ExperienceLevelOptionResult>),
    StatusCodes.Status200OK)]
    public async Task<
    ActionResult<List<ExperienceLevelOptionResult>>>
    GetExperienceLevels(
        CancellationToken cancellationToken)
    {
        var experienceLevels =
            await _searchService.GetExperienceLevelsAsync(
                cancellationToken);

        return Ok(experienceLevels);
    }
    [HttpGet("options/skills")]
    [ProducesResponseType(
    typeof(List<SearchOptionResult>),
    StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SearchOptionResult>>>
    GetSkills(
        CancellationToken cancellationToken)
    {
        var skills =
            await _searchService.GetSkillsAsync(
                cancellationToken);

        return Ok(skills);
    }
    [HttpGet("options/companies")]
    [ProducesResponseType(
    typeof(List<SearchOptionResult>),
    StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SearchOptionResult>>>
    GetCompanies(
        CancellationToken cancellationToken)
    {
        var companies =
            await _searchService.GetCompaniesAsync(
                cancellationToken);

        return Ok(companies);
    }
}
