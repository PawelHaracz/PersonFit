using PersonFit.Core.Queries;
using PersonFit.Domain.Exercise.Application.Dtos;
using PersonFit.Domain.Exercise.Core.Repositories;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;

namespace PersonFit.Domain.Exercise.Application.Queries.QueryHandlers;

public class GetExercisesQueryHandler : IQueryHandler<GetExercisesQuery, PagedResult<ExerciseDto>>
{
    //todo create read repository
    private readonly IExerciseRepository _domainRepository;

    public GetExercisesQueryHandler(IExerciseRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }

    //todo write tests
    public async Task<PagedResult<ExerciseDto>> HandleAsync(GetExercisesQuery query, CancellationToken token = default)
    {
        var entity = await _domainRepository.Get(token);

        var enumerable = entity as Core.Entities.Exercise[] ?? entity.ToArray();
        if (!enumerable.Any())
        {
            return  PagedResult<ExerciseDto>.Empty;
        }

        var item = enumerable.Select(exercise => exercise.AsDto());
        //todo automagicly calculation 
        return PagedResult<ExerciseDto>.Create(item, 0, 10, 1, 10);
    }
}