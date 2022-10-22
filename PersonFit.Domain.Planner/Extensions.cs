using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;

namespace PersonFit.Domain.Planner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonFit.Core;
using PersonFit.Core.Commands;
using PersonFit.Core.Queries;
using Core.Repositories;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Documents;
using Infrastructure.Postgres.Repositories;
using PersonFit.Infrastructure.Postgres;
using Application.Commands.PlannerExercise;
using Application.Commands.PlannerExercise.CommandHandlers;
public static class Extensions
{
        public static WebApplicationBuilder RegisterPlannerDomain(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PostgresPlannerDomainContext>();
        
        builder.Services.AddScoped<ICommandHandler<CreateExerciseCommand>,CreateExerciseCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<AddExerciseRepetitionsCommand>,AddExerciseRepetitionsCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<RemoveExerciseRepetitionsCommand>,RemoveExerciseRepetitionsCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<ReorderExerciseRepetitionsCommand>,ReorderExerciseRepetitionsCommandHandler>();

        builder.Services.AddScoped<ICommandHandler<CreatePlannerCommand>, CreatePlannerCommandHandler>();

        // builder.Services.AddScoped<IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>, GetExercisesQueryHandler>();
        // builder.Services.AddScoped<IQueryHandler<GetExerciseQuery, ExerciseSummaryDto>, GetExerciseQueryHandler>();
        
        builder.Services.AddScoped<IPostgresRepository<ExercisePlannerDocument, Guid>>(
            provider => new PostgresDomainRepository<ExercisePlannerDocument>(provider.GetRequiredService<PostgresPlannerDomainContext>()));
        builder.Services.AddScoped<IPostgresRepository<PlannerDocument, Guid>, PostgresDomainRepository<PlannerDocument>>(  
            provider => new PostgresDomainRepository<PlannerDocument>(provider.GetRequiredService<PostgresPlannerDomainContext>()));
        builder.Services.AddScoped<IExerciseRepository, ExercisePlannerDomainRepository>();
        builder.Services.AddScoped<IPlannerRepository, PlannerDomainRepository>();
        //builder.Services.AddScoped<IReadExerciseRepository, ReadExerciseRepository>();
        
        return builder;
    }

    public static WebApplication UsePlannerDomain(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<PostgresPlannerDomainContext>();    
               context.Database.Migrate();
            }
        }
        
        app.MapGet("/planner", async (HttpContext context ,  IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapGet("/planner/{id:guid}", async (HttpContext context ,  Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapGet("/planner/exercise/{id:guid}", async (HttpContext context ,  Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });

        //
        // app.MapPut("/planner/exercise", async (CreateExercise dto, ICommandDispatcher dispatcher, CancellationToken token) =>
        // {
        //     var id = Guid.NewGuid();
        //
        //     var command = new AddExerciseCommand(id, dto.Name, dto.Description, dto.Tags);
        //     await dispatcher.SendAsync(command, token);
        //     return Results.Created($"planner/exercise/{id}", id);
        // });
        //
        // app.MapPost("/planner/exercise/{id:guid}/add", 
        //     async (AddExerciseRepetitionsCommand dto, CancellationToken token, ICommandDispatcher dispatcher)
        // {
        //     
        // });
        return app;
    }
}