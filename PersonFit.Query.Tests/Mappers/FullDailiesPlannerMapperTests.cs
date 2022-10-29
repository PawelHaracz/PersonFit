using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Dapper.Json;
using PersonFit.Core.Enums;
using PersonFit.Core.Queries;
using PersonFit.Query.Planner.Application.Daos;
using PersonFit.Query.Planner.Application.Dtos;
using PersonFit.Query.Planner.Application.Enums;
using PersonFit.Query.Planner.Application.ValueObjects;
using PersonFit.Query.Planner.Infrastructure.Mappers;
using Shouldly;
using Xunit;

namespace PersonFit.Query.Tests.Mappers;

public class FullDailiesPlannerMapperTests
{
    
    private readonly IDaoToDtoMapper<IEnumerable<QueryFullDailiesPlannerDao>, FullDailiesPlannerDto> _mapper =  new FullDailiesPlannerMapper();

    [Fact]
    public void given_dailies_planner_should_map()
    {
        var plannerId = new Guid("01E7DFB9-B16A-4D17-AA3F-B73CADDD7723");
        var plannerStatus = PlannerStatus.Active;
        var plannerStartTime = DateTime.Now.AddDays(2);
        var plannerEndTime = DateTime.Now.AddDays(-2);
        var exercise1 = "plank1";
        var exercise2 = "plank2";

        var arrange = new[]
        {
            new QueryFullDailiesPlannerDao()
            {
                PlanId = plannerId,
                PlannerStatus = plannerStatus,
                PlannerEndTime = plannerEndTime,
                PlannerStartTime = plannerStartTime,
                ExerciseDescription = string.Empty,
                ExerciseMedia = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                ExerciseName = exercise1,
                ExerciseTags = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                DailyDayOfWork = DayOfWeek.Friday,
                DailyTimeOfWork = TimeOfDay.Evening,
                ExerciseRepetitions = new Json<IEnumerable<Repetition>>(
                    new[]
                    {
                        new Repetition
                        {
                            Count = 2,
                            Order = 1,
                            Note = string.Empty,
                            MeasurementUnit = "Mass"
                        },
                        new Repetition
                        {
                            Count = 12,
                            Order = 2,
                            Note = string.Empty,
                            MeasurementUnit = "Mass"
                        },
                    }
                )
            },
            new QueryFullDailiesPlannerDao()
            {
                PlanId = plannerId,
                PlannerStatus = plannerStatus,
                PlannerEndTime = plannerEndTime,
                PlannerStartTime = plannerStartTime,
                ExerciseDescription = string.Empty,
                ExerciseMedia = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                ExerciseName = exercise2,
                ExerciseTags = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                DailyDayOfWork = DayOfWeek.Friday,
                DailyTimeOfWork = TimeOfDay.Evening,
                ExerciseRepetitions = new Json<IEnumerable<Repetition>>(
                    new[]
                    {
                        new Repetition
                        {
                            Count = 2,
                            Order = 1,
                            Note = string.Empty,
                            MeasurementUnit = "Mass"
                        }
                    }
                )
            },
            new QueryFullDailiesPlannerDao()
            {
                PlanId = plannerId,
                PlannerStatus = plannerStatus,
                PlannerEndTime = plannerEndTime,
                PlannerStartTime = plannerStartTime,
                ExerciseDescription = string.Empty,
                ExerciseMedia = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                ExerciseName = exercise1,
                ExerciseTags = new Json<IEnumerable<string>>(ArraySegment<string>.Empty),
                DailyDayOfWork = DayOfWeek.Saturday,
                DailyTimeOfWork = TimeOfDay.Morning,
                ExerciseRepetitions = new Json<IEnumerable<Repetition>>(
                    new[]
                    {
                        new Repetition
                        {
                            Count = 2,
                            Order = 1,
                            Note = string.Empty,
                            MeasurementUnit = "Mass"
                        },
                        new Repetition
                        {
                            Count = 12,
                            Order = 2,
                            Note = string.Empty,
                            MeasurementUnit = "Mass"
                        },
                    }
                )
            },
        };
        
        
        var act = _mapper.Map(arrange);

        act.ShouldNotBeNull();
        act.Id.ShouldBe(plannerId);
        act.Status.ShouldBe(plannerStatus);
        act.EndTime.ShouldBe(DateOnly.FromDateTime(plannerEndTime));
        act.StartTime.ShouldBe(DateOnly.FromDateTime(plannerStartTime));
        Assert.Collection(act.DailyPlans, plan =>
        {
            plan.TimeOfDay.ShouldBe(TimeOfDay.Evening);
            plan.DayOfWeek.ShouldBe(DayOfWeek.Friday);
            Assert.Collection(plan.Exercises, ex =>
            {
                ex.Name.ShouldBe(exercise1);
                ex.Description.ShouldBe(string.Empty);
                ex.Media.ShouldBeEmpty();
                ex.Tags.ShouldBeEmpty();
                ex.Repetitions.ShouldNotBeEmpty();
            },
            ex =>
                {
                    ex.Name.ShouldBe(exercise2);
                    ex.Description.ShouldBe(string.Empty);
                    ex.Media.ShouldBeEmpty();
                    ex.Tags.ShouldBeEmpty();
                    ex.Repetitions.ShouldNotBeEmpty();
                });
        }, plan =>
        {
            plan.TimeOfDay.ShouldBe(TimeOfDay.Morning);
            plan.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
            Assert.Collection(plan.Exercises, ex =>
            {
                ex.Name.ShouldBe(exercise1);
                ex.Description.ShouldBe(string.Empty);
                ex.Media.ShouldBeEmpty();
                ex.Tags.ShouldBeEmpty();
                ex.Repetitions.ShouldNotBeEmpty();
            });
        });

    }
}