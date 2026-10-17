namespace InterviewPrep.Domain.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<QuestionCompany> QuestionCompanies { get; set; }
        = new List<QuestionCompany>();
}