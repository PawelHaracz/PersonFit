namespace PersonFit.Domain.Exercise.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Documents;
using PersonFit.Infrastructure.Postgres.Options;
using Microsoft.Extensions.Logging;

internal class PostgresExerciseDomainContext : DbContext
{
    private const string Schema = "exercise";
    private readonly ILoggerFactory _loggerFactory;
    public virtual DbSet<ExerciseDocument> Exercises { get; private set; }
    private readonly DbSetting _setting;
    
    public PostgresExerciseDomainContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
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