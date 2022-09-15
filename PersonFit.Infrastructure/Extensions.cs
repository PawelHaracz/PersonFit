using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Queries;
using PersonFit.Infrastructure.Dispatchers;
using PersonFit.Infrastructure.Events;
using PersonFit.Infrastructure.Postgres.Options;
using Serilog;

namespace PersonFit.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        
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