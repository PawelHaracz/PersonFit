namespace PersonFit.Domain.Planner.Tests.Entities.PlannerExercise;
using System;
using PersonFit.Core.Aggregations;
using Core.Exceptions;
using PersonFit.Core.Enums;
using Core.Events;
using Shouldly;
using Xunit;

public class RemoveRepetitionTests
{
    [Fact]
    public void given_exercise_planner_add_repetition_then_remove_it()
    {
        var planner = Arrange();
        var order = planner.AddRepetition(1, MeasurementUnit.None, String.Empty);
        planner.RemoveRepetition(order);
        
        planner.Repetitions.ShouldBeEmpty();
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>(),
            @event =>  @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event =>  @event.ShouldBeOfType<RemovedRepetitionEvent>());
    }

    [Fact]
    public void given_exercise_planner_add_three_repetitions_then_remove_second_should_remove_and_reorder()
    {
        var items = new[]
        {
            new { count = 2, unit = MeasurementUnit.Mass, note = string.Empty },
            new { count = 0, unit = MeasurementUnit.Time, note = "blabla" },
            new { count = 10, unit = MeasurementUnit.Length, note = "blabla" }
        };
        var planner = Arrange();
        foreach (var item in items)
        {
            planner.AddRepetition(item.count, item.unit, item.note);
        }
        
        planner.RemoveRepetition(2);
        
        Assert.Collection(planner.Repetitions, 
            repetition =>
        {
            repetition.Order.ShouldBe(1);
            repetition.Count.ShouldBe(items[0].count);
            repetition.Unit.ShouldBe(items[0].unit);
            repetition.Note.ShouldBe(items[0].note);
        },  
        repetition =>
        {
            repetition.Order.ShouldBe(2);
            repetition.Count.ShouldBe(items[2].count);
            repetition.Unit.ShouldBe(items[2].unit);
            repetition.Note.ShouldBe(items[2].note);
        });

        Assert.Collection(planner.Events,
            @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedRepetitionEvent>();
                var r = @event as RemovedRepetitionEvent;
                r.Order.ShouldBe(2);
                r.Count.ShouldBe(items[1].count);
                r.Unit.ShouldBe(items[1].unit);
                r.Note.ShouldBe(items[1].note);
            });
    }

    [Fact]
    public void given_exercise_planner_add_repetition_then_remove_minus_one_order_should_throw()
    {
        var planner = Arrange();
        planner.AddRepetition(1, MeasurementUnit.None, String.Empty);
        var exception = Record.Exception(() => planner.RemoveRepetition(-1));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RemoveExerciseRepetitionException>();
    }
    
    [Fact]
    public void given_exercise_planner_add_repetition_then_remove_second_order_should_throw()
    {
        var planner = Arrange();
        planner.AddRepetition(1, MeasurementUnit.None, String.Empty);
        var exception = Record.Exception(() => planner.RemoveRepetition(2));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RemoveExerciseRepetitionException>();
    }
    
    private Core.Entities.PlannerExercise Arrange()
    {
        var id = new AggregateId();
        var exerciseId = new Guid("AA594AD9-6FA0-428E-9CE5-587FC2E05B2A");
        var ownerId = new Guid("11209823-9BB1-4AAE-8640-40A41B54D7D4");
        return Core.Entities.PlannerExercise.Create(id, ownerId, exerciseId);
    }
}