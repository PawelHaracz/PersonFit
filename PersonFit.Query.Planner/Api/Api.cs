namespace PersonFit.Query.Planner.Api;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Queries;
using Application.Dtos;
using Application.Enums;
using Application.Queries;

internal static class  Api
{
    private static readonly Guid _ownerId = new ("FC8838FE-5A92-472C-8F0E-89BC39DDA978");
    private static readonly JsonSerializerOptions _options = new ()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
    public static WebApplication UseQueryPlannerDomainApi(this WebApplication app)
    {
        app.MapGet("/planner",
            async ([FromQuery] PlannerStatus status, HttpContext context, IQueryDispatcher dispatcher,
                CancellationToken token) =>
            {
                var query = new GetPlannerQuery(_ownerId, status);
                var results = await dispatcher.QueryAsync(query, token);

                return Results.Json(results, _options);
            });

        app.MapGet("/planner/{id:guid}",
            async (HttpContext context, Guid id, IQueryDispatcher dispatcher, CancellationToken token) =>
            {
                var query = new GetFullDailiesPlanQuery(_ownerId, id);
                var results = await dispatcher.QueryAsync<FullDailiesPlannerDto>(query, token);
                
                return Results.Json(results, _options);
            });
        return app;
    }
}