﻿// <auto-generated />
using ElevatorSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElevatorSystem.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230907141025_InitialCreation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElevatorSystem.Domain.Models.Elevator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentFloor")
                        .HasColumnType("int");

                    b.Property<int>("Direction")
                        .HasColumnType("int");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<int>("MaintenanceCount")
                        .HasColumnType("int");

                    b.Property<int>("OccupantsCount")
                        .HasColumnType("int");

                    b.Property<int>("RequestedDirection")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TargetFloor")
                        .HasColumnType("int");

                    b.Property<int>("WeightLimit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Elevators");
                });

            modelBuilder.Entity("ElevatorSystem.Domain.Models.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.Property<int>("TotalPeopleGoingDown")
                        .HasColumnType("int");

                    b.Property<int>("TotalPeopleGoingUp")
                        .HasColumnType("int");

                    b.Property<int>("WaitingOccupants")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Floors");
                });
#pragma warning restore 612, 618
        }
    }
}
