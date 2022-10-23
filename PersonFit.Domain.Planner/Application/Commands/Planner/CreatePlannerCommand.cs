namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;

internal record  CreatePlannerCommand(Guid Id, Guid OwnerId, DateOnly StartTime, DateOnly EndTime): ICommand {}
