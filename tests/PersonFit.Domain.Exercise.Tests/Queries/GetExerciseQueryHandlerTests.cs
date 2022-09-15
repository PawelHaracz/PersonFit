using PersonFit.Domain.Exercise.Application.Dtos;

namespace PersonFit.Domain.Exercise.Tests.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using PersonFit.Core.Aggregations;
using PersonFit.Domain.Exercise.Application.Queries;
using PersonFit.Domain.Exercise.Application.Queries.QueryHandlers;
using Core.Enums;
using Core.Repositories;
using Core.ValueObjects;
using Shouldly;
using Xunit;

public class GetExerciseQueryHandlerTests
{

    [Fact]
    public async Task Get_item_by_guid1_should_return()
    {
        var token = CancellationToken.None;
        var command = new GetExerciseQuery(_guid1);    
        
        _readExerciseRepository.Get(Arg.Is(_guid1), token).Returns(items => 
            _enumerable.SingleOrDefault(e =>  e.Id == new AggregateId(items.ArgAt<Guid>(0))));
        
        var act = await _handler.HandleAsync(command, token);
        
        _readExerciseRepository.Received(1).Get(Arg.Is(_guid1), Arg.Is(token)); //Ignore this warning, because method is never called there only Assert
        act.Contents.ShouldBeEmpty();
        act.Id.ShouldBe(_guid1);
        act.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Get_item_by_guid2_should_return()
    {
        var token = CancellationToken.None;
        var command = new GetExerciseQuery(_guid2);    
        
        _readExerciseRepository.Get(Arg.Is(_guid2), token).Returns(items => 
            _enumerable.SingleOrDefault(e =>  e.Id == new AggregateId(items.ArgAt<Guid>(0))));
        
        var act = await _handler.HandleAsync(command, token);
        
        _readExerciseRepository.Received(1).Get(Arg.Is(_guid2), Arg.Is(token)); //Ignore this warning, because method is never called there only Assert
        act.Contents.Count().ShouldBeEquivalentTo(1);
        act.Contents.ShouldBe(new []
        {
            new ExerciseSummaryContent("https://test.com", MediaContentType.Image.ToString())
        });
        act.Id.ShouldBe(_guid2);
        act.ShouldNotBeNull();
    }

    [Fact]
    public async Task Get_item_by_random_guid_should_return_default()
    {
        var token = CancellationToken.None;
        var command = new GetExerciseQuery(Guid.NewGuid());    
        
        _readExerciseRepository.Get(Arg.Any<Guid>(), token).Returns(items => 
            _enumerable.SingleOrDefault(e =>  e.Id == new AggregateId(items.ArgAt<Guid>(0))));
        
        var act = await _handler.HandleAsync(command, token);
        
        _readExerciseRepository.Received(1).Get(Arg.Any<Guid>(), Arg.Is(token)); //Ignore this warning, because method is never called there only Assert
        act.ShouldBe(ExerciseSummaryDto.Default);
    }
    private readonly GetExerciseQueryHandler _handler;
    private readonly IReadExerciseRepository _readExerciseRepository;
    private static readonly Guid _guid1 = new Guid("072D9020-567A-4416-A75E-5102A8AF530F");
    private static readonly Guid _guid2 = new Guid("7A9F8578-6F0D-4B5C-A10B-4FC9CDD5B1E6");
    
    private readonly Core.Entities.Exercise[] _enumerable =
    {
        new(_guid1, "test 1", "test des", new[] { "test tag" }, Enumerable.Empty<MediaContent>()),
        new(_guid2, "test 2", "test des", Enumerable.Empty<string>(), new MediaContent[]{new MediaContent("https://test.com", MediaContentType.Image)}),
    };
    
    public GetExerciseQueryHandlerTests()
    {
        _readExerciseRepository = Substitute.For<IReadExerciseRepository>();
    
        _handler = new GetExerciseQueryHandler(_readExerciseRepository);
    }
}