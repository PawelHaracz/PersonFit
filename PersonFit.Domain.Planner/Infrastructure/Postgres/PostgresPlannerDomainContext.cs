namespace PersonFit.Domain.Planner.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Documents;
using PersonFit.Infrastructure.Postgres;
using PersonFit.Infrastructure.Postgres.Options;
using Microsoft.Extensions.Logging;

internal class PostgresPlannerDomainContext : PostgresDomainContext
{
    private const string Schema = "planner";
    public virtual DbSet<ExercisePlannerDocument> PlannerExercises { get; private set; }
    public virtual DbSet<PlannerDocument> Planners { get; private set; }
    public PostgresPlannerDomainContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
    }
}