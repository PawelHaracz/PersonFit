namespace PersonFit.Infrastructure.Exceptions;

public class ItemNotExistDatabaseException : InfraException
{
    public Guid Id { get; }
    public string EntityName { get; }
    public override string Code{ get; } = "database_does_not_have_item";

    public ItemNotExistDatabaseException(Guid id, string entityName) : base($"database does not have item with id {id}")
    {
        Id = id;
        EntityName = entityName;
    }
}