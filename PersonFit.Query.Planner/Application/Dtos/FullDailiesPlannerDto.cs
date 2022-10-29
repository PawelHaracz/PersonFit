namespace PersonFit.Query.Planner.Application.Dtos;
using Enums;
using PersonFit.Core.Enums;
using System.Text.Json.Serialization;
using PersonFit.Infrastructure;
using ValueObjects;

public record FullDailiesPlannerDto(Guid Id, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly StartTime, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly EndTime, PlannerStatus Status,
    IEnumerable<DailyPlan> DailyPlans);

public record DailyPlan(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<Exercise> Exercises);

public record Exercise(string Name, string Description, IEnumerable<Repetition> Repetitions, IEnumerable<string> Tags, IEnumerable<string> Media);