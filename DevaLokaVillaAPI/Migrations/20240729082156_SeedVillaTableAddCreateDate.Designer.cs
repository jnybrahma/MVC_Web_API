﻿// <auto-generated />
using System;
using DevaLokaVillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevaLokaVillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240729082156_SeedVillaTableAddCreateDate")]
    partial class SeedVillaTableAddCreateDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DevaLokaVillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 7, 29, 18, 21, 55, 799, DateTimeKind.Local).AddTicks(5943),
                            Details = "Royal villa with private spa",
                            ImageUrl = "https://placehold.co/",
                            Name = "Royal Villa",
                            Occupancy = 1,
                            Rate = 111.0,
                            Sqft = 1111,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 7, 29, 18, 21, 55, 799, DateTimeKind.Local).AddTicks(5965),
                            Details = "Premium villa with private pool",
                            ImageUrl = "https://placehold.co/",
                            Name = "Premium Villa",
                            Occupancy = 2,
                            Rate = 222.0,
                            Sqft = 2222,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 7, 29, 18, 21, 55, 799, DateTimeKind.Local).AddTicks(5967),
                            Details = "Luxury villa with private and Spa pool",
                            ImageUrl = "https://placehold.co/",
                            Name = "Luxury Villa",
                            Occupancy = 3,
                            Rate = 333.0,
                            Sqft = 33333,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 7, 29, 18, 21, 55, 799, DateTimeKind.Local).AddTicks(5969),
                            Details = "Presidential villa with private and spa pool",
                            ImageUrl = "https://placehold.co/",
                            Name = "Presidential Villa",
                            Occupancy = 4,
                            Rate = 444.0,
                            Sqft = 4444,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
