namespace InterviewPrep.Application.DTOs;

public class DashboardDto
{
    public int TotalQuestions { get; set; }

    public int CompletedQuestions { get; set; }

    public int RemainingQuestions { get; set; }

    public double CompletionPercentage { get; set; }
}