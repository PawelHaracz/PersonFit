namespace PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Core;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Exceptions;
using Core.Repositories;
internal class RemoveDailyPlannerCommandHandler: ICommandHandler<RemoveDailyPlannerCommand>
{
    private readonly IPolicyEvaluator<Core.Entities.Planner> _policyEvaluator;
    private readonly IPlannerRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public RemoveDailyPlannerCommandHandler(IPolicyEvaluator<Core.Entities.Planner> policyEvaluator, IPlannerRepository domainRepository, IEventProcessor eventProcessor)
    {
        _policyEvaluator = policyEvaluator;
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(RemoveDailyPlannerCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.GetById(command.OwnerId, command.PlannerId, token);
        var hasPerform = await _policyEvaluator.Evaluate(planner, token);

        if (hasPerform is false)
        {
            throw new CannotChangePlannerException(planner.Id, nameof(RemoveDailyPlannerCommandHandler));
        }

        var status  = planner.RemoveDailyPlanner(command.DayOfWeek, command.TimeOfDay);

        if (status is false)
        {
            return;
        }
        
        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}