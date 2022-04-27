namespace PersonFit.Core;

public interface IIdentifiable<T> 
{
    T Id { get; set; }
}