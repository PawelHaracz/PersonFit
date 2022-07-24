using PersonFit.Domain.Exercise.Api.Dtos.Queries;

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

public static class Extensions
{
    public static WebApplicationBuilder RegisterExerciseDomain(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PostgresContext>();
        
        builder.Services.AddScoped<ICommandHandler<AddExerciseCommand>, AddExerciseCommandHandler>();

        builder.Services.AddScoped<IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>, GetExercisesQueryHandler>();
        
        builder.Services.AddScoped<IPostgresRepository<ExerciseDocument, Guid>, ExercisePostgresRepository>();
        builder.Services.AddScoped<IExerciseRepository, ExerciseDomainRepository>();
        builder.Services.AddScoped<IReadExerciseRepository, ReadExerciseRepository>();
        
        return builder;
    }

    public static WebApplication UseExerciseDomain(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<PostgresContext>();    
                context.Database.Migrate();
            }
        }
        
        app.MapGet("/exercise", async (HttpContext context ,  CancellationToken token, IQueryDispatcher dispatcher) =>
        {
            var query = await dispatcher.QueryAsync(new GetExercisesQuery(), token);
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