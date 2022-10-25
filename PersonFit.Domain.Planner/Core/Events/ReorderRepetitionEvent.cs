namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record ReorderRepetitionEvent(int OldOrder, int NewOrder): IDomainEvent{ }