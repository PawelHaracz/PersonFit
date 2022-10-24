namespace PersonFit.Domain.Planner.Api;
using PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PersonFit.Core.Commands;
using PersonFit.Core.Queries;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Dtos;
using Application.Enums;
using Dtos.Commands.PlannerExercise;
using Application.Commands.PlannerExercise;

internal static class  Api
{
    private static readonly Guid _ownerId = new ("FC8838FE-5A92-472C-8F0E-89BC39DDA978"); 
    public static WebApplication UsePlannerDomainApi(this WebApplication app)
    {
        app.MapGet("/planner", async (HttpContext context ,  IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapGet("/planner/{id:guid}", async (HttpContext context ,  Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });

        app.MapPut("/planner", async (CreatePlannerCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var id = Guid.NewGuid();
            var command = new CreatePlannerCommand(id, _ownerId, dto.StartTime, dto.EndTime);
            await dispatcher.SendAsync(command, token);
            
            return Results.Created(new Uri($"planner/{id}", UriKind.Relative), id);
        });
        
        app.MapPut("/planner/{id:guid}/daily", async (Guid id, AddDailyPlannerCommandDto dto, ICommandDispatcher dispatcher, HttpContext context,
            CancellationToken token) =>
        {
            var command = new AddDailyPlannerCommand(id, _ownerId, dto.DayOfWeek, dto.TimeOfDay, dto.Workouts) ;
            await dispatcher.SendAsync(command, token);
            
            return Results.Accepted();
        });

        app.MapDelete("/planner/{id:guid}/daily", async (Guid id, RemovedDailyPlannerCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var command = new RemoveDailyPlannerCommand(id, _ownerId, dto.DayOfWeek, dto.TimeOfDay);
            await dispatcher.SendAsync(command, token);
            return Results.Accepted();
        });
        
        app.MapPost("/planner/{id:guid}/daily", async (Guid id, ModifyDailyPlannerCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var toRemove = dto.ToRemove?.Select(r => new DailyPlannerModifierDto(r, ActionModifier.Remove));
            var exercises = new List<DailyPlannerModifierDto>(toRemove ?? Enumerable.Empty<DailyPlannerModifierDto>());

            var toAdd = dto.ToAdd?.Select(g => new DailyPlannerModifierDto(g, ActionModifier.Add));
            exercises.AddRange(toAdd ?? Enumerable.Empty<DailyPlannerModifierDto>());
            
            var command = new ModifyDailyPlannerCommand(id, _ownerId, dto.DayOfWeek, dto.TimeOfDay, exercises);
            await dispatcher.SendAsync(command, token);
            return Results.Accepted();
        });
        
        app.MapGet("/planner/exercise/{id:guid}", async (HttpContext context ,  Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapGet("/planner/exercise", async (HttpContext context , IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapPut("/planner/exercise", async (CreateExerciseCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var id = Guid.NewGuid();
            var command = new CreateExerciseCommand(id, _ownerId, dto.ExerciseId, dto.Repetitions);
            await dispatcher.SendAsync(command, token);
            
            return Results.Created(new Uri($"planner/exercise/{id}", UriKind.Relative), id);
        });
        
        app.MapPut("/planner/exercise/{id:guid}", async (Guid id, AddExerciseRepetitionsCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var command = new AddExerciseRepetitionsCommand(id, dto.Repetitions);
            await dispatcher.SendAsync(command, token);

            return Results.Accepted($"/planner/exercise/{id}", id);
        });
        
        app.MapDelete("/planner/exercise/{id:guid}", async (Guid id, RemoveExerciseRepetitionsCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var command = new RemoveExerciseRepetitionsCommand(id, dto.RepetitionOrders);
            await dispatcher.SendAsync(command, token);

            return Results.Accepted($"/planner/exercise/{id}", id);
        });
        
        app.MapPost("/planner/exercise/{id:guid}", async (Guid id, ReorderExerciseRepetitionsCommandDto dto, ICommandDispatcher dispatcher,
            HttpContext context,
            CancellationToken token) =>
        {
            var command = new ReorderExerciseRepetitionsCommand(id, dto.RepetitionReorder);
            await dispatcher.SendAsync(command, token);

            return Results.Accepted($"/planner/exercise/{id}", id);
        });
        
        return app;
    }
}