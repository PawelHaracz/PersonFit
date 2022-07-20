

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PersonFit.Domain.Exercise;

using PersonFit.Core;
using PersonFit.Core.Commands;
using Application.Commands;
using Application.Commands.CommandHandlers;
using Core.Repositories;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Documents;
using Infrastructure.Postgres.Repositories;
using Api.Dtos;
public static class Extensions
{
    public static WebApplicationBuilder RegisterExerciseDomain(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PostgresContext>();
        
        builder.Services.AddScoped<ICommandHandler<AddExerciseCommand>, AddExerciseCommandHandler>();
        builder.Services.AddScoped<IPostgresRepository<ExerciseDocument, Guid>, ExercisePostgresRepository>();
        builder.Services.AddScoped<IExerciseRepository, ExerciseDomainRepository>();
        
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