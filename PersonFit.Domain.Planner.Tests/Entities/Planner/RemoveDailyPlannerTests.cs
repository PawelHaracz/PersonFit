namespace PersonFit.Domain.Planner.Tests.Entities.Planner;
using System;
using System.Linq;
using Core.Enums;
using Core.Events;
using Core.ValueObjects;
using Shouldly;
using Xunit;
using PersonFit.Core.Enums;
public class RemoveDailyPlannerTests
{
    [Fact]
    public void remove_whole_planner_should_delete_item()
    {

        var planner = Arrange();
        var status = planner.RemoveDailyPlanner(DayOfWeek.Monday, TimeOfDay.Afternoon);

        Assert.True(status);
        planner.DailyPlanners.Count().ShouldBe(2);
        
        Assert.Collection(planner.DailyPlanners,
            dailyPlanner =>
            {
                dailyPlanner.TimeOfDay.ShouldBe(TimeOfDay.Afternoon);
                dailyPlanner.DayOfWeek.ShouldBe(DayOfWeek.Friday);
            },
            dailyPlanner =>
            {
                dailyPlanner.TimeOfDay.ShouldBe(TimeOfDay.Night);
                dailyPlanner.DayOfWeek.ShouldBe(DayOfWeek.Monday);
            });
        
        Assert.Collection(planner.Events,
            @event => @event.ShouldBeOfType<RemovedDailyPlannerEvent>());
    }
    
    [Fact]
    public void remove_whole_planner_but_not_exists_should_ignore()
    {

        var planner = Arrange();
        var status = planner.RemoveDailyPlanner(DayOfWeek.Wednesday, TimeOfDay.Afternoon);
        
        Assert.False(status);
        planner.DailyPlanners.Count().ShouldBe(3);
        
        planner.Events.ShouldBeEmpty();
    }


    private Core.Entities.Planner Arrange()
        => new(
            new Guid("64BFB25A-224C-4339-9097-0FEBF5879595"),
            new Guid("70A4B84D-8900-4928-82CA-A2779B162AA6"),
            DateTime.Now.AddMonths(-1),
            DateTime.Now.AddMonths(1),
            PlannerStatus.Activate,
            new[]
            {
                new DailyPlanner(DayOfWeek.Friday, TimeOfDay.Afternoon,
                    new[]
                    {
                        new Guid("3D8E493D-1747-497B-A829-C0827688C535"),
                        new Guid("3D92880B-5B23-442C-BD8F-2AF15F41BD79")
                    }),
                new DailyPlanner(DayOfWeek.Monday, TimeOfDay.Afternoon,
                    new[] { new Guid("3D8E493D-1747-497B-A829-C0827688C535") }),
                new DailyPlanner(DayOfWeek.Monday, TimeOfDay.Night,
                    new[] { new Guid("3D92880B-5B23-442C-BD8F-2AF15F41BD79") }),
            });
}