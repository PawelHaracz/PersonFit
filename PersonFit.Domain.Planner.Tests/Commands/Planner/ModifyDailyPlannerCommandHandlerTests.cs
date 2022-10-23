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

public class ModifyDailyPlannerCommandHandlerTests
{

    [Fact]
    public Task add_new_exercise_should_add()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task remove_new_exercise_should_add()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task invoke_action_on_inactive_planning_should_throw()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public Task add_and_remove_exercises_should_invoke()
    {
        throw new NotImplementedException();
    }
    
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