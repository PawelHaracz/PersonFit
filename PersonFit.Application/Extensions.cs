
namespace PersonFit.Application;
using Microsoft.AspNetCore.Builder;
using Domain.Planner;
using Domain.Exercise;
using Query.Planner;
using Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder RegisterApplication(this WebApplicationBuilder builder)
        => builder
            .AddInfrastructure()
            .RegisterExerciseDomain()
            .RegisterPlannerDomain()
            .RegisterPlannerQueries();

    public static WebApplication UseApplication(this WebApplication webApplication) =>
        webApplication
            .UseInfrastructure()
            .UseExerciseDomain()
            .UsePlannerDomain()
            .UsePlannerQueries();
}