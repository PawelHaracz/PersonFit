using PersonFit.Domain.Planner.Core.Enums;

namespace PersonFit.Domain.Planner.Api.Dtos.Commands;

public record AddDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<ExerciseWorkoutDto> Workouts){}