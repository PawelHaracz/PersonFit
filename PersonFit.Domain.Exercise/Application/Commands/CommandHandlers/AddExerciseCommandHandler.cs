namespace PersonFit.Domain.Exercise.Application.Commands.CommandHandlers;
using PersonFit.Core;

internal class AddExerciseCommandHandler: ICommandHandler<AddExerciseCommand>
{
    private readonly IDomainRepository<Core.Entities.Exercise> _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public AddExerciseCommandHandler(IDomainRepository<Core.Entities.Exercise> domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }
    
    public async Task HandleAsync(AddExerciseCommand command)
    {
        var tags = command.Tags is not null ? command.Tags.ToArray() : Array.Empty<string>();
        var exercise = Core.Entities.Exercise.Create(Guid.NewGuid(), command.Name, command.Description, tags);

        await _domainRepository.AddAsync(exercise);
        await _eventProcessor.ProcessAsync(exercise.Events);
    }
}