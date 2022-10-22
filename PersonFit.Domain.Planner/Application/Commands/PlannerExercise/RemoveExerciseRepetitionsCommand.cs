namespace PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Core.Commands;

internal record  RemoveExerciseRepetitionsCommand(Guid Id, IEnumerable<int> RepetitionOrders) : ICommand {}