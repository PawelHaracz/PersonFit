﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonFit.Domain.Exercise.Infrastructure.Postgres;
using PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;

#nullable disable

namespace PersonFit.Domain.Exercise.Migrations
{
    [DbContext(typeof(PostgresExerciseDomainContext))]
    partial class PostgresExerciseDomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("exercise")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents.ExerciseDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<IEnumerable<Media>>("Media")
                        .HasColumnType("jsonb")
                        .HasColumnName("media");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<IEnumerable<string>>("Tags")
                        .HasColumnType("jsonb")
                        .HasColumnName("tags");

                    b.Property<int>("Version")
                        .HasColumnType("integer")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_exercises");

                    b.ToTable("exercises", "exercise");
                });
#pragma warning restore 612, 618
        }
    }
}
