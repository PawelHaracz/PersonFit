namespace PersonFit.Domain.Planner.Api.Dtos.Commands.PlannerExercise;

public record RemoveExerciseRepetitionsCommandDto(IEnumerable<int> RepetitionOrders);