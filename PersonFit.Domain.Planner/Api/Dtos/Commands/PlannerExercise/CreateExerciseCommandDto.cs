namespace PersonFit.Domain.Planner.Api.Dtos.Commands.PlannerExercise;
using PersonFit.Domain.Planner.Application.Dtos;

public record CreateExerciseCommandDto(Guid ExerciseId, IEnumerable<ExerciseRepetitionDto> Repetitions) { }