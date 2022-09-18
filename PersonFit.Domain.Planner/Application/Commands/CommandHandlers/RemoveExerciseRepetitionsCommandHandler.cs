namespace PersonFit.Domain.Planner.Application.Commands.CommandHandlers;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Core.Repositories;

internal class RemoveExerciseRepetitionsCommandHandler : ICommandHandler<RemoveExerciseRepetitionsCommand>
{
    private readonly IExerciseRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public RemoveExerciseRepetitionsCommandHandler(IExerciseRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(RemoveExerciseRepetitionsCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.Get(command.Id, token);

        foreach (var order in command.RepetitionOrders)
        {
            planner.RemoveRepetition(order);
        }        
        
        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}