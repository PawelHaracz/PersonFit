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
                name: "planner_exercises",
                schema: "planner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    repetitions = table.Column<IEnumerable<RepetitionDao>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planner_exercises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planners",
                schema: "planner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    daily_planners = table.Column<IEnumerable<DailyPlannerDao>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planners", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "planner_exercises",
                schema: "planner");

            migrationBuilder.DropTable(
                name: "planners",
                schema: "planner");
        }
    }
}
