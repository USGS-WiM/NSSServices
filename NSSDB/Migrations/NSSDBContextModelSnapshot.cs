﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSSDB;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    [DbContext(typeof(NSSDBContext))]
    partial class NSSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("nss")
                .HasAnnotation("Npgsql:PostgresExtension:postgis", "'postgis', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("NSSDB.Resources.Citation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<string>("CitationURL")
                        .IsRequired();

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Citations");
                });

            modelBuilder.Entity("NSSDB.Resources.Coefficient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Criteria")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("RegressionRegionID");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Coefficients");
                });

            modelBuilder.Entity("NSSDB.Resources.Equation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("DA_Exponent");

                    b.Property<double?>("EquivalentYears");

                    b.Property<string>("Expression")
                        .IsRequired();

                    b.Property<DateTime>("LastModified");

                    b.Property<int?>("OrderIndex");

                    b.Property<int?>("PredictionIntervalID");

                    b.Property<int>("RegressionRegionID");

                    b.Property<int>("RegressionTypeID");

                    b.Property<int>("StatisticGroupTypeID");

                    b.Property<int>("UnitTypeID");

                    b.HasKey("ID");

                    b.HasIndex("PredictionIntervalID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Equations");
                });

            modelBuilder.Entity("NSSDB.Resources.EquationError", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EquationID");

                    b.Property<int>("ErrorTypeID");

                    b.Property<double>("Value");

                    b.HasKey("ID");

                    b.HasIndex("EquationID");

                    b.ToTable("EquationErrors");
                });

            modelBuilder.Entity("NSSDB.Resources.EquationUnitType", b =>
                {
                    b.Property<int>("EquationID");

                    b.Property<int>("UnitTypeID");

                    b.HasKey("EquationID", "UnitTypeID");

                    b.ToTable("EquationUnitTypes");
                });

            modelBuilder.Entity("NSSDB.Resources.Limitation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Criteria")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("RegressionRegionID");

                    b.HasKey("ID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("Limitations");
                });

            modelBuilder.Entity("NSSDB.Resources.Manager", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("OtherInfo");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PrimaryPhone");

                    b.Property<int>("RoleID");

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("SecondaryPhone");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("Username");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("NSSDB.Resources.PredictionInterval", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("BiasCorrectionFactor");

                    b.Property<string>("CovarianceMatrix");

                    b.Property<DateTime>("LastModified");

                    b.Property<double?>("Student_T_Statistic");

                    b.Property<double?>("Variance");

                    b.Property<string>("XIRowVector");

                    b.HasKey("ID");

                    b.ToTable("PredictionIntervals");
                });

            modelBuilder.Entity("NSSDB.Resources.Region", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("Code");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("NSSDB.Resources.RegionManager", b =>
                {
                    b.Property<int>("ManagerID");

                    b.Property<int>("RegionID");

                    b.HasKey("ManagerID", "RegionID");

                    b.HasIndex("RegionID");

                    b.ToTable("RegionManager");
                });

            modelBuilder.Entity("NSSDB.Resources.RegionRegressionRegion", b =>
                {
                    b.Property<int>("RegionID");

                    b.Property<int>("RegressionRegionID");

                    b.HasKey("RegionID", "RegressionRegionID");

                    b.HasIndex("RegressionRegionID");

                    b.ToTable("RegionRegressionRegions");
                });

            modelBuilder.Entity("NSSDB.Resources.RegressionRegion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CitationID");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastModified");

                    b.Property<Polygon>("Location");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("CitationID");

                    b.HasIndex("Code");

                    b.ToTable("RegressionRegions");
                });

            modelBuilder.Entity("NSSDB.Resources.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NSSDB.Resources.Variable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CoefficientID");

                    b.Property<string>("Comments");

                    b.Property<int?>("EquationID");

                    b.Property<DateTime>("LastModified");

                    b.Property<int?>("LimitationID");

                    b.Property<double?>("MaxValue");

                    b.Property<double?>("MinValue");

                    b.Property<int?>("RegressionTypeID");

                    b.Property<int>("UnitTypeID");

                    b.Property<int>("VariableTypeID");

                    b.HasKey("ID");

                    b.HasIndex("CoefficientID");

                    b.HasIndex("EquationID");

                    b.HasIndex("LimitationID");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("NSSDB.Resources.VariableUnitType", b =>
                {
                    b.Property<int>("VariableID");

                    b.Property<int>("UnitTypeID");

                    b.HasKey("VariableID", "UnitTypeID");

                    b.ToTable("VariableUnitTypes");
                });

            modelBuilder.Entity("NSSDB.Resources.Coefficient", b =>
                {
                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("RegressionRegionCoefficients")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.Equation", b =>
                {
                    b.HasOne("NSSDB.Resources.PredictionInterval", "PredictionInterval")
                        .WithMany("Equation")
                        .HasForeignKey("PredictionIntervalID");

                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("Equations")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.EquationError", b =>
                {
                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("EquationErrors")
                        .HasForeignKey("EquationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.EquationUnitType", b =>
                {
                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("EquationUnitTypes")
                        .HasForeignKey("EquationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.Limitation", b =>
                {
                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("Limitations")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.Manager", b =>
                {
                    b.HasOne("NSSDB.Resources.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("NSSDB.Resources.RegionManager", b =>
                {
                    b.HasOne("NSSDB.Resources.Manager", "Manager")
                        .WithMany("RegionManagers")
                        .HasForeignKey("ManagerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NSSDB.Resources.Region", "Region")
                        .WithMany("RegionManagers")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.RegionRegressionRegion", b =>
                {
                    b.HasOne("NSSDB.Resources.Region", "Region")
                        .WithMany("RegionRegressionRegions")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NSSDB.Resources.RegressionRegion", "RegressionRegion")
                        .WithMany("RegionRegressionRegions")
                        .HasForeignKey("RegressionRegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.RegressionRegion", b =>
                {
                    b.HasOne("NSSDB.Resources.Citation", "Citation")
                        .WithMany("RegressionRegions")
                        .HasForeignKey("CitationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NSSDB.Resources.Variable", b =>
                {
                    b.HasOne("NSSDB.Resources.Coefficient", "Coefficient")
                        .WithMany("Variables")
                        .HasForeignKey("CoefficientID");

                    b.HasOne("NSSDB.Resources.Equation", "Equation")
                        .WithMany("Variables")
                        .HasForeignKey("EquationID");

                    b.HasOne("NSSDB.Resources.Limitation", "Limitation")
                        .WithMany("Variables")
                        .HasForeignKey("LimitationID");
                });

            modelBuilder.Entity("NSSDB.Resources.VariableUnitType", b =>
                {
                    b.HasOne("NSSDB.Resources.Variable", "Variable")
                        .WithMany("VariableUnitTypes")
                        .HasForeignKey("VariableID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
