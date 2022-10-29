namespace PersonFit.Domain.Planner.Tests.Commands.PlannerExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Aggregations;
using PersonFit.Core.Commands;
using PersonFit.Core.Events;
using PersonFit.Core.Tests.Extensions;
using Application.Dtos;
using Application.Exceptions;
using Core.Repositories;
using Extensions;
using Xunit;
using Shouldly;
using PersonFit.Core.Enums;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise;
using PersonFit.Domain.Planner.Application.Commands.PlannerExercise.CommandHandlers;
public class CreateExerciseCommandTests
{
    [Fact]
    public async Task given_crate_command_with_exist_exercise_for_owner()
    {
        var cancellationToken = CancellationToken.None;
        var ownerId = new Guid("04853A56-7347-4D0A-8BAF-EF115E227EC1");
        var exerciseId = new Guid("38609CBE-9E9D-4511-9A5B-79F6D4DB7FA5");
        var plannerExerciseId = new Guid("C0CDD93F-CE37-47E7-AD62-ACBEACDCD1E4");
        var command = new CreateExerciseCommand(new AggregateId(), ownerId, exerciseId, Enumerable.Empty<ExerciseRepetitionDto>());
        
        _repository.Exists(Arg.Is<Guid>(g => g == ownerId), Arg.Is<Guid>(g => g == exerciseId),
            Arg.Any<CancellationToken>())
            .Returns(plannerExerciseId);

        var exception = await Record.ExceptionAsync(async () => await _handler.HandleAsync(command, cancellationToken));
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ExerciseAlreadyCreatedException>();
    }
    
    [Fact]
    public async Task given_create_command_without_repetition_should_create_planner()
    {
        var cancellationToken = CancellationToken.None;
        var command = new CreateExerciseCommand(new AggregateId(), new Guid("F5C3C417-530B-4AB0-8B12-5B604C3C2524"), new Guid("27F69BAA-8C55-4322-85C0-4372C4AF43C5"), Enumerable.Empty<ExerciseRepetitionDto>());
        var exercise = Core.Entities.PlannerExercise.Create(command.Id, command.OwnerId, command.ExerciseId);
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Create(Arg.Is<Core.Entities.PlannerExercise>(e => 
            e.Compare( exercise)), cancellationToken);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(exercise.Events)), 
            cancellationToken);
    }
    
    [Fact]
    public async Task given_create_command_with_repetition_should_create_planner()
    {
        var cancellationToken = CancellationToken.None;
        var command = new CreateExerciseCommand(new AggregateId(), 
            new Guid("F5C3C417-530B-4AB0-8B12-5B604C3C2524"), 
            new Guid("27F69BAA-8C55-4322-85C0-4372C4AF43C5"), 
            new []
        {
            new ExerciseRepetitionDto(4, MeasurementUnit.Length, string.Empty),
            new ExerciseRepetitionDto(3, MeasurementUnit.Mass, "test"),
        });
        
        var exercise = Core.Entities.PlannerExercise.Create(command.Id, command.OwnerId, command.ExerciseId);
        
        foreach (var dto in command.Repetition)
        {
            exercise.AddRepetition(dto.Count, dto.Unit, dto.Note);
        }
        
        await _handler.HandleAsync(command, cancellationToken);

        await _repository.Received(1).Create(Arg.Is<Core.Entities.PlannerExercise>(e => 
            e.Compare( exercise)), cancellationToken);
        await _eventProcessor.Received(1).ProcessAsync(Arg.Is<IEnumerable<IDomainEvent>>(
                events => events.CompareArrays(exercise.Events)), 
            cancellationToken);
    }

    private readonly ICommandHandler<CreateExerciseCommand> _handler;
    private readonly IExerciseRepository _repository;
    private readonly IEventProcessor _eventProcessor;
    
    public CreateExerciseCommandTests()
    {
        _repository = Substitute.For<IExerciseRepository>();
        _eventProcessor = Substitute.For<IEventProcessor>();
        _handler = new CreateExerciseCommandHandler(_repository, _eventProcessor);
    }
}