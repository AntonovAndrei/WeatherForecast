﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecast.Domain;

namespace WeatherForecast.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    partial class WeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WeatherForecast.Domain.Entities.Weather", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("AtmosphericPressure")
                        .HasColumnType("int");

                    b.Property<int?>("CloudCover")
                        .HasColumnType("int");

                    b.Property<int?>("CloudLowerLimit")
                        .HasColumnType("int");

                    b.Property<double>("DewPoint")
                        .HasColumnType("float");

                    b.Property<int?>("HorizontalVisibility")
                        .HasColumnType("int");

                    b.Property<double>("RelativeHumidity")
                        .HasColumnType("float");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.Property<string>("WeatherPhenomena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WindSpeed")
                        .HasColumnType("int");

                    b.HasKey("Date");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
