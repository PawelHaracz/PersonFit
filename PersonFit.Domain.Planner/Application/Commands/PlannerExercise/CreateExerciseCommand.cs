namespace PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Core.Commands;
using Dtos;
internal record CreateExerciseCommand(Guid Id, Guid OwnerId, Guid ExerciseId, IEnumerable<ExerciseRepetitionDto> Repetition) : ICommand
{
    
}