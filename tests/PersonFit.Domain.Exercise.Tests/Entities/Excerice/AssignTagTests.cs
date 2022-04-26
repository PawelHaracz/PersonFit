using System;
using System.Collections.Generic;
using System.Linq;
using PersonFit.Core;
using Shouldly;
using Xunit;

namespace PersonFit.Domain.Exercise.Tests.Entities.Excerice;

public class AssignTagTests
{
    [Fact]
    public void given_exercise_without_tags_then_assign_tag_should_be_assigned()
    {
        var id = new AggregateId();
        var tags = new[] { "test" };
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, Array.Empty<string>());
        exercise.AssignTags(tags);
        
        exercise.Tags.Count().ShouldBe(1);
        exercise.Tags.ShouldBe(tags);
        exercise.Events.Count().ShouldBe(2);
    }

    [Fact]
    public void given_exercise_with_same_tags_then_assign_tag_should_be_skipped()
    {
        var id = new AggregateId();
        var tags = new[] { "test", "play" };
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, tags);
        exercise.AssignTags(tags);
        
        exercise.Tags.Count().ShouldBe(2);
        exercise.Tags.ShouldBe(tags);
        exercise.Events.Count().ShouldBe(2);
    }

    [Fact]
    public void given_exercise_with__tags_then_assign__new_tag_should_be_added()
    {
        var id = new AggregateId();
        var tags = new[] { "test", "play" };
        var newTag = new[] { "gym" };
        var mergeTags = new List<string>(tags);
        mergeTags.AddRange(newTag);
        
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, tags);
        exercise.AssignTags(newTag);
        
        exercise.Tags.Count().ShouldBe(3);
        exercise.Tags.ShouldBe(mergeTags);
        exercise.Events.Count().ShouldBe(3);
    }
}