using PersonFit.Domain.Planner.Core.Enums;

namespace PersonFit.Domain.Planner.Api.Dtos.Commands;

//todo implent it
public record AddDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<ExerciseWorkoutDto> Workouts){}