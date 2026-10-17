using InterviewPrep.Application.Contracts.Search;

namespace InterviewPrep.Application.Interfaces;

public interface ISearchService
{
    Task<SearchQuestionsResponse> SearchQuestionsAsync(
        SearchQuestionsRequest request,
        CancellationToken cancellationToken = default);


    Task<List<SearchOptionResult>> GetRolesAsync(
        CancellationToken cancellationToken = default);


    Task<List<ExperienceLevelOptionResult>>
        GetExperienceLevelsAsync(
            CancellationToken cancellationToken = default);


    Task<List<SearchOptionResult>> GetSkillsAsync(
        CancellationToken cancellationToken = default);


    Task<List<SearchOptionResult>> GetCompaniesAsync(
        CancellationToken cancellationToken = default);
}