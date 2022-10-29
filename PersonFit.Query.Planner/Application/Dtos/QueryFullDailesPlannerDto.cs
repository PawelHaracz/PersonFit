namespace PersonFit.Query.Planner.Application.Dtos;
using PersonFit.Core.Enums;
using Enums;
using Dapper.Json;
internal class QueryFullDailesPlannerDto
{
    public Guid PlanId { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseDescription { get; set; }
    public Json<IEnumerable<string>> ExerciseMedia { get; set; }
    public Json<IEnumerable<string>> ExerciseTags { get; set; }
    public Json<IEnumerable<Repetition>> ExerciseRepetitions { get; set; }
    public DateTime PlannerStartTime { get; set; }
    public DateTime PlannerEndTime { get; set; }
    public PlannerStatus PlannerStatus { get; set; }
    public DayOfWeek DailyDayOfWork { get; set; }
    public TimeOfDay DailyTimeOfWork { get; set; }
    
    private static readonly string _query = 
@"SELECT 
    daily_exercises.id          AS planId, 
    ex.name                     AS exerciseName,
    ex.description              AS exerciseDescription,
    ex.media                    AS exerciseMedia,
    ex.tags                     AS exerciseTags,
    pex.repetitions             AS exerciseRepetitions,
    daily_exercises.start_time  AS plannerStartTime,  
    daily_exercises.end_time    AS plannerEndTime, 
    daily_exercises.status      AS plannerStatus,
    daily_exercises.dow         AS dailyDayOfWork,
    daily_exercises.tod         AS dailyTimeOfWork
FROM (
         SELECT jsonb_array_elements_text(dailies.elements -> 'ExercisesPlanners') :: uuid AS exerciseId,
                dailies.elements -> 'DayOfWeek'   AS dow,
                dailies.elements -> 'TimeOfDay'   AS tod,
                dailies.status                    AS status,
                dailies.start_time AS start_time,
                dailies.end_time AS end_time,
                dailies.id,
                dailies.owner_id
         FROM (
                  SELECT jsonb_array_elements(pje.dailies) AS elements, pje.start_time, pje.status, pje.end_time, pje.id, pje.owner_id
                  FROM (select to_jsonb(p.daily_planners) AS dailies, p.id, p.status, p.start_time, p.end_time, p.owner_id FROM planner.planners p
                  WHERE p.owner_id = @OwnerId and p.id  = @PlannerId) AS pje) AS dailies
     ) AS daily_exercises
JOIN  planner.planner_exercises AS pex
ON daily_exercises.exerciseId = pex.id
JOIN exercise.exercises AS ex
ON ex.id = pex.exercise_id
WHERE pex.owner_id = @OwnerId
";

    public static string GetSqlQuery()
        => _query;

    public static object CreateParameters(Guid ownerId, Guid plannerId)
    {
        return new
        {
            OwnerId = ownerId,
            PlannerId = plannerId
        };
    }
}