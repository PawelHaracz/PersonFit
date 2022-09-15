namespace PersonFit.Domain.Planner.Tests.Entities.PlannerExercise;
using System;
using System.Linq;
using PersonFit.Core.Aggregations;
using Core.Enums;
using Core.Events;
using Shouldly;
using Xunit;

public class AddRepetitionTests
{
    [Fact]
    public void given_exercise_planner_then_add_new_repetition()
    {
        var count = 2;
        var unit = MeasurementUnit.None;
        var note = string.Empty;
        var planner = Arrange();
        
        planner.AddRepetition(count, unit, note);
        
        planner.Repetitions.Count().ShouldBe(1);
        Assert.Collection(planner.Repetitions, repetition =>
        {
            repetition.Count.ShouldBe(count);
            repetition.Order.ShouldBe(1);
            repetition.Note.ShouldBe(note);
            repetition.Unit.ShouldBe(unit);
        });
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>(),
            @event =>  @event.ShouldBeOfType<AddedRepetitionEvent>());
    }

    [Fact]
    public void given_three_repetition_should_created_in_proper_order()
    {
        var items = new[]
        {
            new { count = 2, unit = MeasurementUnit.Mass, note = string.Empty },
            new { count = -1, unit = MeasurementUnit.Time, note = "blabla" },
            new { count = 10, unit = MeasurementUnit.Length, note = string.Empty }
        };
        var planner = Arrange();
        foreach (var item in items)
        {
            planner.AddRepetition(item.count, item.unit, item.note);
        }
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedRepetitionEvent>();
                var r = @event as AddedRepetitionEvent;
                r.Order.ShouldBe(1);
                r.Count.ShouldBe(items[0].count);
                r.Unit.ShouldBe(items[0].unit);
                r.Note.ShouldBe(items[0].note);
            },
            @event =>
            {
                @event.ShouldBeOfType<AddedRepetitionEvent>();
                var r = @event as AddedRepetitionEvent;
                r.Order.ShouldBe(2);
                r.Count.ShouldBe(0);
                r.Unit.ShouldBe(items[1].unit);
                r.Note.ShouldBe(items[1].note);
            },
            @event =>
            {
                @event.ShouldBeOfType<AddedRepetitionEvent>();
                var r = @event as AddedRepetitionEvent;
                r.Order.ShouldBe(3);
                r.Count.ShouldBe(items[2].count);
                r.Unit.ShouldBe(items[2].unit);
                r.Note.ShouldBe(items[2].note);
            }
        );
    }

    private Core.Entities.PlannerExercise Arrange()
    {
        var id = new AggregateId();
        var exerciseId = new Guid("FE270C83-EFB5-4414-A86D-98D9E19D7EAD");
       return Core.Entities.PlannerExercise.Create(id, exerciseId);
    }
}