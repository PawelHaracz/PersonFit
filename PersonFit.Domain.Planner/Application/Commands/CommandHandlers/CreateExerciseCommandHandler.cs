namespace PersonFit.Domain.Planner.Application.Commands.CommandHandlers;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using Exceptions;
using Core.Entities;
using Core.Repositories;

internal class CreateExerciseCommandHandler : ICommandHandler<CreateExerciseCommand>
{
    private readonly IExerciseRepository _domainRepository;
    private readonly IEventProcessor _eventProcessor;

    public CreateExerciseCommandHandler(IExerciseRepository domainRepository, IEventProcessor eventProcessor)
    {
        _domainRepository = domainRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(CreateExerciseCommand command, CancellationToken token = default)
    {
        var hasExist = await _domainRepository.Exists(command.OwnerId, command.ExerciseId, token);
        if (hasExist != Guid.Empty)
        {
            throw new ExerciseAlreadyCreatedException(command.OwnerId, command.ExerciseId,hasExist);
        }
        var planner = PlannerExercise.Create(command.Id ,command.OwnerId, command.ExerciseId);
        foreach (var exerciseRepetitionDto in command.Repetition)
        {
            planner.AddRepetition(exerciseRepetitionDto.Count, exerciseRepetitionDto.Unit, exerciseRepetitionDto.Note);
        }
        
        await _domainRepository.Create(planner, token);
        await _eventProcessor.ProcessAsync(planner.Events, token);
    }
}