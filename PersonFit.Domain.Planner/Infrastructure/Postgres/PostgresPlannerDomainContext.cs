namespace PersonFit.Domain.Planner.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Documents;
using PersonFit.Infrastructure.Postgres.Options;
using Microsoft.Extensions.Logging;

internal class PostgresPlannerDomainContext : DbContext
{
    private const string Schema = "planner";
    private readonly ILoggerFactory _loggerFactory;
    public virtual DbSet<ExercisePlannerDocument> PlannerExercises { get; private set; }
    public virtual DbSet<PlannerDocument> Planners { get; private set; }
    private readonly DbSetting _setting;
    
    public PostgresPlannerDomainContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.UseNpgsql(_setting.ToString());
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
    }
}