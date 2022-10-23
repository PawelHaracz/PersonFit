using PersonFit.Domain.Planner.Core.Enums;

namespace PersonFit.Domain.Planner.Api.Dtos;

public class ExerciseWorkoutDto
{
    public Guid ExerciseId { get; set; }
    public int Reps { get; set; }
    public  MeasurementUnit Unit { get; set; }
}