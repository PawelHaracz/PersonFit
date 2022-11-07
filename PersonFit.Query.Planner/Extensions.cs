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
using Application.Daos;
using Infrastructure.Mappers;
public static class Extensions
{
    public static WebApplicationBuilder RegisterPlannerQueries(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReadDbContext, PostgresPlannerReadContext>();

        builder.Services
            .AddScoped<IQueryHandler<GetPlannerQuery, IEnumerable<PlannersDto>>, GetPlannerQueryHandler>()
            .AddScoped<IQueryHandler<GetFullDailiesPlanQuery, FullDailiesPlannerDto>, GetFullDailiesPlanQueryHandler>();

        builder.Services
            .AddSingleton<IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>>, PlannerMapper>()
            .AddSingleton<IDaoToDtoMapper<IEnumerable<QueryFullDailiesPlannerDao>, FullDailiesPlannerDto>,
                FullDailiesPlannerMapper>();
        return builder;
    }

    public static WebApplication UsePlannerQueries(this WebApplication app)
    {
        return app.UseQueryPlannerDomainApi();
    }
}