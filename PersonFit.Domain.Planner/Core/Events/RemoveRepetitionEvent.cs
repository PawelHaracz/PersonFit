namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record RemoveRepetitionEvent(int Order): IDomainEvent{ }