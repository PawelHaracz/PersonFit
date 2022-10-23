namespace PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Core;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Exceptions;
using Core.Repositories;
internal class AddDailyPlannerCommandHandler : ICommandHandler<AddDailyPlannerCommand>
{
    private readonly IPlannerRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;
    private readonly IPolicyEvaluator<Core.Entities.Planner> _policyEvaluator;

    public AddDailyPlannerCommandHandler(IPolicyEvaluator<Core.Entities.Planner> policyEvaluator, IPlannerRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
        _policyEvaluator = policyEvaluator;
    }

    public async Task HandleAsync(AddDailyPlannerCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.GetById(command.PlannerId, command.OwnerId, token);
        var hasPerform = await _policyEvaluator.Evaluate(planner, token);

        if (hasPerform is false)
        {
            throw new CannotChangePlannerException(planner.Id, nameof(AddDailyPlannerCommandHandler));
        }
        
        planner.AddExercises(command.DayOfWeek, command.TimeOfDay, command.Exercises);

        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}