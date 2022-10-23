namespace PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Core;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Enums;
using Exceptions;
using Core.Repositories;

internal class ModifyDailyPlannerCommandHandler : ICommandHandler<ModifyDailyPlannerCommand>
{
    private readonly IPolicyEvaluator<Core.Entities.Planner> _policyEvaluator;
    private readonly IPlannerRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public ModifyDailyPlannerCommandHandler(IPolicyEvaluator<Core.Entities.Planner> policyEvaluator, IPlannerRepository domainRepository, IEventProcessor eventProcessor)
    {
        _policyEvaluator = policyEvaluator;
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(ModifyDailyPlannerCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.GetById(command.OwnerId, command.PlannerId, token);
        var hasPerform = await _policyEvaluator.Evaluate(planner, token);

        if (hasPerform is false)
        {
            throw new CannotChangePlannerException(planner.Id, nameof(ModifyDailyPlannerCommandHandler));
        }
        
        var grouped = command.Exercises
            .GroupBy(e => e.Modifier).ToArray();
        
        var toAdd = grouped.SingleOrDefault(e => e.Key == ActionModifier.Add)?.Select(e => e.ExerciseId).ToArray() ?? Array.Empty<Guid>();
        var toRemove = grouped.SingleOrDefault(e => e.Key == ActionModifier.Remove)?.Select(e => e.ExerciseId).ToArray() ?? Array.Empty<Guid>();
        
        if (toAdd.Any())
        {
            planner.AddExercises(command.DayOfWeek, command.TimeOfDay, toAdd.Where(r => !toRemove.Contains(r)));
        }

        
        if (toRemove.Any())
        {
            planner.RemoveExercises(command.DayOfWeek, command.TimeOfDay, toRemove.Where(r => !toAdd.Contains(r)));
        }
        
   
        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}