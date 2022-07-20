using PersonFit.Core.Aggregations;
using PersonFit.Core.Events;

namespace PersonFit.Domain.Exercise.Tests.Commands.Exercise;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PersonFit.Domain.Exercise.Application.Commands;
using PersonFit.Domain.Exercise.Application.Commands.CommandHandlers;
using NSubstitute;
using PersonFit.Core;
using Core.Repositories;
using Xunit;
using System.Collections.Generic;
using Extensions;
using Application.Exceptions;
using Shouldly;
public class AddExerciseCommandTests
{
    
   [Fact]
    public async Task given_add_command_should_create_exercise()
    {
        var cancellationToken = CancellationToken.None;
        var command = new AddExerciseCommand(new AggregateId(), "Plank", "plank is exercise", ArraySegment<string>.Empty);
        var exercise = Core.Entities.Exercise.Create(command.Id, command.Name, command.Description, command.Tags.ToArray());
        
        await _handler.HandleAsync(command, cancellationToken);
        
        await _repository.Received(1).Create(Arg.Is<Core.Entities.Exercise>(e => 
            e.Compare( exercise)), cancellationToken);
        await _eventProcessor.Received().ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
            events => events.CompareArrays(exercise.Events)), 
            cancellationToken);
    }

    [Fact]
    public async Task exercise_already_created_given_add_command_should_throw_exception()
    {
        var cancellationToken = CancellationToken.None;
        var command = new AddExerciseCommand(new AggregateId(), "Plank", "plank is exercise", ArraySegment<string>.Empty);
     
        _repository.Exist(command.Name, cancellationToken).Returns(true);
        
        var exception = await Record.ExceptionAsync(async () =>  await _handler.HandleAsync(command, cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ExerciseAlreadyCreatedException>();
    }
    
    private readonly AddExerciseCommandHandler _handler;
    private readonly IExerciseRepository _repository;
    private readonly IEventProcessor _eventProcessor;
    
    public AddExerciseCommandTests()
    {
        _repository = Substitute.For<IExerciseRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new AddExerciseCommandHandler(_repository, _eventProcessor);
    }
}