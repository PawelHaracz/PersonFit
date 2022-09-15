namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using Enums;
internal record AddRepetitionEvent(int Order, int Count, MeasurementUnit Unit): IDomainEvent{ }