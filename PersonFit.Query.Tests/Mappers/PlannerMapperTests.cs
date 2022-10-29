namespace PersonFit.Query.Tests.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Queries;
using Planner.Application.Daos;
using Planner.Application.Dtos;
using Planner.Application.Enums;
using Planner.Application.Exceptions;
using PersonFit.Query.Planner.Infrastructure.Mappers;
using Shouldly;
using Xunit;

public class PlannerMapperTests
{
    private readonly IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>> _mapper =  new PlannerMapper();
    
    [Fact]
    public void add_collection_should_map()
    {
        var dbResults = new QueryPlannerDao[]
        {
            new QueryPlannerDao()
            {
                Id = new Guid("E918AF49-7403-4482-9048-1A08CF5BCA06"),
                Status = PlannerStatus.Active,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            },
            new QueryPlannerDao()
            {
                Id = new Guid("0C9C96FF-9B4D-4B63-BA37-6B7C5D5155CE"),
                Status = PlannerStatus.Pending,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            }
        };

        var act = _mapper.Map(dbResults);
        
        Assert.Collection(act, dto =>
        {
            var item = dbResults[0];
            dto.Id.ShouldBe(item.Id);
            dto.Status.ShouldBe(item.Status.ToString());
            dto.StartTime.ShouldBe(DateOnly.FromDateTime(item.StartTime));
            dto.EndTime.ShouldBe(DateOnly.FromDateTime(item.EndTime));
        },
        dto =>
        {
            var item = dbResults[1];
            dto.Id.ShouldBe(item.Id);
            dto.Status.ShouldBe(item.Status.ToString());
            dto.StartTime.ShouldBe(DateOnly.FromDateTime(item.StartTime));
            dto.EndTime.ShouldBe(DateOnly.FromDateTime(item.EndTime));
        });
    }

    [Fact]
    public void empty_collection_should_return_empty()
    {
        var assert = ArraySegment<QueryPlannerDao>.Empty;

        var act = _mapper.Map(assert);

        act.ShouldBeEmpty();
    }
    
    [Fact]
    public void add_empty_guid_collection_should_throw()
    {
        var dbResults = new QueryPlannerDao[]
        {
            new QueryPlannerDao()
            {
                Id = Guid.Empty,
                Status = PlannerStatus.Active,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            },
            new QueryPlannerDao()
            {
                Id = new Guid("0C9C96FF-9B4D-4B63-BA37-6B7C5D5155CE"),
                Status = PlannerStatus.Pending,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            }
        };

        var act = Record.Exception(() => _mapper.Map(dbResults).ToArray());
        
        act.ShouldNotBeNull();
        act.ShouldBeAssignableTo<MappingObjectException>();
    }

    [Fact]
    public void add_empty_startTime_collection_should_throw()
    {
        var dbResults = new QueryPlannerDao[]
        {
            new QueryPlannerDao()
            {
                Id = new Guid("E918AF49-7403-4482-9048-1A08CF5BCA06"),
                Status = PlannerStatus.Active,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            },
            new QueryPlannerDao()
            {
                Id = new Guid("0C9C96FF-9B4D-4B63-BA37-6B7C5D5155CE"),
                Status = PlannerStatus.Pending,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = new DateTime()
            }
        };

        var act = Record.Exception(() => _mapper.Map(dbResults).ToArray());
        
        act.ShouldNotBeNull();
        act.ShouldBeAssignableTo<MappingObjectException>();

    }
    
    [Fact]
    public void add_empty_endTime_collection_should_throw()
    {
        var dbResults = new QueryPlannerDao[]
        {
            new QueryPlannerDao()
            {
                Id = new Guid("E918AF49-7403-4482-9048-1A08CF5BCA06"),
                Status = PlannerStatus.Active,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            },
            new QueryPlannerDao()
            {
                Id = new Guid("0C9C96FF-9B4D-4B63-BA37-6B7C5D5155CE"),
                Status = PlannerStatus.Pending,
                EndTime = new DateTime(),
                StartTime = DateTime.Now.AddDays(-1),
            }
        };

        var act = Record.Exception(() => _mapper.Map(dbResults).ToArray());
        
        act.ShouldNotBeNull();
        act.ShouldBeAssignableTo<MappingObjectException>();

    }
    
    [Fact]
    public void add_collection_with_wrong_date_should_throw()
    {
        var dbResults = new QueryPlannerDao[]
        {
            new QueryPlannerDao()
            {
                Id = new Guid("E918AF49-7403-4482-9048-1A08CF5BCA06"),
                Status = PlannerStatus.Active,
                EndTime = DateTime.Now.AddDays(2),
                StartTime = DateTime.Now.AddDays(-2)
            },
            new QueryPlannerDao()
            {
                Id = new Guid("0C9C96FF-9B4D-4B63-BA37-6B7C5D5155CE"),
                Status = PlannerStatus.Pending,
                EndTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(2)
            }
        };

    
        var act = Record.Exception(() => _mapper.Map(dbResults).ToArray());
        
        act.ShouldNotBeNull();
        act.ShouldBeAssignableTo<MappingObjectException>();
    }
}