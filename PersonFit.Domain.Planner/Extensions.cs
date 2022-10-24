namespace PersonFit.Domain.Planner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonFit.Core;
using PersonFit.Core.Commands;
using Core.Repositories;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Documents;
using Infrastructure.Postgres.Repositories;
using PersonFit.Infrastructure.Postgres;
using Application.Commands.PlannerExercise;
using Application.Commands.PlannerExercise.CommandHandlers;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using Api;
using Application.Policies;

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
        builder.Services.AddScoped<ICommandHandler<AddDailyPlannerCommand>, AddDailyPlannerCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<RemoveDailyPlannerCommand>, RemoveDailyPlannerCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<ModifyDailyPlannerCommand>, ModifyDailyPlannerCommandHandler>();

        // builder.Services.AddScoped<IQueryHandler<GetExercisesQuery, IEnumerable<ExerciseDto>>, GetExercisesQueryHandler>();
        // builder.Services.AddScoped<IQueryHandler<GetExerciseQuery, ExerciseSummaryDto>, GetExerciseQueryHandler>();

        builder.Services.AddScoped<IPolicyEvaluator<Core.Entities.Planner>, PlannerPolicyEvaluator>();
        
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
        
        return app.UsePlannerDomainApi();
    }
}