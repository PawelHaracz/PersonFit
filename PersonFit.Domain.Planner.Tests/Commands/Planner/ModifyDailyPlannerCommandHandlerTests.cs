using NSubstitute;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Domain.Planner.Application.Policies;
using PersonFit.Domain.Planner.Core.Repositories;

namespace PersonFit.Domain.Planner.Tests.Commands.Planner;

public class ModifyDailyPlannerCommandHandlerTests
{
    private readonly ICommandHandler<ModifyDailyPlannerCommand> _handler;
    private readonly IPlannerRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public ModifyDailyPlannerCommandHandlerTests()
    {
        _repository = Substitute.For<IPlannerRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new ModifyDailyPlannerCommandHandler(new PlannerPolicyEvaluator(),  _repository, _eventProcessor);
    }
}