namespace PersonFit.Domain.Exercise.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Shouldly;
using Xunit;

public class UnassignTagTests
{
    [Fact]
    public void given_exercise_without_tags_then_unassign_tag_should_be_skipped()
    {
        var id = new AggregateId();
        var tags = new[] { "test" };
        var exercise = Exercise.Create(id, string.Empty, string.Empty, Array.Empty<string>());
        exercise.UnassignTags(tags);
        
        exercise.Tags.Count().ShouldBe(0);
        exercise.Tags.ShouldBeEmpty();
        exercise.Events.Count().ShouldBe(1);
    }

    [Fact]
    public void given_exercise_with_tags_then_unassign_tags_that_not_exists_should_be_skipped()
    {
        var id = new AggregateId();
        var tags = new[] { "test" };
        var newTag = new[] { "gym", "home" };
        var exercise = Exercise.Create(id, string.Empty, string.Empty, tags);
        exercise.UnassignTags(newTag);
        
        exercise.Tags.Count().ShouldBe(1);
        exercise.Tags.ShouldBe(tags);
        exercise.Events.Count().ShouldBe(2);
    }

    [Fact]
    public void given_exercise_with_tags_then_unassign_tags_should_be_unassigned()
    {
        var id = new AggregateId();
        var tags = new[] { "gym", "home" };

        var exercise = Exercise.Create(id, string.Empty, string.Empty, tags);
        exercise.UnassignTags(tags);
        
        exercise.Tags.Count().ShouldBe(0);
        exercise.Tags.ShouldBeEmpty();
        exercise.Events.Count().ShouldBe(3);
    }
    
    [Fact]
    public void given_exercise_with_tags_then_unassign_some_tags_should_be_unassigned()
    {
        var id = new AggregateId();
        var tags = new[] { "test", };
        var newTag = new[] { "gym", "home" };
        var mergeTags = new List<string>(tags);
        mergeTags.AddRange(newTag);
        
        var exercise = Exercise.Create(id, string.Empty, string.Empty, mergeTags.ToArray());
        exercise.UnassignTags(newTag);
        
        exercise.Tags.Count().ShouldBe(1);
        exercise.Tags.ShouldBe(tags);
        exercise.Events.Count().ShouldBe(3);
    }
}