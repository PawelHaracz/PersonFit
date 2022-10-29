using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;

#nullable disable

namespace PersonFit.Domain.Exercise.Migrations
{
    public partial class ExceriseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exercise");

            migrationBuilder.CreateTable(
                name: "exercises",
                schema: "exercise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    tags = table.Column<IEnumerable<string>>(type: "jsonb", nullable: true),
                    media = table.Column<IEnumerable<Media>>(type: "jsonb", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercises", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exercises",
                schema: "exercise");
        }
    }
}
