namespace PersonFit.Domain.Exercise.Tests.Queries;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using NSubstitute;
using PersonFit.Domain.Exercise.Application.Queries;
using PersonFit.Domain.Exercise.Application.Queries.QueryHandlers;
using Core.Repositories;
using Core.ValueObjects;
using Infrastructure.Postgres.Documents;
using Shouldly;
using Xunit;

public class GetExercisesQueryHandlerTests
{
    [Fact]
    public async Task Get_all_exercises()
    {
        var token = CancellationToken.None;
        var command = new GetExercisesQuery();
        _readExerciseRepository.GetAll(token).Returns(_ => _enumerable);

        var act = await _handler.HandleAsync(command, token);

        _readExerciseRepository.Received(1).GetAll(Arg.Is(token));
        act.ShouldBeEquivalentTo(_enumerable.Select(e => e.AsDto()));
    }

    [Fact]
    public async Task No_such_data_should_return_empty_collection()
    {
        var token = CancellationToken.None;
        var command = new GetExercisesQuery();      
        
        _readExerciseRepository.GetAll(token).Returns(_ => Enumerable.Empty<Core.Entities.Exercise>());
        
        var act = await _handler.HandleAsync(command, token);
        
        _readExerciseRepository.Received(1).GetAll(Arg.Is(token)); //Ignore this warning, because method is never called there only Assert
        act.ShouldBeEmpty();
    }


    private readonly GetExercisesQueryHandler _handler;
    private readonly IReadExerciseRepository _readExerciseRepository;

    private readonly Core.Entities.Exercise[] _enumerable =
    {
        new(Guid.NewGuid(), "test 1", "test des", new[] { "test tag" }, Enumerable.Empty<MediaContent>()),
        new(Guid.NewGuid(), "test 2", "test des", new[] { "test tag1", "test tag2" }, Enumerable.Empty<MediaContent>()),
        new(Guid.NewGuid(), "test 3", "test des", Enumerable.Empty<String>(), Enumerable.Empty<MediaContent>()),
    };

    public GetExercisesQueryHandlerTests()
    {
        _readExerciseRepository = Substitute.For<IReadExerciseRepository>();
    
        _handler = new GetExercisesQueryHandler(_readExerciseRepository);
    }
}