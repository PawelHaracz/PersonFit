namespace PersonFit.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Options;

public abstract class PostgresDomainContext : DbContext
{
    private readonly IOptions<DbSetting> _options;
    private readonly ILoggerFactory _loggerFactory;
    private readonly DbSetting _setting;
    public PostgresDomainContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
    {
        _options = options;
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
        optionsBuilder.UseNpgsql(_setting.ToString())
            .UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }
}