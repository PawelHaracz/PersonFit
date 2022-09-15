

using System.Collections;
using System.Collections.Generic;

namespace PersonFit.Domain.Planner.Tests.Entities.PlannerExercise;
using System;
using PersonFit.Core.Aggregations;
using Core.Enums;
using Core.Events;
using Shouldly;
using Xunit;

public class ReorderRepetitionsTests
{
    [Fact]
    public void given_three_repetition_should_created_in_proper_order()
    {
        var items = new[]
        {
            new { count = 2, unit = MeasurementUnit.Mass, note = string.Empty },
            new { count = 1, unit = MeasurementUnit.Time, note = "blabla" },
            new { count = 10, unit = MeasurementUnit.Length, note = string.Empty }
        };
        var reorder = new Dictionary<int, int>
        {
            [2] = 1,
            [1] = 2,
            [3] = 3
        };
        
        var planner = Arrange();
        foreach (var item in items)
        {
            planner.AddRepetition(item.count, item.unit, item.note);
        }
        
        planner.ReorderRepetitions(reorder);

        Assert.Collection(planner.Repetitions, 
            repetition =>
            {
                repetition.Order.ShouldBe(1);
                repetition.Count.ShouldBe(items[1].count);
                repetition.Unit.ShouldBe(items[1].unit);
                repetition.Note.ShouldBe(items[1].note);
            },  
            repetition =>
            {
                repetition.Order.ShouldBe(2);
                repetition.Count.ShouldBe(items[0].count);
                repetition.Unit.ShouldBe(items[0].unit);
                repetition.Note.ShouldBe(items[0].note);
            },
            repetition =>
            {
                repetition.Order.ShouldBe(3);
                repetition.Count.ShouldBe(items[2].count);
                repetition.Unit.ShouldBe(items[2].unit);
                repetition.Note.ShouldBe(items[2].note);
            }
        );
        
        Assert.Collection(planner.Events,
            @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event => @event.ShouldBeOfType<AddedRepetitionEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<ReorderRepetitionEvent>();
                var r = @event as ReorderRepetitionEvent;
                r.OldOrder.ShouldBe(1);
                r.NewOrder.ShouldBe(2);
            },
            @event =>
            {
                @event.ShouldBeOfType<ReorderRepetitionEvent>();
                var r = @event as ReorderRepetitionEvent;
                r.OldOrder.ShouldBe(2);
                r.NewOrder.ShouldBe(1);
            }
        );
    }

    public void given_duplicated_records_should_throw()
    {
        var items = new[]
        {
            new { count = 2, unit = MeasurementUnit.Mass, note = string.Empty },
            new { count = 1, unit = MeasurementUnit.Time, note = "blabla" },
            new { count = 10, unit = MeasurementUnit.Length, note = string.Empty }
        };
        var reorder = new Dictionary<int, int>
        {
            [2] = 1,
            [1] = 1,
            [3] = 3
        };
        
        var planner = Arrange();
        foreach (var item in items)
        {
            planner.AddRepetition(item.count, item.unit, item.note);
        }
        
        var exception = Record.Exception(() => planner.ReorderRepetitions(reorder));
        exception.ShouldNotBeNull();
    }
    
    private Core.Entities.PlannerExercise Arrange()
    {
        var id = new AggregateId();
        var exerciseId = new Guid("FE270C83-EFB5-4414-A86D-98D9E19D7EAD");
        return Core.Entities.PlannerExercise.Create(id, exerciseId);
    }
}