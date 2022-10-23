namespace PersonFit.Domain.Planner.Tests.Commands.Planner;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Aggregations;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Tests.Extensions;
using PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Domain.Planner.Application.Commands.Planner.CommandHandlers;
using Application.Exceptions;
using Core.Repositories;
using Extensions;
using Shouldly;
using Xunit;

public class CreatePlannerCommandHandlerTests
{
    [Fact]
    public async Task given_create_command_should_create_planner()
    {
        var cancellationToken = CancellationToken.None;
        var command = new CreatePlannerCommand(new AggregateId(), new Guid("F5C3C417-530B-4AB0-8B12-5B604C3C2524"), new DateOnly(2022, 9, 10), new DateOnly(2022, 12, 10));
        var planner = Core.Entities.Planner.Create(command.Id, command.OwnerId, command.StartTime.ToDateTime(TimeOnly.MinValue), command.EndTime.ToDateTime(TimeOnly.MinValue));
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Create(Arg.Is<Core.Entities.Planner>(e => 
            e.Compare( planner)), cancellationToken);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(planner.Events)), 
            cancellationToken);
    }
    
    [Fact]
    public async Task given_create_command_but_planner_exists_should_throw()
    {
        var cancellationToken = CancellationToken.None;
        var command = new CreatePlannerCommand(new AggregateId(), new Guid("F5C3C417-530B-4AB0-8B12-5B604C3C2524"), new DateOnly(2022, 9, 10), new DateOnly(2022, 12, 10));
        
        _repository.GetActivatedPlannerId(command.OwnerId,  command.StartTime.ToDateTime(TimeOnly.MinValue),
            command.EndTime.ToDateTime(TimeOnly.MinValue), cancellationToken)
        .Returns(new Guid("B2B98E40-27C7-4D0B-A7B4-0BB5299F8E85"));
        
        var exception =  await Record.ExceptionAsync(async () =>  await _handler.HandleAsync(command, cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<PlannerAlreadyCreatedException>();
        await _repository.Received(1).GetActivatedPlannerId(
            Arg.Is<Guid>(g => g == command.OwnerId),
            Arg.Is<DateTime>(d => d == command.StartTime.ToDateTime(TimeOnly.MinValue)),
            Arg.Is<DateTime>(d => d == command.EndTime.ToDateTime(TimeOnly.MinValue)),
            Arg.Any<CancellationToken>());
    }
    
    private readonly ICommandHandler<CreatePlannerCommand> _handler;
    private readonly IPlannerRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public CreatePlannerCommandHandlerTests()
    {
        _repository = Substitute.For<IPlannerRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new CreatePlannerCommandHandler(_repository, _eventProcessor);
    }
}