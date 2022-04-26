using System;
using System.Linq;
using PersonFit.Core;
using Shouldly;
using Xunit;

namespace PersonFit.Domain.Exercise.Tests.Entities.Excerice;

public class CreateExerciseTests
{
    [Fact]
    public void given_valid_id_name_description_should_be_created()
    {
        var id = new AggregateId();
        var name = "plank";
        var description = "The plank is an isometric core strength exercise that involves maintaining a position similar to a push-up for the maximum possible time.";
        
        var exercise = Core.Entities.Exercise.Create(id, name, description, Array.Empty<string>());
        exercise.ShouldNotBeNull();
        exercise.Id.ShouldBe(id);
        exercise.Description.ShouldBe(description);
        exercise.Contents.ShouldBeEmpty();
        exercise.Tags.ShouldBeEmpty();
        
        exercise.Events.Count().ShouldBe(1);
    }
    
    [Fact]
    public void given_valid_id_name_tags_should_be_created()
    {
        var id = new AggregateId();
        var name = "plank";
        var description = string.Empty;
        var tags = new[] { "home", "gym" };
        var exercise = Core.Entities.Exercise.Create(id, name, description, tags);
        exercise.ShouldNotBeNull();
        exercise.Id.ShouldBe(id);
        exercise.Description.ShouldBeEmpty();
        exercise.Contents.ShouldBeEmpty();
        exercise.Tags.Count().ShouldBe(2);
        exercise.Tags.ShouldBe(tags);
        exercise.Events.Count().ShouldBe(2);
    }
}