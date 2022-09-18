namespace PersonFit.Application;
using Microsoft.AspNetCore.Builder;
using Domain.Planner;
using Domain.Exercise;
using Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder RegisterApplication(this WebApplicationBuilder builder) 
        => builder
            .AddInfrastructure()
            .RegisterExerciseDomain()
            .RegisterPlannerDomain();

    public static WebApplication UseApplication(this WebApplication webApplication) =>
        webApplication
            .UseInfrastructure()
            .UseExerciseDomain()
            .UsePlannerDomain();
}