namespace PersonFit.Domain.Planner.Core.Entities;
using PersonFit.Core.Aggregations;
using Enums;
using Exceptions;
using ValueObjects;
using Events;

internal class PlannerExercise : AggregateRoot, IAggregateRoot
{
    public Guid ExerciseId { get; private set; }

    public IEnumerable<Repetition> Repetitions
    {
        get => _repetitions.AsEnumerable();
        private init => _repetitions = new HashSet<Repetition>(value);
    }

    private readonly ISet<Repetition> _repetitions;
    

    public PlannerExercise(Guid id, Guid exerciseId, IEnumerable<Repetition> repetitions, int version = 0)
    {
        Id = id;
        ExerciseId = exerciseId;
        Repetitions = repetitions;
        Version = version;
    }

    public static PlannerExercise Create(AggregateId id, Guid exerciseId)
    {
        if (exerciseId == Guid.Empty)
        {
            throw new InvalidExerciseIdPlannerExerciseException();
        }
        var plannerExercise = new PlannerExercise(id, exerciseId, Enumerable.Empty<Repetition>());
        plannerExercise.AddEvent(new PlannerExerciseCreatedEvent(id, exerciseId));

        return plannerExercise;
    }

    public int AddRepetition(int count, MeasurementUnit unit, string note)
    {
        if (count < 0)
        {
            count = 0;
        }
        var order = _repetitions.Count + 1;
        _repetitions.Add(new Repetition(order, count, unit, note));
        AddEvent(new AddedRepetitionEvent(order, count, unit, note));
        return order;
    }

    public void RemoveRepetition(int order)
    {
        if (order < 1)
        {
            throw new RemoveExerciseRepetitionException(Id, order);
        }
        var item = _repetitions.SingleOrDefault(repetition => repetition.Order == order, Repetition.Default);
        if (Repetition.Default.Equals(item))
        {
            throw new RemoveExerciseRepetitionException(Id, order);
        }

        _repetitions.Remove(item);
       
        int newOrder = 1;
        var newRepetitions = _repetitions.ToArray();
        _repetitions.Clear();
        foreach (var repetition in newRepetitions)
        {
            repetition.ChangeOrder(newOrder);
            _repetitions.Add(repetition);
            newOrder++;
        }
        
        AddEvent(new RemovedRepetitionEvent(order, item.Count,item.Unit, item.Note));
    }

    public void ReorderRepetitions(IDictionary<int, int> orderingMapping)
    {
        var repetitions = _repetitions.ToArray();
        var tempRepetitions = new List<Repetition>();
        _repetitions.Clear();
        foreach (var repetition in repetitions)
        {
            var oldOrder = repetition.Order;
            if (!orderingMapping.ContainsKey(oldOrder))
            {
                throw new MissingOrderKeyException(Id, repetition.Order);
            }
            var newOrder = orderingMapping[oldOrder];
            
            if (newOrder < 1 || newOrder > repetitions.Length + 1)
            {
                throw new ReorderingExerciseRepetitionException(Id, oldOrder, newOrder);
            }
            
            if(oldOrder != newOrder)
            {
                repetition.ChangeOrder(newOrder);
                AddEvent(new ReorderRepetitionEvent(oldOrder, newOrder));
            }
            tempRepetitions.Add(repetition);
        }
        foreach (var repetition in tempRepetitions.OrderBy(r => r.Order))
        {
            _repetitions.Add(repetition);
        }
    }
}