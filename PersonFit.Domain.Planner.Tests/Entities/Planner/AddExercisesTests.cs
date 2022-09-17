namespace PersonFit.Domain.Planner.Tests.Entities.Planner;
using System;
using Core.Enums;
using Core.Events;
using Core.ValueObjects;
using Shouldly;
using Xunit;

public class AddExercisesTests
{
    [Fact]
    public void given_new_exercises_for_new_day_and_time_should_add()
    {
        var dayOfWeek = DayOfWeek.Friday;
        var timeOfDay = TimeOfDay.Evening;
        var exercises = new[]
        {
            new Guid("A0374058-260E-479B-A0B3-73A7827459FC"), 
            new Guid("914C44B9-528F-46A9-861A-A88BB2B86E40")
        };
        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, exercises);
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, exercises);
        
        Assert.Collection(planner.DailyPlanners, daily =>  daily.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercises[0]);
            },
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercises[1]);
            });
    }

    [Fact]
    public void given_new_exercises_for_monday_whole_day_should_add()
    {
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.WholeDay;
        var exercises = new[]
        {
            new Guid("A0374058-260E-479B-A0B3-73A7827459FC"),
        };
        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, exercises);
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, exercises);
        
        Assert.Collection(planner.DailyPlanners, daily =>  daily.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercises[0]);
            });
    }

    [Fact]
    public void given_new_exercises_for_already_existed_day_and_time_should_add()
    {
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.Evening;
        var exercise1 = new Guid("A0374058-260E-479B-A0B3-73A7827459FC");
        var exercise2 = new Guid("914C44B9-528F-46A9-861A-A88BB2B86E40");
        
        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1, exercise2});
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise1 });
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, daily =>  daily.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercise1);
            },
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercise2);
            });
    }

    [Fact]
    public void given_new_exercises_for_already_existed_day_but_different_time_should_add()
    {
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay1 = TimeOfDay.Evening;
        var timeOfDay2 = TimeOfDay.Morning;
        var exercise1 = new Guid("A0374058-260E-479B-A0B3-73A7827459FC");
        var exercise2 = new Guid("A0374058-260E-479B-A0B3-73A7827459FC");
        
        var dailyPlanner1 = new DailyPlanner(dayOfWeek, timeOfDay1, new []{ exercise1 });
        var dailyPlanner2 = new DailyPlanner(dayOfWeek, timeOfDay2, new []{ exercise1 });
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay1, new []{ exercise1 });
        planner.AddExercises(dayOfWeek, timeOfDay2, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, 
            daily =>  daily.ShouldBe(dailyPlanner1),
            daily =>  daily.ShouldBe(dailyPlanner2));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay1);
                dp.PlannerExercise.ShouldBe(exercise1);
            },
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay2);
                dp.PlannerExercise.ShouldBe(exercise2);
            });
    }
    
    [Fact]
    public void give_empty_exercise_should_ignore()
    {
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.WholeDay;
        var exercise1 = new Guid("A0374058-260E-479B-A0B3-73A7827459FC");
        var exercise2 = Guid.Empty;
        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1 });
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise1 });
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise2 });
        
        Assert.Collection(planner.DailyPlanners, daily =>  daily.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event =>
            {
                @event.ShouldBeOfType<AddedPlannerExercise>();
                var dp = @event as AddedPlannerExercise;
                dp.DayOfWeek.ShouldBe(dayOfWeek);
                dp.TimeOfDay.ShouldBe(timeOfDay);
                dp.PlannerExercise.ShouldBe(exercise1);
            });
    }

    [Fact]
    public void given_duplicated_exercise_for_monday_whole_day_should_ignore()
    {
        var dayOfWeek = DayOfWeek.Monday;
        var timeOfDay = TimeOfDay.WholeDay;
        var exercise1 = new Guid("A0374058-260E-479B-A0B3-73A7827459FC");
        var dailyPlanner = new DailyPlanner(dayOfWeek, timeOfDay, new []{ exercise1 });
        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise1 });
        planner.AddExercises(dayOfWeek, timeOfDay, new []{ exercise1 });
        
        Assert.Collection(planner.DailyPlanners, daily =>  daily.ShouldBe(dailyPlanner));
        
        Assert.Collection(planner.Events, 
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>(),
            @event => @event.ShouldBeOfType<AddedPlannerExercise>());
    }

    [Fact]
    public void given_empty__exercises_should_ignore()
    {
        
        var dayOfWeek = DayOfWeek.Thursday;
        var timeOfDay = TimeOfDay.WholeDay;

        var planner = Arrange();
        planner.AddExercises(dayOfWeek, timeOfDay, ArraySegment<Guid>.Empty);
        
        planner.DailyPlanners.ShouldBeEmpty();
        Assert.Collection(planner.Events,
            @event => @event.ShouldBeOfType<CreatedNewPlannerEvent>());
    }

    private Core.Entities.Planner Arrange()
    {
        return Core.Entities.Planner.Create(
            new Guid("1E5A57C9-496F-4DA3-94E2-24721FD3A4B4"),
            new Guid("3A27D9F5-5044-4C2E-B9B3-C624F3E78BE9"),
            DateTime.Now.AddMonths(-1), 
            DateTime.Now.AddMonths(1));
    }
}