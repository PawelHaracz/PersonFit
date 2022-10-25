namespace PersonFit.Query.Planner.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonFit.Infrastructure.Postgres.Options;

internal class PostgresPlannerReadContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DbSetting _setting;
    public const string ExerciseSchema = "exercise";
    public const string PlannerSchema = "planner";
    
    public PostgresPlannerReadContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        if (options.Value is not null)
        {
            _setting = options.Value;
        }
        else
        {
            throw new ArgumentNullException(nameof(options));
        }
    }
}