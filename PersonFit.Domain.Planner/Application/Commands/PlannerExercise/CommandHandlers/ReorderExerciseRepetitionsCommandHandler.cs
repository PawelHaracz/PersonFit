namespace PersonFit.Domain.Planner.Application.Commands.PlannerExercise.CommandHandlers;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Exceptions;
using Core.Repositories;

internal class ReorderExerciseRepetitionsCommandHandler : ICommandHandler<ReorderExerciseRepetitionsCommand>
{
    private readonly IExerciseRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public ReorderExerciseRepetitionsCommandHandler(IExerciseRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(ReorderExerciseRepetitionsCommand command, CancellationToken token = default)
    {
        var planner = await _domainRepository.Get(command.Id, token);

        var dictionary = new Dictionary<int, int>();
        foreach (var repetitionReorderDto in command.RepetitionReorder)
        {
            if (dictionary.ContainsKey(repetitionReorderDto.Old))
            {
                throw new DuplicatedReorderingRepetitionOldKeyException(repetitionReorderDto.Old, repetitionReorderDto.New, dictionary[repetitionReorderDto.Old]);
            }
            dictionary.Add(repetitionReorderDto.Old, repetitionReorderDto.New);
        }        
        
        planner.ReorderRepetitions(dictionary);
        await _domainRepository.Update(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}