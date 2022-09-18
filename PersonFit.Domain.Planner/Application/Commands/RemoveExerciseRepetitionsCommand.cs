using PersonFit.Core.Commands;

namespace PersonFit.Domain.Planner.Application.Commands;

internal record  RemoveExerciseRepetitionsCommand(Guid Id, IEnumerable<int> RepetitionOrders) : ICommand {}