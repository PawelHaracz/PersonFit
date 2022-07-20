namespace PersonFit.Infrastructure.Exceptions;

public abstract class InfraException: Exception
{
    public virtual string Code { get; }
    protected InfraException(string message): base(message)
    {
    }
}