using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using PersonFit.Domain.Exercise.Core.Repositories;
using PersonFit.Domain.Exercise.Infrastructure.Postgres;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Compact;
using Serilog.Sinks.ILogger;

namespace PersonFit.Domain.Exercise.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.Configure(options =>
            {
                options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId | ActivityTrackingOptions.TraceId |
                                                  ActivityTrackingOptions.ParentId;
            });
            loggingBuilder.AddConfiguration();
            var log = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
            
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(log, dispose: true);
            builder.Host.UseSerilog(log);
        });
        
        builder.Services.AddDbContext<PostgresContext>((serviceProvider, options) =>
        {
            options.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());
            options.UseNpgsql(builder.Configuration.GetValue<string>("ExerciseDatabaseConnection"));
        });
        builder.Services.AddScoped<IPostgresRepository<ExerciseDocument, Guid>, PostgresRepository>();
        builder.Services.AddScoped<IExerciseRepository, ExerciseDomainRepository>();
        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication webApplication)
    {
   
        webApplication.UseSerilogRequestLogging();
        return webApplication;
    }
}