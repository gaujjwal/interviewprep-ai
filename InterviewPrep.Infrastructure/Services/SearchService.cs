using InterviewPrep.Application.Contracts.Search;
using InterviewPrep.Application.Interfaces;
using InterviewPrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Infrastructure.Services;

public sealed class SearchService : ISearchService
{
    private readonly InterviewPrepDbContext _dbContext;

    public SearchService(
        InterviewPrepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SearchQuestionsResponse> SearchQuestionsAsync(
        SearchQuestionsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateRequest(request);

        var skillIds = request.SkillIds
            .Where(id => id > 0)
            .Distinct()
            .ToList();

        var query = _dbContext.Questions
            .AsNoTracking()
            .AsQueryable();


        // =====================================
        // ROLE FILTER
        // =====================================

        if (request.RoleId.HasValue)
        {
            query = query.Where(
                question =>
                    question.RoleId == request.RoleId.Value);
        }


        // =====================================
        // EXPERIENCE FILTER
        // =====================================

        if (request.ExperienceLevelId.HasValue)
        {
            query = query.Where(
                question =>
                    question.ExperienceLevelId ==
                    request.ExperienceLevelId.Value);
        }


        // =====================================
        // COMPANY FILTER
        // =====================================

        if (request.CompanyId.HasValue)
        {
            query = query.Where(
                question =>
                    question.QuestionCompanies.Any(
                        questionCompany =>
                            questionCompany.CompanyId ==
                            request.CompanyId.Value));
        }


        // =====================================
        // SKILL FILTER
        //
        // ANY selected skill:
        //
        // SkillId = 1 OR
        // SkillId = 2 OR
        // SkillId = 3
        // =====================================

        if (skillIds.Count > 0)
        {
            query = query.Where(
                question =>
                    question.QuestionSkills.Any(
                        questionSkill =>
                            skillIds.Contains(
                                questionSkill.SkillId)));
        }


        // =====================================
        // EXECUTE RESULT QUERY
        // =====================================

        var questions = await query
            .OrderBy(question => question.Id)
            .Select(question =>
                new SearchQuestionResult
                {
                    Id = question.Id,

                    Title = question.Title,

                    Answer = question.Answer,


                    // Role
                    RoleId = question.RoleId,

                    Role = question.Role != null
                        ? question.Role.Name
                        : string.Empty,


                    // Category
                    CategoryId = question.CategoryId,

                    Category =
                        question.CategoryDetails.Name,


                    // Experience
                    ExperienceLevelId =
                        question.ExperienceLevelId!.Value,

                    ExperienceLevel =
                        question.ExperienceLevelDetails != null
                            ? question.ExperienceLevelDetails.Name
                            : string.Empty,


                    // Skills
                    Skills = question.QuestionSkills
                        .OrderBy(questionSkill =>
                            questionSkill.Skill.Name)
                        .Select(questionSkill =>
                            new SearchSkillResult
                            {
                                Id = questionSkill.SkillId,

                                Name =
                                    questionSkill.Skill.Name
                            })
                        .ToList(),


                    // Companies
                    Companies = question.QuestionCompanies
                        .OrderBy(questionCompany =>
                            questionCompany.Company.Name)
                        .Select(questionCompany =>
                            new SearchCompanyResult
                            {
                                Id =
                                    questionCompany.CompanyId,

                                Name =
                                    questionCompany.Company.Name
                            })
                        .ToList()
                })
            .ToListAsync(cancellationToken);


        return new SearchQuestionsResponse
        {
            TotalCount = questions.Count,
            Questions = questions
        };
    }


    private static void ValidateRequest(
        SearchQuestionsRequest request)
    {
        if (request.RoleId.HasValue &&
            request.RoleId.Value <= 0)
        {
            throw new ArgumentException(
                "RoleId must be greater than zero.",
                nameof(request));
        }


        if (request.ExperienceLevelId.HasValue &&
            request.ExperienceLevelId.Value <= 0)
        {
            throw new ArgumentException(
                "ExperienceLevelId must be greater than zero.",
                nameof(request));
        }


        if (request.CompanyId.HasValue &&
            request.CompanyId.Value <= 0)
        {
            throw new ArgumentException(
                "CompanyId must be greater than zero.",
                nameof(request));
        }


        if (request.SkillIds.Any(id => id <= 0))
        {
            throw new ArgumentException(
                "All SkillIds must be greater than zero.",
                nameof(request));
        }


        var hasRole =
            request.RoleId.HasValue;

        var hasExperience =
            request.ExperienceLevelId.HasValue;

        var hasCompany =
            request.CompanyId.HasValue;

        var hasSkills =
            request.SkillIds.Count > 0;


        if (!hasRole &&
            !hasExperience &&
            !hasCompany &&
            !hasSkills)
        {
            throw new ArgumentException(
                "At least one search filter must be provided.",
                nameof(request));
        }
    }
    public async Task<List<SearchOptionResult>> GetRolesAsync(
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .OrderBy(role => role.Name)
            .Select(role =>
                new SearchOptionResult
                {
                    Id = role.Id,
                    Name = role.Name
                })
            .ToListAsync(cancellationToken);
    }
    public async Task<List<ExperienceLevelOptionResult>>
    GetExperienceLevelsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExperienceLevels
            .AsNoTracking()
            .OrderBy(level => level.SortOrder)
            .Select(level =>
                new ExperienceLevelOptionResult
                {
                    Id = level.Id,

                    Name = level.Name,

                    MinYears = level.MinYears,

                    MaxYears = level.MaxYears,

                    SortOrder = level.SortOrder
                })
            .ToListAsync(cancellationToken);
    }
    public async Task<List<SearchOptionResult>> GetSkillsAsync(
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Skills
            .AsNoTracking()
            .OrderBy(skill => skill.Name)
            .Select(skill =>
                new SearchOptionResult
                {
                    Id = skill.Id,
                    Name = skill.Name
                })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SearchOptionResult>> GetCompaniesAsync(
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.Companies
            .AsNoTracking()
            .OrderBy(company => company.Name)
            .Select(company =>
                new SearchOptionResult
                {
                    Id = company.Id,
                    Name = company.Name
                })
            .ToListAsync(cancellationToken);
    }
}