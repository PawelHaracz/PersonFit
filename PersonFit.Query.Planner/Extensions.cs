namespace PersonFit.Query.Planner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Api;
using Core;
using Core.Queries;
using Application.Dtos;
using Application.Queries;
using Application.Queries.QueryDispatcher;
using Infrastructure.Postgres;
public static class Extensions
{
    public static WebApplicationBuilder RegisterPlannerQueries(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReadDbContext, PostgresPlannerReadContext>();

        builder.Services
            .AddScoped<IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>, GetPlannerQueryHandler>()
            .AddScoped<IQueryHandler<GetFullDailiesPlanQuery, FullDailiesPlannerDto>, GetFullDailiesPlanQueryHandler>();
        
        return builder;
    }

    public static WebApplication UsePlannerQueries(this WebApplication app)
    {
        return app.UseQueryPlannerDomainApi();
    }
}