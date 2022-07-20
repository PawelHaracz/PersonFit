using PersonFit.Core.Events;

namespace PersonFit.Core.Aggregations
{
    public abstract class AggregateRoot<TIdType> where TIdType : struct 
    {
   
        private readonly ICollection<IDomainEvent> _events = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> Events => _events.AsEnumerable();
        public  TIdType Id { get; protected set; }
        public int Version { get; protected set; }
    
        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
            {
                Version++;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    
        public override bool Equals(object? entity)
            =>  entity is AggregateRoot<TIdType> @base && this == @base;

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(AggregateRoot<TIdType> entity1, AggregateRoot<TIdType> entity2)
        {
            return false;
        }

        public static bool operator !=(AggregateRoot<TIdType> entity1,
            AggregateRoot<TIdType> entity2)
        {
            return (!(entity1 == entity2));
        }
    
    }

    public abstract class AggregateRoot: AggregateRoot<AggregateId>
    {
    }
}