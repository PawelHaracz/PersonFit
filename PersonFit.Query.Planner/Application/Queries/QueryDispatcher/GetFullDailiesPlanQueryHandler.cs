using PersonFit.Core;
using PersonFit.Core.Queries;
using PersonFit.Query.Planner.Application.Dtos;

namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;

internal class GetFullDailiesPlanQueryHandler: IQueryHandler<GetFullDailiesPlanQuery, IEnumerable<QueryFulldailesPlannerDto>>
{    private readonly IReadDbContext _context;
    
    private readonly string _query = 
        @"select  ex.name, ex.description, ex.media, ex.tags, pex.repetitions, daily_exercises.start_time, daily_exercises.end_time, daily_exercises.status
    from (
        select jsonb_array_elements_text(dailies.elements -> 'ExercisesPlanners') :: uuid as exerciseId,
    dailies.elements -> 'DayOfWeek'                               as dow,
    dailies.elements -> 'TimeOfDay'                               as tod,
    dailies.status                                                as status,
    dailies.start_time as start_time,
    dailies.end_time as end_time
        from (
            select jsonb_array_elements(pje.dailies) as elements, pje.start_time, pje.status, pje.end_time
        from (select to_jsonb(p.daily_planners) as dailies, p.status, p.start_time, p.end_time from planner.planners p) as pje) as dailies
    ) as daily_exercises
        JOIN  planner.planner_exercises as pex
        ON daily_exercises.exerciseId = pex.id
        JOIN exercise.exercises as ex
        ON ex.id = pex.exercise_id
    ";

    public GetFullDailiesPlanQueryHandler(IReadDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<QueryFulldailesPlannerDto>> HandleAsync(GetFullDailiesPlanQuery query, CancellationToken token = default)
    {
        var z= await  _context.QueryAsync<QueryFulldailesPlannerDto>(_query, query, null,  token);
        return z;
    }
}