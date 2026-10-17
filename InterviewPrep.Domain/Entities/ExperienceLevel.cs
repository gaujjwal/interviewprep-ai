namespace InterviewPrep.Domain.Entities;

public class ExperienceLevel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? MinYears { get; set; }

    public int? MaxYears { get; set; }

    public int SortOrder { get; set; }

    public ICollection<Question> Questions { get; set; }
        = new List<Question>();
}