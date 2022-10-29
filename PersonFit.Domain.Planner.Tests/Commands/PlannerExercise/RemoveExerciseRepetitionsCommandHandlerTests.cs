namespace PersonFit.Domain.Planner.Tests.Commands.PlannerExercise;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Aggregations;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Tests.Extensions;
using Core.Repositories;
using Core.ValueObjects;
using Extensions;
using Xunit;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise.CommandHandlers;
using PersonFit.Core.Enums;

public class RemoveExerciseRepetitionsCommandHandlerTests
{
    [Fact]
    public async Task given_remove_repetition_command_should_perform()
    {
        var cancellationToken = CancellationToken.None;
        var planner = new Core.Entities.PlannerExercise(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"), 
            new []
            {
                new Repetition(1, 12, MeasurementUnit.Length, "1"),
                new Repetition(2, 1, MeasurementUnit.None, string.Empty),
                new Repetition(3, 10, MeasurementUnit.Mass, "2"),
            });
        var excepted  =  new Core.Entities.PlannerExercise(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"), 
            new []
            {
                new Repetition(1, 12, MeasurementUnit.Length, "1"),
                new Repetition(2, 10, MeasurementUnit.Mass, "2"),
            });
        var command = new RemoveExerciseRepetitionsCommand(planner.Id, new []{ 2 });
        
        _repository.Get(Arg.Is<Guid>(g => g == planner.Id.Value), cancellationToken).Returns(planner);
       
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Update(Arg.Is<Core.Entities.PlannerExercise>(e => 
            e.Compare(excepted)), cancellationToken);
        
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(planner.Events)), cancellationToken);
    }
    
    private readonly ICommandHandler<RemoveExerciseRepetitionsCommand> _handler;
    private readonly IExerciseRepository _repository;
    private readonly IEventProcessor _eventProcessor;
    
    public RemoveExerciseRepetitionsCommandHandlerTests()
    {
        _repository = Substitute.For<IExerciseRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new RemoveExerciseRepetitionsCommandHandler(_repository, _eventProcessor);
    }
}