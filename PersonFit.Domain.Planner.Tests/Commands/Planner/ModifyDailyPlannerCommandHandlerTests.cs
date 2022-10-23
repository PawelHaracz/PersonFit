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
using Application.Dtos;
using Application.Enums;
using Application.Exceptions;
using Application.Policies;
using Core.Enums;
using Core.Events;
using Core.Repositories;
using Core.ValueObjects;
using Shouldly;
using Xunit;

public class ModifyDailyPlannerCommandHandlerTests
{
    [Fact]
    public async Task add_new_exercise_should_add()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");
        var exerciseId2 = new Guid("5DC7C7F9-FCF8-4EA3-8ACA-C52B4CC27ED4");
        
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, new []
            {
                new DailyPlanner(dayOfWeek, timeOfDay, new []{ exerciseId1 })
            });
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new []
        {
            new DailyPlannerModifierDto(exerciseId2, ActionModifier.Add)
        });
        
        await _handler.HandleAsync(command, token);
        
        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
        
        Assert.Collection(planner.Events, @event => @event.ShouldBeAssignableTo<AddedPlannerExerciseEvent>());
    }
    
    [Fact]
    public async Task remove_new_exercise_should_add()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");

        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, new []
            {
                new DailyPlanner(dayOfWeek, timeOfDay, new []{ exerciseId1 })
            });
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new []
        {
            new DailyPlannerModifierDto(exerciseId1, ActionModifier.Remove)
        });
        
        await _handler.HandleAsync(command, token);
        
        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
        
        Assert.Collection(planner.Events,
            @event => @event.ShouldBeAssignableTo<RemovedPlannerExerciseEvent>(),
            @event => @event.ShouldBeAssignableTo<RemovedDailyPlannerEvent>());
    }
    
    [Fact]
    public async Task invoke_action_on_inactive_planning_should_throw()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("80F84C3A-5A0F-459D-9E9E-2AA468AAEAC0");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");
        
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-2),
            DateTime.Now.AddDays(-1),
            PlannerStatus.Disabled, Enumerable.Empty<DailyPlanner>());
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);


        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new[]
        {
            new DailyPlannerModifierDto(exerciseId1, ActionModifier.Remove)
        });
        var exception = await Record.ExceptionAsync(async () => await _handler.HandleAsync(command, token));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<CannotChangePlannerException>();
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task add_and_remove_exercises_should_invoke()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");
        var exerciseId2 = new Guid("5DC7C7F9-FCF8-4EA3-8ACA-C52B4CC27ED4");

        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, new []
            {
                new DailyPlanner(dayOfWeek, timeOfDay, new []{ exerciseId1 })
            });
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new []
        {
            new DailyPlannerModifierDto(exerciseId1, ActionModifier.Remove),
            new DailyPlannerModifierDto(exerciseId2, ActionModifier.Add)
        });
        
        await _handler.HandleAsync(command, token);
        
        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
        
        Assert.Collection(planner.Events,
            @event => @event.ShouldBeAssignableTo<AddedPlannerExerciseEvent>(),
            @event => @event.ShouldBeAssignableTo<RemovedPlannerExerciseEvent>());
    }

    [Fact]
    public async Task add_and_remove_same_exercise_that_exists_should_ignore()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");
 
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, new []
            {
                new DailyPlanner(dayOfWeek, timeOfDay, new []{ exerciseId1 })
            });
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new []
        {
            new DailyPlannerModifierDto(exerciseId1, ActionModifier.Remove),
            new DailyPlannerModifierDto(exerciseId1, ActionModifier.Add)
        });
        
        await _handler.HandleAsync(command, token);
        
        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
        
        planner.Events.ShouldBeEmpty();
    }
    
    [Fact]
    public async Task add_and_remove_same_exercise_should_ignore()
    {
        var token = CancellationToken.None;
        var plannerId = new Guid("C94C8672-97CB-43C2-B05F-029BED80E7D9");
        var ownerId = new Guid("833CAA9E-A8AC-49F4-8919-0EDCCBD387B3");
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Morning;
        var exerciseId1 = new Guid("D6F07F06-797B-473A-B86E-719E419C1426");
        var exerciseId2 = new Guid("5DC7C7F9-FCF8-4EA3-8ACA-C52B4CC27ED4");
        var planner = new Core.Entities.Planner(
            plannerId,
            ownerId,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            PlannerStatus.Activate, new []
            {
                new DailyPlanner(dayOfWeek, timeOfDay, new []{ exerciseId1 })
            });
        
        _repository.GetById(ownerId, plannerId, token).Returns(planner);
        
        var command = new ModifyDailyPlannerCommand(plannerId, ownerId, dayOfWeek, timeOfDay, new []
        {
            new DailyPlannerModifierDto(exerciseId2, ActionModifier.Remove),
            new DailyPlannerModifierDto(exerciseId2, ActionModifier.Add)
        });
        
        await _handler.HandleAsync(command, token);
        
        await _repository.Received(1).Update(Arg.Is<Core.Entities.Planner>(p => p.Id.Value == plannerId), token);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(planner.Events)), token);
        await _repository.Received(1).GetById(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<Guid>(g => g == plannerId),
            Arg.Any<CancellationToken>());
        
        planner.Events.ShouldBeEmpty();
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