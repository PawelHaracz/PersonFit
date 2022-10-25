namespace PersonFit.Domain.Planner.Application.Dtos;
using Enums;

internal record DailyPlannerModifierDto(Guid ExerciseId, ActionModifier Modifier){}