namespace InterviewPrep.Application.Contracts.Search;

public sealed class ExperienceLevelOptionResult
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? MinYears { get; set; }

    public int? MaxYears { get; set; }

    public int SortOrder { get; set; }
}