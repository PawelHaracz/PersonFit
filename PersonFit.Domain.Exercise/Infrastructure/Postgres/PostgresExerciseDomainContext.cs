using PersonFit.Infrastructure.Postgres;

namespace PersonFit.Domain.Exercise.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Documents;
using PersonFit.Infrastructure.Postgres.Options;
using Microsoft.Extensions.Logging;

internal class PostgresExerciseDomainContext : PostgresDomainContext
{
    private const string Schema = "exercise";
    public virtual DbSet<ExerciseDocument> Exercises { get; private set; }

    
    public PostgresExerciseDomainContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory): base(options, loggerFactory)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
    }
}