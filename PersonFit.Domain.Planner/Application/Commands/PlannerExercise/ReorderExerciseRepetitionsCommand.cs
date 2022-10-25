namespace PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Core.Commands;
using Dtos;

internal record ReorderExerciseRepetitionsCommand(Guid Id, IEnumerable<RepetitionReorderDto> RepetitionReorder): ICommand {}