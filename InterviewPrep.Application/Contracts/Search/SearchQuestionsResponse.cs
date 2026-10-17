namespace InterviewPrep.Application.Contracts.Search;

public sealed class SearchQuestionsResponse
{
    public int TotalCount { get; set; }

    public List<SearchQuestionResult> Questions { get; set; }
        = new List<SearchQuestionResult>();
}