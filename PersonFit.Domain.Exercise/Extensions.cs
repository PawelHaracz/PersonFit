using Microsoft.Extensions.Configuration;

namespace PersonFit.Domain.Exercise;
using PersonFit.Core;
using PersonFit.Core.Commands;
using Application.Commands;
using Application.Commands.CommandHandlers;
using Core.Repositories;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Documents;
using Infrastructure.Postgres.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonFit.Core.Queries;
using Api.Dtos.Commands;
using Application.Dtos;
using Application.Queries;
using Application.Queries.QueryHandlers;
using PersonFit.Infrastructure.Postgres;

public static class Extensions
{
    public static WebApplicationBuilder RegisterExerciseDomain(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PostgresExerciseDomainContext>();
        
        builder.Services.AddScoped<ICommandHandler<AddExerciseCommand>, AddExerciseCommandHandler>();

        builder.Services.AddScoped<IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>, GetExercisesQueryHandler>();
        builder.Services.AddScoped<IQueryHandler<GetExerciseQuery, ExerciseSummaryDto>, GetExerciseQueryHandler>();
        
        builder.Services.AddScoped<IPostgresRepository<ExerciseDocument, Guid>>(
            provider => new  PostgresDomainRepository<ExerciseDocument>(provider.GetRequiredService<PostgresExerciseDomainContext>()));
        builder.Services.AddScoped<IExerciseRepository, ExerciseDomainRepository>();
        builder.Services.AddScoped<IReadExerciseRepository, ReadExerciseRepository>();
        
        return builder;
    }

    public static WebApplication UseExerciseDomain(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("isMigrate"))
        {
            using var scope = app.Services.CreateScope();
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<PostgresExerciseDomainContext>();    
                context.Database.Migrate();
            }
        }
        
        app.MapGet("/exercise", async (HttpContext context ,  CancellationToken token, IQueryDispatcher dispatcher) =>
        {
            var query = await dispatcher.QueryAsync(new GetExercisesQuery(), token);
            return Results.Ok(query);
        });
        
        app.MapGet("/exercise/{id:guid}", async (HttpContext context ,  CancellationToken token, Guid id, IQueryDispatcher dispatcher) =>
        {
            var query = await dispatcher.QueryAsync(new GetExerciseQuery(id), token);
            return Results.Ok(query);
        });
        
        app.MapPut("/exercise", async (AddExerciseCommandDto dto, CancellationToken token, ICommandDispatcher dispatcher) =>
        {
            var id = Guid.NewGuid();

            var command = new AddExerciseCommand(id, dto.Name, dto.Description, dto.Tags);
            await dispatcher.SendAsync(command, token);
            return Results.Created($"exercise/{id}", id);
        });

        return app;
    }
}