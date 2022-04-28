namespace PersonFit.Domain.Exercise.Application.Exceptions;

public abstract class AppException : Exception
{
    public virtual string Code { get; }

    protected AppException(string message) : base(message)
    {
    }
}