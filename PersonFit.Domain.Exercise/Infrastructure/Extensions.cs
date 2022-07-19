using PersonFit.Core;
using PersonFit.Domain.Exercise.Core.Repositories;
using PersonFit.Domain.Exercise.Infrastructure.Postgres;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.Logging.Configuration;
using PersonFit.Domain.Exercise.Application.Commands;
using PersonFit.Domain.Exercise.Application.Commands.CommandHandlers;
using PersonFit.Domain.Exercise.Infrastructure.Dispatchers;
using PersonFit.Domain.Exercise.Infrastructure.Events;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Options;
using Serilog;

namespace PersonFit.Domain.Exercise.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddScoped<ICommandHandler<AddExerciseCommand>, AddExerciseCommandHandler>();
        
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

        builder.Services.AddOptions<DbSetting>().Configure<IConfiguration>((setting, configuration) =>
            configuration.GetSection("postgres").Bind(setting));
        builder.Services.AddDbContext<PostgresContext>();
        builder.Services.AddScoped<IPostgresRepository<ExerciseDocument, Guid>, PostgresRepository>();
        builder.Services.AddScoped<IExerciseRepository, ExerciseDomainRepository>();
        
        builder.Services.AddDaprClient();
        builder.Services.AddScoped<IEventMapper, EventMapper>();
        builder.Services.AddScoped<IEventProcessor, EventProcessor>();
        builder.Services.AddScoped<IMessageBroker, MessageBroker>();
        return builder;
    }

    public static WebApplication UseInfrastructure(this WebApplication webApplication)
    {
        webApplication.UseSerilogRequestLogging();
        return webApplication;
    }
}