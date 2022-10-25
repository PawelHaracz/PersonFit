namespace PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Core.Commands;
using Dtos;
internal record  AddExerciseRepetitionsCommand(Guid Id, IEnumerable<ExerciseRepetitionDto> Repetitions) : ICommand {}