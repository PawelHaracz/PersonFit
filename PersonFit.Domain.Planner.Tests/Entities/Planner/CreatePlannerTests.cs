namespace PersonFit.Domain.Planner.Tests.Entities.Planner;
using System;
using PersonFit.Core.Exceptions;
using Core.Enums;
using Core.Events;
using Core.Exceptions;
using Shouldly;
using Xunit;

public class CreatePlannerTests
{
    [Fact]
    public void given_empty_id_should_throw_exception()
    {
        var exception = Record.Exception(() => Core.Entities.Planner.Create(Guid.Empty, Guid.NewGuid(), DateTime.Now.AddMonths(-1), DateTime.Now));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidAggregateIdException>();
    }
    
    [Fact]
    public void given_empty_user_id_should_throw_exception()
    {
        var exception = Record.Exception(() => Core.Entities.Planner.Create(Guid.NewGuid(), Guid.Empty, DateTime.Now.AddMonths(-1), DateTime.Now));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlannerEmptyUserException>();
    }

    [Fact]
    public void given_end_date_earlier_then_start_date_should_throw()
    {
        var exception = Record.Exception(() => Core.Entities.Planner.Create(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, DateTime.Now.AddMonths(-1)));
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlannerCreatingException>();
    }
    
    [Fact]
    public void given_future_start_date_should_return_pending_planner()
    {
        var startDate = DateTime.Now.AddMonths(1);
        var endDate = DateTime.Now.AddMonths(2);
        var userId = new Guid("0B9AF73E-90D1-46AA-AFAE-8A75239F80A7");
        var id = new Guid("4EECCBDF-0A77-4EAF-97A9-19DED3A17F0A");

        var planner = Core.Entities.Planner.Create(id, userId, startDate, endDate);
        
        planner.Status.ShouldBe(PlannerStatus.Pending);
    }

    [Fact]
    public void given_start_date_earlier_should_status_be_activate()
    {
        var startDate = DateTime.Now.AddMonths(-1);
        var endDate = DateTime.Now.AddMonths(2);
        var userId = new Guid("0B9AF73E-90D1-46AA-AFAE-8A75239F80A7");
        var id = new Guid("4EECCBDF-0A77-4EAF-97A9-19DED3A17F0A");

        var planner = Core.Entities.Planner.Create(id, userId, startDate, endDate);
        
        planner.Status.ShouldBe(PlannerStatus.Activate);
      
    }
    
    [Fact]
    public void given_start_and_end_date_earlier_should_throw()
    {
        var startDate = DateTime.Now.AddMonths(-2);
        var endDate = DateTime.Now.AddMonths(-1);
        var userId = new Guid("0B9AF73E-90D1-46AA-AFAE-8A75239F80A7");
        var id = new Guid("4EECCBDF-0A77-4EAF-97A9-19DED3A17F0A");

        var exception = Record.Exception(() =>  Core.Entities.Planner.Create(id, userId, startDate, endDate));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlannerCreatingException>();
    }
    
    [Fact]
    public void given_future_start_date_should_return_planner()
    {
        var startDate = DateTime.Now.AddMonths(1);
        var endDate = DateTime.Now.AddMonths(2);
        var userId = new Guid("0B9AF73E-90D1-46AA-AFAE-8A75239F80A7");
        var id = new Guid("4EECCBDF-0A77-4EAF-97A9-19DED3A17F0A");

        var planner = Core.Entities.Planner.Create(id, userId, startDate, endDate);
        
        planner.Id.Value.ShouldBe(id);
        planner.Status.ShouldBe(PlannerStatus.Pending);
        planner.StartTime.ShouldBe(startDate.ToUniversalTime());
        planner.EndTime.ShouldBe(endDate.ToUniversalTime());
        planner.OwnerId.ShouldBe(userId);
        
        Assert.Collection(planner.Events, @event =>
        {
            @event.ShouldBeOfType<CreatedNewPlannerEvent>();
            var p = @event as CreatedNewPlannerEvent;
            p.Id.ShouldBe(id);
            p.Status.ShouldBe(PlannerStatus.Pending);
            p.StartTime.ShouldBe(startDate.ToUniversalTime());
            p.EndTime.ShouldBe(endDate.ToUniversalTime());
            p.OwnerId.ShouldBe(userId);
        });
    }
}