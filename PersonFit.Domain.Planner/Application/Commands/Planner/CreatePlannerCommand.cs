namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;

internal record  CreatePlannerCommand(Guid Id, Guid OwnerId, DateTime StartTime, DateTime EndTime): ICommand {}
