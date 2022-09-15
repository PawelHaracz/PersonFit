namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record ReprderRepetitionEvent(int OldOrder, int NewOrder): IDomainEvent{ }