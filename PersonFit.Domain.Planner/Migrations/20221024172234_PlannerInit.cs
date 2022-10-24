using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;

#nullable disable

namespace PersonFit.Domain.Planner.Migrations
{
    public partial class PlannerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "planner");

            migrationBuilder.CreateTable(
                name: "PlannerExercises",
                schema: "planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Repetitions = table.Column<IEnumerable<RepetitionDao>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planners",
                schema: "planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    DailyPlanners = table.Column<IEnumerable<DailyPlannerDao>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerExercises",
                schema: "planner");

            migrationBuilder.DropTable(
                name: "Planners",
                schema: "planner");
        }
    }
}
