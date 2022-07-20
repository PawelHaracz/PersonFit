using PersonFit.Infrastructure.Exceptions;

namespace PersonFit.Domain.Exercise.Infrastructure.Exceptions;

internal class ExerciseNotExistDatabaseException : InfraException
{
    public override string Code { get; } = "Exercise_not_exist_database_exception";

    public ExerciseNotExistDatabaseException(Guid id):base($"Exercise with id {id} does not exists in the database")
    {
        
    }
}