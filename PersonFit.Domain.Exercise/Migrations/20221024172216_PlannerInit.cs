using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;

#nullable disable

namespace PersonFit.Domain.Exercise.Migrations
{
    public partial class PlannerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exercise");

            migrationBuilder.CreateTable(
                name: "Exercises",
                schema: "exercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<IEnumerable<string>>(type: "jsonb", nullable: true),
                    Media = table.Column<IEnumerable<Media>>(type: "jsonb", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises",
                schema: "exercise");
        }
    }
}
