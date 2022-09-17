namespace PersonFit.Domain.Planner.Tests.Entities.PlannerExercise;
using Shouldly;
using System;
using Xunit;
using Core.Events;
using Core.Exceptions;
using PersonFit.Core.Aggregations;
using PersonFit.Core.Exceptions;

public class CreatePlannerExerciseTests
{
    [Fact]
    public void given_empty_id_should_throw_exception()
    {
        var exception = Record.Exception(() => Core.Entities.PlannerExercise.Create(Guid.Empty, Guid.NewGuid(), Guid.NewGuid()));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidAggregateIdException>();
    }

    [Fact]
    public void given_empty_exercise_id_should_throw_exception()
    {
        var exception = Record.Exception(() => Core.Entities.PlannerExercise.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidExerciseIdPlannerExerciseException>();
    }
    
    
    [Fact]
    public void given_empty_owner_id_should_throw_exception()
    {
        var exception = Record.Exception(() => Core.Entities.PlannerExercise.Create(Guid.NewGuid(), Guid.Empty, Guid.NewGuid()));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyPlannerExerciseOwnerException>();
    }


    [Fact]
    public void create_new_exercise_planner()
    {
        var id = new AggregateId();
        var exerciseId = new Guid("FE270C83-EFB5-4414-A86D-98D9E19D7EAD");
        var ownerId = new Guid("E9A2F089-447E-49A0-AE2D-D6D6F5E38415");
        var planner = Core.Entities.PlannerExercise.Create(id, ownerId, exerciseId);
        
        planner.ExerciseId.ShouldBe(exerciseId);
        planner.Id.ShouldBe(id);
        Assert.Collection(planner.Events, @event => @event.ShouldBeOfType<PlannerExerciseCreatedEvent>());
    }
}