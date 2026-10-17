namespace InterviewPrep.Application.Contracts.Search;

public sealed class SearchQuestionResult
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public string Role { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Category { get; set; } = string.Empty;

    public int ExperienceLevelId { get; set; }

    public string ExperienceLevel { get; set; } = string.Empty;

    public List<SearchSkillResult> Skills { get; set; } = [];

    public List<SearchCompanyResult> Companies { get; set; } = [];
}