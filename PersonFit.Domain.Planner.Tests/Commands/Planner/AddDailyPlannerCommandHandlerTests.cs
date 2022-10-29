namespace PersonFit.Domain.Planner.Tests.Commands.Planner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Tests.Extensions;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using Application.Exceptions;
using Application.Policies;
using Core.Enums;
using Core.Repositories;
using Core.ValueObjects;
using Shouldly;
using Xunit;
using Core.Events;
using PersonFit.Core.Enums;
public class AddDailyPlannerCommandHandlerTests
{

    [Fact]
    public async Task given_new_daily_planner_should_add()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseIds = new[] { new Guid("8D5284CC-1B65-4006-8025-10457A37AD76") };
        
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, Enumerable.Empty<DailyPlanner>());

        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new AddDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, exerciseIds);
        await _handler.HandleAsync(command, token);

        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        
        Assert.Collection(planner.Events, @event => @event.ShouldBeAssignableTo<AddedPlannerExerciseEvent>());
        
    }
    
    [Fact]
    public async Task given_new_daily_planner_to_not_active_should_throw()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("80F84C3A-5A0F-459D-9E9E-2AA468AAEAC0");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseIds = new[] { new Guid("8D5284CC-1B65-4006-8025-10457A37AD76") };
        
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-2),
            DateTime.Now.AddDays(-1),
            PlannerStatus.Disabled, Enumerable.Empty<DailyPlanner>());
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new AddDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, exerciseIds);
        var exception = await Record.ExceptionAsync(async () => await _handler.HandleAsync(command, token));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<CannotChangePlannerException>();
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
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