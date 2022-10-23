using System;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using PersonFit.Domain.Planner.Application.Policies;
using PersonFit.Domain.Planner.Core.Repositories;
using Xunit;

namespace PersonFit.Domain.Planner.Tests.Commands.Planner;

public class AddDailyPlannerCommandHandlerTests
{

    [Fact]
    public Task given_new_daily_planner_should_add()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task given_new_daily_planner_to_not_active_should_throw()
    {
        throw new NotImplementedException();
    }
    
    private readonly ICommandHandler<AddDailyPlannerCommand> _handler;
    private readonly IPlannerRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public AddDailyPlannerCommandHandlerTests()
    {
        _repository = Substitute.For<IPlannerRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new AddDailyPlannerCommandHandler(new PlannerPolicyEvaluator(),  _repository, _eventProcessor);
    }
}