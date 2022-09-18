namespace PersonFit.Domain.Planner.Application.Commands.CommandHandlers;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Core.Repositories;

internal class AddExerciseRepetitionsCommandHandler : ICommandHandler<AddExerciseRepetitionsCommand>
{
    private readonly IExerciseRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public AddExerciseRepetitionsCommandHandler(IExerciseRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(AddExerciseRepetitionsCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.Get(command.Id, token);

        foreach (var dto in command.Repetitions)
        {
            planner.AddRepetition(dto.Count, dto.Unit, dto.Note);
        }        
        
        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}