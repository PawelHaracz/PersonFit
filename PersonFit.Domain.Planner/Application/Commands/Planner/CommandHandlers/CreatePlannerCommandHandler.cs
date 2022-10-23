using PersonFit.Domain.Planner.Application.Exceptions;

namespace PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Core.Repositories;

internal class CreatePlannerCommandHandler : ICommandHandler<CreatePlannerCommand>
{
    private readonly IPlannerRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public CreatePlannerCommandHandler(IPlannerRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(CreatePlannerCommand command, CancellationToken token = default)
    {
        var plannerId =
            await _domainRepository.GetActivatedPlannerId(
                command.OwnerId, 
                command.StartTime.ToDateTime(TimeOnly.MinValue), 
                command.EndTime.ToDateTime(TimeOnly.MinValue),
                token);
        
        if (plannerId != Guid.Empty)
        {
            throw new PlannerAlreadyCreatedException(command.OwnerId, command.StartTime, command.EndTime, plannerId);
        }
        
        var planner = Core.Entities.Planner.Create(command.Id, command.OwnerId, command.StartTime.ToDateTime(TimeOnly.MinValue), command.EndTime.ToDateTime(TimeOnly.MinValue));

        await _domainRepository.Create(planner, token);
       await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}