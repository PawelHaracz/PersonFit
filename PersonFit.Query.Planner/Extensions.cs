using PersonFit.Query.Planner.Infrastructure.Postgres;
namespace PersonFit.Query.Planner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Core.Queries;
using Api;
using Application.Dtos;
using Application.Queries;
using Application.Queries.QueryDispatcher;

public static class Extensions
{
    public static WebApplicationBuilder RegisterPlannerQueries(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PostgresPlannerReadContext>();

        builder.Services
            .AddScoped<IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>, GetPlannerQueryHandler>();
        return builder;
    }

    public static WebApplication UsePlannerQueries(this WebApplication app)
    {
        return app.UseQueryPlannerDomainApi();
    }
}