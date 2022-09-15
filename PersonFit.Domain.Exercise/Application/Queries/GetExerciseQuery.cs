namespace PersonFit.Domain.Exercise.Application.Queries;
using PersonFit.Core.Queries;
using Dtos;

public record GetExerciseQuery(Guid Id) : IQuery<ExerciseSummaryDto>;