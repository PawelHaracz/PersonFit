namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using PersonFit.Core.Enums;

internal record RemovedRepetitionEvent(int Order, int Count, MeasurementUnit Unit, string Note): IDomainEvent{ }