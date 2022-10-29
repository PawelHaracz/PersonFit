namespace PersonFit.Query.Planner.Application.Dtos;
using Enums;
internal class QueryPlannerDto
{
    public Guid Id { get; set; }
    public PlannerStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    private static readonly string _query = "SELECT p.id as id, p.status as status, p.start_time as startTime, p.end_time as endTime FROM planner.planners p WHERE p.owner_id = @OwnerId AND p.status = ANY(@Status)";
    public static string GetSqlQuery() => _query;
    public static object GetParameters(Guid ownerId, PlannerStatus status)
    {
        var statusCollection = new List<int>();
        if (status is PlannerStatus.Active)
        {
            statusCollection.Add((int)PlannerStatus.Active);
        }

        if (status is PlannerStatus.Pending)
        {
            statusCollection.Add((int)PlannerStatus.Pending);
        }

        return new
        {
            OwnerId = ownerId,
            Status = statusCollection.ToArray()
        };
    }
}