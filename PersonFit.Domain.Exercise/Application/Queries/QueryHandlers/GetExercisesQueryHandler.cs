namespace PersonFit.Domain.Exercise.Application.Queries.QueryHandlers;
using PersonFit.Core.Queries;
using Dtos;
using Core.Repositories;
using Infrastructure.Postgres.Documents;

internal class GetExercisesQueryHandler : IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>
{
    private readonly IReadExerciseRepository _domainRepository;

    public GetExercisesQueryHandler(IReadExerciseRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }
    
    public async Task<IEnumerable<ExerciseDto>> HandleAsync(GetExercisesQuery query, CancellationToken token = default)
    {
        var entity = await _domainRepository.GetAll(token);

        var enumerable = entity as Core.Entities.Exercise[] ?? entity.ToArray();
        if (!enumerable.Any())
        {
            return Enumerable.Empty<ExerciseDto>();
        }

        var item = enumerable.Select(exercise => exercise.AsDto());

        return item;
    }
}