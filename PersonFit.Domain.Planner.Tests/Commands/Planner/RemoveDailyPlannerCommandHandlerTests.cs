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

public class RemoveDailyPlannerCommandHandlerTests
{
    [Fact]
    public Task given_remove_daily_planner_that_exists_should_perform()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task given_remove_daily_planner_that_not_exists_should_ignore()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task given_remove_daily_planner_to_not_active_should_throw()
    {
        throw new NotImplementedException();
    }
    
    private readonly ICommandHandler<RemoveDailyPlannerCommand> _handler;
    private readonly IPlannerRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public RemoveDailyPlannerCommandHandlerTests()
    {
        _repository = Substitute.For<IPlannerRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new RemoveDailyPlannerCommandHandler(new PlannerPolicyEvaluator(),  _repository, _eventProcessor);
    }
}