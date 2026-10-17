namespace InterviewPrep.Domain.Entities;

public class Question
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    // Legacy text field.
    // Keep temporarily so the existing API does not break.
    public string Category { get; set; } = string.Empty;

    // Legacy
    public string ExperienceLevel { get; set; }
        = string.Empty;


    // New normalized relationship
    public int? ExperienceLevelId { get; set; }

    public ExperienceLevel? ExperienceLevelDetails
    {
        get;
        set;
    }

    public int RoleId { get; set; }

    public Role? Role { get; set; }


    public int CategoryId { get; set; }

    public Category CategoryDetails { get; set; } = null!;
    public ICollection<QuestionSkill> QuestionSkills { get; set; }
    = new List<QuestionSkill>();
    public ICollection<QuestionCompany> QuestionCompanies { get; set; }
    = new List<QuestionCompany>();
}