namespace PersonFit.Application;
using Microsoft.AspNetCore.Builder;
using Domain.Exercise;
using Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder RegisterApplication(this WebApplicationBuilder builder) 
        => builder
            .AddInfrastructure()
            .RegisterExerciseDomain();

    public static WebApplication UseApplication(this WebApplication webApplication) =>
        webApplication
            .UseInfrastructure()
            .UseExerciseDomain();
}