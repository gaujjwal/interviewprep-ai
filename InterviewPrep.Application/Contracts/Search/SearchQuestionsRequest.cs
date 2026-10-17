namespace InterviewPrep.Application.Contracts.Search;

public sealed class SearchQuestionsRequest
{
    public int? RoleId { get; set; }

    public int? ExperienceLevelId { get; set; }

    public List<int> SkillIds { get; set; } = [];

    public int? CompanyId { get; set; }
}