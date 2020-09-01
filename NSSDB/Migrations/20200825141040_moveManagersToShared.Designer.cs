﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSSDB;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    [DbContext(typeof(NSSDBContext))]
    [Migration("20200825141040_moveManagersToShared")]
    partial class moveManagersToShared
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("nss")
                .HasAnnotation("Npgsql:PostgresExtension:postgis", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NSSDB.Resources.Citation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CitationURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Citations");
                });

            modelBuilder.Entity("NSSDB.Resources.Coefficient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RegressionRegionID")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Coefficients");
                });

            modelBuilder.Entity("NSSDB.Resources.Equation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double?>("DA_Exponent")
                        .HasColumnType("double precision");

                    b.Property<double?>("EquivalentYears")
                        .HasColumnType("double precision");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrderIndex")
                        .HasColumnType("integer");

                    b.Property<int?>("PredictionIntervalID")
                        .HasColumnType("integer");

                    b.Property<int>("RegressionRegionID")
                        .HasColumnType("integer");

                    b.Property<int>("RegressionTypeID")
                        .HasColumnType("integer");

                    b.Property<int>("StatisticGroupTypeID")
                        .HasColumnType("integer");

                    b.Property<int>("UnitTypeID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("PredictionIntervalID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Equations");
                });

            modelBuilder.Entity("NSSDB.Resources.EquationError", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EquationID")
                        .HasColumnType("integer");

                    b.Property<int>("ErrorTypeID")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("EquationID");

                    b.ToTable("EquationErrors");
                });

            modelBuilder.Entity("NSSDB.Resources.EquationUnitType", b =>
                {
                    b.Property<int>("EquationID")
                        .HasColumnType("integer");

                    b.Property<int>("UnitTypeID")
                        .HasColumnType("integer");

                    b.HasKey("EquationID", "UnitTypeID");

                    b.ToTable("EquationUnitTypes");
                });

            modelBuilder.Entity("NSSDB.Resources.Limitation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("RegressionRegionID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Limitations");
                });

            modelBuilder.Entity("NSSDB.Resources.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AssociatedCodes")
                        .HasColumnType("text");

                    b.Property<Geometry>("Geometry")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.HasKey("ID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("NSSDB.Resources.Method", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Methods");
                });

            modelBuilder.Entity("NSSDB.Resources.PredictionInterval", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double?>("BiasCorrectionFactor")
                        .HasColumnType("double precision");

                    b.Property<string>("CovarianceMatrix")
                        .HasColumnType("text");

                    b.Property<double?>("DegreesOfFreedom")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double?>("Student_T_Statistic")
                        .HasColumnType("double precision");

                    b.Property<double?>("Variance")
                        .HasColumnType("double precision");

                    b.Property<string>("XIRowVector")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("PredictionIntervals");
                });

            modelBuilder.Entity("NSSDB.Resources.RegionRegressionRegion", b =>
                {
                    b.Property<int>("RegionID")
                        .HasColumnType("integer");

                    b.Property<int>("RegressionRegionID")
                        .HasColumnType("integer");

                    b.HasKey("RegionID", "RegressionRegionID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("RegionRegressionRegions");
                });

            modelBuilder.Entity("NSSDB.Resources.RegressionRegion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CitationID")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LocationID")
                        .HasColumnType("integer");

                    b.Property<int?>("MethodID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("StatusID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("CitationID");

                    b.HasIndex("Code");

                    b.HasIndex("LocationID");

                    b.HasIndex("MethodID");

                    b.HasIndex("StatusID");

                    b.ToTable("RegressionRegions");
                });

            modelBuilder.Entity("NSSDB.Resources.Status", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("NSSDB.Resources.Variable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CoefficientID")
                        .HasColumnType("integer");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<int?>("EquationID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LimitationID")
                        .HasColumnType("integer");

                    b.Property<double?>("MaxValue")
                        .HasColumnType("double precision");

                    b.Property<double?>("MinValue")
                        .HasColumnType("double precision");

                    b.Property<int?>("RegressionTypeID")
                        .HasColumnType("integer");

                    b.Property<int>("UnitTypeID")
                        .HasColumnType("integer");

                    b.Property<int>("VariableTypeID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("CoefficientID");

                    b.HasIndex("EquationID");

                    b.HasIndex("LimitationID");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("NSSDB.Resources.VariableUnitType", b =>
                {
                    b.Property<int>("VariableID")
                        .HasColumnType("integer");

                    b.Property<int>("UnitTypeID")
                        .HasColumnType("integer");

                    b.HasKey("VariableID", "UnitTypeID");

                    b.ToTable("VariableUnitTypes");
                });

            modelBuilder.Entity("NSSDB.Resources.Coefficient", b =>
                {
                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("RegressionRegionCoefficients")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.Equation", b =>
                {
                    b.HasOne("NSSDB.Resources.PredictionInterval", "PredictionInterval")
                        .WithMany("Equation")
                        .HasForeignKey("PredictionIntervalID");

                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("Equations")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.EquationError", b =>
                {
                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("EquationErrors")
                        .HasForeignKey("EquationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.EquationUnitType", b =>
                {
                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("EquationUnitTypes")
                        .HasForeignKey("EquationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.Limitation", b =>
                {
                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("Limitations")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.RegionRegressionRegion", b =>
                {
                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("RegionRegressionRegions")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NSSDB.Resources.RegressionRegion", b =>
                {
                    b.HasOne("NSSDB.Resources.Citation", "Citation")
                        .WithMany("RegressionRegions")
                        .HasForeignKey("CitationID");

                    b.HasOne("NSSDB.Resources.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NSSDB.Resources.Method", "Method")
                        .WithMany()
                        .HasForeignKey("MethodID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NSSDB.Resources.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("NSSDB.Resources.Variable", b =>
                {
                    b.HasOne("NSSDB.Resources.Coefficient", "Coefficient")
                        .WithMany("Variables")
                        .HasForeignKey("CoefficientID");

                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("Variables")
                        .HasForeignKey("EquationID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NSSDB.Resources.Limitation", "Limitation")
                        .WithMany("Variables")
                        .HasForeignKey("LimitationID");
                });

            modelBuilder.Entity("NSSDB.Resources.VariableUnitType", b =>
                {
                    b.HasOne("NSSDB.Resources.Variable", "Variable")
                        .WithMany("VariableUnitTypes")
                        .HasForeignKey("VariableID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
