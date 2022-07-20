namespace PersonFit.Core.Aggregations
{
    public interface IIdentifiable<T> 
    {
        T Id { get; set; }
    }
}