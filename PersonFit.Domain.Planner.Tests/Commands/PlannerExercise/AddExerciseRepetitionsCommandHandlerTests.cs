
namespace PersonFit.Domain.Planner.Tests.Commands.PlannerExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PersonFit.Core.Aggregations;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Tests.Extensions;
using Application.Dtos;
using Core.Repositories;
using Infrastructure.Postgres.Documents;
using Extensions;
using PersonFit.Infrastructure.Exceptions;
using Shouldly;
using Xunit;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise.CommandHandlers;
using PersonFit.Core.Enums;

public class AddExerciseRepetitionsCommandHandlerTests
{
    [Fact]
    public async Task given_add_repetition_command_should_perform()
    {
        var cancellationToken = CancellationToken.None;
        var planner = Core.Entities.PlannerExercise.Create(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"));
        var excepted  = Core.Entities.PlannerExercise.Create(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"));
        var command = new AddExerciseRepetitionsCommand(planner.Id, new []
        {
            new ExerciseRepetitionDto(12, MeasurementUnit.Length, string.Empty)
        });
        
        _repository.Get(Arg.Is<Guid>(g => g == planner.Id.Value), cancellationToken).Returns(planner);

        foreach (var dto in command.Repetitions)
        {
            excepted.AddRepetition(dto.Count, dto.Unit, dto.Note);
        }     
       
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Update(Arg.Is<Core.Entities.PlannerExercise>(e => 
            e.Compare(excepted)), cancellationToken);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(excepted.Events)), 
            cancellationToken);
    }
    
    [Fact]
    public async Task given_add_empty_repetition_command_should_perform()
    {
      
        var cancellationToken = CancellationToken.None;
        var planner = Core.Entities.PlannerExercise.Create(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"));
        var excepted  = Core.Entities.PlannerExercise.Create(new AggregateId(new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5")), new Guid("60B19A71-ACF8-4B59-8420-F1A3605CB641"), new Guid("7495BB29-33F3-476E-804B-EC8790A0E437"));
        var command = new AddExerciseRepetitionsCommand(planner.Id, Enumerable.Empty<ExerciseRepetitionDto>());
        
        _repository.Get(Arg.Is<Guid>(g => g == planner.Id.Value), cancellationToken).Returns(planner);
        
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Update(Arg.Is<Core.Entities.PlannerExercise>(e => 
            e.Compare(excepted)), cancellationToken);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(excepted.Events)), 
            cancellationToken);
    }
    
    [Fact]
    public async Task given_add_repetition_command_but_exercise_planner_does_not_exist_should_throw()
    {
        var cancellationToken = CancellationToken.None;
        var id = new Guid("72ADF6C0-FC8E-41B5-95A9-7641BDFFCAD5");
        _repository.Get(Arg.Is<Guid>(g => g == id), cancellationToken).Throws(info =>
            new ItemNotExistDatabaseException(info.ArgAt<Guid>(0), nameof(ExercisePlannerDocument)));
        
        var command = new AddExerciseRepetitionsCommand(id, new []
        {
            new ExerciseRepetitionDto(12, MeasurementUnit.Length, string.Empty)
        });

        var exception = await Record.ExceptionAsync(async ()=>  await _handler.HandleAsync(command, cancellationToken));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ItemNotExistDatabaseException>();
    }
    
    private readonly ICommandHandler<AddExerciseRepetitionsCommand> _handler;
    private readonly IExerciseRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public AddExerciseRepetitionsCommandHandlerTests()
    {
        _repository = Substitute.For<IExerciseRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new AddExerciseRepetitionsCommandHandler(_repository, _eventProcessor);
    }
}