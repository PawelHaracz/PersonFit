using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Options;

namespace PersonFit.Domain.Exercise.Infrastructure.Postgres;

internal class PostgresContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    public virtual DbSet<ExerciseDocument> Exercises { get; private set; }
    private readonly DbSetting _setting;
    
    public PostgresContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
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
}