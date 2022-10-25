using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonFit.Core.Queries;
using PersonFit.Query.Planner.Application.Enums;
using PersonFit.Query.Planner.Application.Queries;

namespace PersonFit.Query.Planner.Api;

internal static class  Api
{
    private static readonly Guid _ownerId = new ("FC8838FE-5A92-472C-8F0E-89BC39DDA978");

    public static WebApplication UseQueryPlannerDomainApi(this WebApplication app)
    {
        app.MapGet("/planner",
            async ([FromQuery] PlannerStatus status, HttpContext context, IQueryDispatcher dispatcher,
                CancellationToken token) =>
            {
                var query = new GetPlannerQuery(_ownerId, status);
                var results = await dispatcher.QueryAsync(query, token);

                return Results.Json(results);
            });

        app.MapGet("/planner/{id:guid}",
            async (HttpContext context, Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
            {
                return Results.NoContent();
            });
        
                
        app.MapGet("/planner/exercise/{id:guid}", async (HttpContext context ,  Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        app.MapGet("/planner/exercise", async (HttpContext context , IQueryDispatcher dispatcher, CancellationToken token) =>
        {
            return Results.NoContent();
        });
        
        return app;
    }
}