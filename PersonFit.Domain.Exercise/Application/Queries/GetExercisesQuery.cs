using PersonFit.Core.Commands;

namespace PersonFit.Domain.Exercise.Application.Queries;
using PersonFit.Core.Queries;
using Dtos;

public record GetExercisesQuery : IQuery<IEnumerable<ExerciseDto>>, ICommand;


