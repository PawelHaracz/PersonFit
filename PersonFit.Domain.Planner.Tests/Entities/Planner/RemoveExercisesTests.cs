namespace PersonFit.Domain.Planner.Tests.Entities.Planner;
using System;
using Core.Enums;
using Core.Events;
using Core.ValueObjects;
using Shouldly;
using Xunit;

public class RemoveExercisesTests
{
    [Fact]
    public void given_exercises_in_one_day_then_remove_one_should_delete()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1, exercise3 });
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1, exercise2, exercise3);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, dp => dp.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise2);
            });
    }

    [Fact]
    public void given_exercises_in_one_day_then_remove_two_should_delete_in_the_proper_order()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise3 });
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1, exercise2, exercise3);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise2, exercise1 });
        
        Assert.Collection(planner.DailyPlanners, dp => dp.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise2);
            },
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise1);
            });
    }

    [Fact]
    public void given_two_daily_planners_removed_from_second_planner_one_exercise()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek1 = DayOfWeek.Friday;
        var timeOfDay1 = TimeOfDay.Morning;
        var dayOfWeek2 = DayOfWeek.Saturday;
        var timeOfDay2 = TimeOfDay.Morning;

        var dailyPlanner1 = new DailyPlanner(dayOfWeek1, timeOfDay1, new []{ exercise1, exercise2 });
        var dailyPlanner2 = new DailyPlanner(dayOfWeek2, timeOfDay2, new []{ exercise3 });
        var planner = Arrange(dayOfWeek1, timeOfDay1, exercise1, exercise2);
        
        planner.AddExercises(dayOfWeek2, timeOfDay2, new []{ exercise2, exercise3 }); 
        planner.RemoveExercises(dayOfWeek2, timeOfDay2, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, 
            dp => dp.ShouldBe(dailyPlanner1),
            dp => dp.ShouldBe(dailyPlanner2));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek2);
                r.TimeOfDay.ShouldBe(timeOfDay2);
                r.PlannerExercise.ShouldBe(exercise2);
            });
    }

    [Fact]
    public void given_empty_planner_exercise_should_ignore()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1 });
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ Guid.Empty });
        
        Assert.Collection(planner.DailyPlanners, dp => dp.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>());   
    }

    [Fact]
    public void given_exercises_in_one_day_then_remove_one_twice_should_second_ignore()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1, exercise3 });
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1, exercise2, exercise3);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise2 });
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, dp => dp.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise2);
            });
    }

    [Fact]
    public void remove_all_exercises_should_remove_daily_planner()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var planner = Arrange(dayOfWeek, timeOfDay, exercise1);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise1 });
        
        planner.DailyPlanners.ShouldBeEmpty();
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise1);
            },
            @event =>
            {
                @event.ShouldBeOfType<RemovedDailyPlannerEvent>();
                var r = @event as RemovedDailyPlannerEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
            });  
    }

    [Fact]
    public void remove_exercise_does_not_exist_in_planner_should_ignore()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;

        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1, exercise2 });
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1, exercise2);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise3 });
        
        Assert.Collection(planner.DailyPlanners, dp => dp.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>());
    }

    [Fact]
    public void remove_all_exercises_from_planner_should_remove_planer()
    {
        Guid exercise1 = new Guid("24C4C695-5DA4-4F38-8BED-2207FA438370");
        Guid exercise2 = new Guid("E4FC0C08-E226-4737-A3C7-59E4EC529718");
        Guid exercise3 = new Guid("F31CDE79-C94D-427B-A5A4-FCA26B0841B8");
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Morning;
        
        var planner = Arrange(dayOfWeek, timeOfDay, exercise1, exercise2, exercise3);
        
        planner.RemoveExercises(dayOfWeek, timeOfDay, new []{ exercise2, exercise1,exercise3  });
        
        planner.DailyPlanners.ShouldBeEmpty();
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event =>  @event.ShouldBeOfType<AddedPlannerExerciseEvent>(),
            @event => 
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise2);
            },
            @event => 
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise1);
            },
            @event => 
            {
                @event.ShouldBeOfType<RemovedPlannerExerciseEvent>();
                var r = @event as RemovedPlannerExerciseEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
                r.PlannerExercise.ShouldBe(exercise3);
            },
            @event => 
            {
                @event.ShouldBeOfType<RemovedDailyPlannerEvent>();
                var r = @event as RemovedDailyPlannerEvent;
                r.DayOfWeek.ShouldBe(dayOfWeek);
                r.TimeOfDay.ShouldBe(timeOfDay);
            });
    }
    
    
    private Core.Entities.Planner Arrange(DayOfWeek dayOfWeek, TimeOfDay timeOfDay,  params Guid[] exerciseIds)
    {
        var planner = Core.Entities.Planner.Create(
            new Guid("1E5A57C9-496F-4DA3-94E2-24721FD3A4B4"),
            new Guid("3A27D9F5-5044-4C2E-B9B3-C624F3E78BE9"),
            DateTime.Now.AddMonths(-1), 
            DateTime.Now.AddMonths(1));
        
        planner.AddExercises(dayOfWeek, timeOfDay, exerciseIds);
        
        return planner;
    }
    
}