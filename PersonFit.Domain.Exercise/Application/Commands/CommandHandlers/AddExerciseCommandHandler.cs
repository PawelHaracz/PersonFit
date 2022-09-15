using PersonFit.Core.Commands;
using PersonFit.Core.Events;

namespace PersonFit.Domain.Exercise.Application.Commands.CommandHandlers;
using PersonFit.Core;
using Core.Repositories;
using Exceptions;

internal class AddExerciseCommandHandler: ICommandHandler<AddExerciseCommand>
{
    private readonly IExerciseRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public AddExerciseCommandHandler(IExerciseRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }
    
    public async Task HandleAsync(AddExerciseCommand command, CancellationToken token = default)
    {
        var hasExist = await _domainRepository.Exist(command.Name, token);
        if (hasExist)
        {
            throw new ExerciseAlreadyCreatedException(command.Name);
        }
        var tags = command.Tags is not null ? command.Tags.ToArray() : Array.Empty<string>();
        var exercise = Core.Entities.Exercise.Create(command.Id, command.Name, command.Description, tags);

        await _domainRepository.Create(exercise, token);
        await _eventProcessor.ProcessAsync(exercise.Events, token);
    }
}