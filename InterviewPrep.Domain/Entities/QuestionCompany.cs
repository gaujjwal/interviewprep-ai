namespace InterviewPrep.Domain.Entities;

public class QuestionCompany
{
    public long QuestionId { get; set; }

    public Question Question { get; set; } = null!;

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;
}