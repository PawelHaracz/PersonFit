using System;
using System.Linq;
using PersonFit.Core;
using PersonFit.Core.Aggregations;
using PersonFit.Domain.Exercise.Core.Enums;
using PersonFit.Domain.Exercise.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace PersonFit.Domain.Exercise.Tests.Entities.Excerice;

public class AddContentTests
{
    [Fact]
    public void given_exercise_without_content_then_add_media_should_be_added()
    {
        var id = new AggregateId();
        var media = new MediaContent("https://test.com", MediaContentType.Image);
        
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, Array.Empty<string>());
        exercise.AddContent(media);
        
        exercise.Contents.Count().ShouldBe(1);
        exercise.Contents.ShouldBe(new []{ media });
        exercise.Events.Count().ShouldBe(2);
    }

    [Fact]
    public void given_exercise_without_content_then_add_twice_media_should_be_added()
    {
        var id = new AggregateId();
        var media1 = new MediaContent("https://test.com", MediaContentType.Image);
        var media2 = new MediaContent("https://test1.com", MediaContentType.Video);
        
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, Array.Empty<string>());
        exercise.AddContent(media1);
        exercise.AddContent(media2);
        
        exercise.Contents.Count().ShouldBe(2);
        exercise.Contents.ShouldBe(new []{ media1, media2 });
        exercise.Events.Count().ShouldBe(3);
    }

    [Fact]
    public void given_exercise_without_content_then_add_twice_the_same_content_should_be_added_first()
    {
        var id = new AggregateId();
        var media1 = new MediaContent("https://test.com", MediaContentType.Image);
        var media2 = new MediaContent("https://test.com", MediaContentType.Image);
        
        var exercise = Core.Entities.Exercise.Create(id, string.Empty, string.Empty, Array.Empty<string>());
        exercise.AddContent(media1);
        exercise.AddContent(media2);
        
        exercise.Contents.Count().ShouldBe(1);
        exercise.Contents.ShouldBe(new []{ media1 });
        exercise.Events.Count().ShouldBe(2);
    }
}