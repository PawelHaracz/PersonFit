namespace PersonFit.Domain.Exercise.Application.Queries.QueryHandlers;
using PersonFit.Core.Queries;
using Dtos;
using Core.Repositories;
using Infrastructure.Postgres.Documents;

public class GetExerciseQueryHandler : IQueryHandler<GetExerciseQuery, ExerciseSummaryDto>
{
    private readonly IReadExerciseRepository _domainRepository;

    public GetExerciseQueryHandler(IReadExerciseRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }

    public async Task<ExerciseSummaryDto> HandleAsync(GetExerciseQuery query, CancellationToken token = default)
    {
        var entity = await _domainRepository.Get(query.Id);

        if (entity is null)
        {
            return ExerciseSummaryDto.Default;
        }

        return entity.AsSummaryDto();
    }
}