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
        throw new NotImplementedException();
    }
}