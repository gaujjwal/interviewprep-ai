namespace InterviewPrep.Domain.Entities;

public class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<QuestionSkill> QuestionSkills { get; set; }
        = new List<QuestionSkill>();
}