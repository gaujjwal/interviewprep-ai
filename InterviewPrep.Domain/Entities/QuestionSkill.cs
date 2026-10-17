namespace InterviewPrep.Domain.Entities;

public class QuestionSkill
{
    public long QuestionId { get; set; }

    public Question Question { get; set; } = null!;

    public int SkillId { get; set; }

    public Skill Skill { get; set; } = null!;
}