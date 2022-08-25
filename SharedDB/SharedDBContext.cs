//------------------------------------------------------------------------------
//----- DB Context ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2018 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Resonsible for interacting with Database 
//
//discussion:   The primary class that is responsible for interacting with data as objects. 
//              The context class manages the entity objects during run time, which includes 
//              populating objects with data from a database, change tracking, and persisting 
//              data to the database.
//              
//
//   

using Microsoft.EntityFrameworkCore;
using SharedDB.Resources;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using Newtonsoft.Json;
using System.IO;

namespace SharedDB
{
    public class SharedDBContext:DbContext
    {
        public virtual DbSet<ApplicationInfo> ApplicationInfo { get; set; }
        public virtual DbSet<GeneralInfomation> GeneralInfomation { get; set; }

        public virtual DbSet<ErrorType> ErrorTypes { get; set; }
        public virtual DbSet<RegressionType> RegressionTypes { get; set; }
        public virtual DbSet<StatisticGroupType> StatisticGroupTypes { get; set; }
        public virtual DbSet<UnitConversionFactor> UnitConversionFactors { get; set; }
        public virtual DbSet<UnitSystemType> UnitSystemTypes { get; set; }
        public virtual DbSet<UnitType> UnitTypes { get; set; }
        public virtual DbSet<VariableType> VariableTypes { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionManager> RegionManager { get; set; }
       
        public SharedDBContext() : base()
        {
        }
        public SharedDBContext(DbContextOptions<SharedDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //specify db schema
            modelBuilder.HasDefaultSchema("shared");
            modelBuilder.Entity<ErrorType>().ToTable("ErrorType", "shared");
            modelBuilder.Entity<RegressionType>().ToTable("RegressionType", "shared");
            modelBuilder.Entity<StatisticGroupType>().ToTable("StatisticGroupType", "shared");
            modelBuilder.Entity<UnitConversionFactor>().ToTable("UnitConversionFactor", "shared");            
            modelBuilder.Entity<UnitSystemType>().ToTable("UnitSystemType", "shared");
            modelBuilder.Entity<UnitType>().ToTable("UnitType", "shared");
            modelBuilder.Entity<VariableType>().ToTable("VariableType", "shared");
            modelBuilder.Entity<Region>().ToTable("Regions", "shared");
            modelBuilder.Entity<Manager>().ToTable("Managers", "shared");
            modelBuilder.Entity<RegionManager>().ToTable("RegionManager", "shared");

            modelBuilder.Entity<ApplicationInfo>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("ApplicationInfo", "shared");
                entity.Property(e => e.Application).HasMaxLength(255);
                entity.Property(e => e.ComputationMethod).HasColumnName("Computation method");
                entity.Property(e => e.DataSource).HasColumnName("Data source");
                entity.Property(e => e.ReportVar).HasColumnName("Report var");
                entity.Property(e => e.StatisticLabel)
                    .HasColumnName("Statistic label")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<GeneralInfomation>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("GeneralInfomation", "shared");
                entity.Property(e => e.ArcGisVersionUsedForProcessing).HasColumnName("ArcGIS version used for processing");
                entity.Property(e => e.BasinCharacteristicsComputedUsingContinuousParameterGrids).HasColumnName("Basin characteristics computed using continuous parameter grids");
                entity.Property(e => e.DemSourceAndDate).HasColumnName("DEM source and date");
                entity.Property(e => e.ExcludedAreas).HasColumnName("Excluded areas");
                entity.Property(e => e.GageBasinBoundariesSource).HasColumnName("Gage basin boundaries source");
                entity.Property(e => e.HydrographySource).HasColumnName("Hydrography source");
                entity.Property(e => e.InteractiveSnappingTolerance).HasColumnName("Interactive snapping tolerance");
                entity.Property(e => e.InwallsUsedSource).HasColumnName("Inwalls used & source");
                entity.Property(e => e.ProjectAreaGeneralDescription).HasColumnName("Project area general description");
                entity.Property(e => e.ProjectionOfDemAndDerivatives).HasColumnName("Projection of DEM and derivatives");
                entity.Property(e => e.ResolutionOfDemAndDerivatives).HasColumnName("Resolution of DEM and derivatives");
                entity.Property(e => e.SpecialFunctionality).HasColumnName("Special functionality");
                entity.Property(e => e.TopogridUsed).HasColumnName("Topogrid used?");
                entity.Property(e => e.TypesOfStatisticsAvailable).HasColumnName("Types of statistics available");
            });

            //unique key based on combination of both keys (many to many tables)
            modelBuilder.Entity<RegionManager>().HasKey(k => new { k.ManagerID, k.RegionID });

            //Specify other unique constraints
            //EF Core currently does not support changing the value of alternate keys. We do have #4073 tracking removing this restriction though.
            //BTW it only needs to be an alternate key if you want it to be used as the target key of a relationship.If you just want a unique index, 
            //then use the HasIndex() method, rather than AlternateKey().Unique index values can be changed.
            modelBuilder.Entity<ErrorType>().HasIndex(k => k.Code).IsUnique();
            modelBuilder.Entity<RegressionType>().HasIndex(k => k.Code).IsUnique();
            modelBuilder.Entity<StatisticGroupType>().HasIndex(k => k.Code).IsUnique();
            modelBuilder.Entity<UnitType>().HasIndex(k => k.Name).IsUnique();
            modelBuilder.Entity<UnitType>().HasIndex(k => k.Abbreviation).IsUnique();
            modelBuilder.Entity<UnitSystemType>().HasIndex(k => k.UnitSystem).IsUnique();
            modelBuilder.Entity<VariableType>().HasIndex(k => k.Code).IsUnique();
            modelBuilder.Entity<Region>().HasIndex(k => k.Code).IsUnique();
            modelBuilder.Entity<Manager>().HasIndex(k => k.Username).IsUnique();

            //cascade delete is default, rewrite behavior
            modelBuilder.Entity(typeof(UnitConversionFactor).ToString(), b =>
            {
                b.HasOne(typeof(UnitType).ToString(), "UnitTypeIn")
                    .WithMany("UnitConversionFactorsIn")
                    .HasForeignKey("UnitTypeInID")
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(typeof(UnitType).ToString(), "UnitTypeOut")
                    .WithMany("UnitConversionFactorsOut")
                    .HasForeignKey("UnitTypeOutID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(UnitType).ToString(), b =>
            {
                b.HasOne(typeof(UnitSystemType).ToString(), "UnitSystemType")
                    .WithMany("UnitTypes")
                    .HasForeignKey("UnitSystemTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(VariableType).ToString(), b =>
            {
                b.HasOne(typeof(UnitType).ToString(), "MetricUnitType")
                    .WithMany()
                    .HasForeignKey("MetricUnitTypeID")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
                b.HasOne(typeof(UnitType).ToString(), "EnglishUnitType")
                    .WithMany()
                    .HasForeignKey("EnglishUnitTypeID")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
                b.HasOne(typeof(StatisticGroupType).ToString(), "StatisticGroupType")
                    .WithMany()
                    .HasForeignKey("StatisticGroupTypeID")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            //seed the db
            //var path = Path.Combine(Environment.CurrentDirectory, "Data");
            //modelBuilder.Entity<ErrorType>().HasData(JsonConvert.DeserializeObject<ErrorType[]>(File.ReadAllText(Path.Combine(path, "ErrorType.json"))));
            //modelBuilder.Entity<RegressionType>().HasData(JsonConvert.DeserializeObject<RegressionType[]>(File.ReadAllText(Path.Combine(path, "RegressionType.json"))));
            //modelBuilder.Entity<StatisticGroupType>().HasData(JsonConvert.DeserializeObject<StatisticGroupType[]>(File.ReadAllText(Path.Combine(path, "StatisticGroupType.json"))));
            //modelBuilder.Entity<UnitConversionFactor>().HasData(JsonConvert.DeserializeObject<UnitConversionFactor[]>(File.ReadAllText(Path.Combine(path, "UnitConversionFactor.json"))));
            //modelBuilder.Entity<UnitSystemType>().HasData(JsonConvert.DeserializeObject<UnitSystemType[]>(File.ReadAllText(Path.Combine(path, "UnitSystemType.json"))));
            //modelBuilder.Entity<UnitType>().HasData(JsonConvert.DeserializeObject<UnitType[]>(File.ReadAllText(Path.Combine(path, "UnitType.json"))));
            //modelBuilder.Entity<VariableType>().HasData(JsonConvert.DeserializeObject<VariableType[]>(File.ReadAllText(Path.Combine(path, "VariableType.json"))));


            base.OnModelCreating(modelBuilder);             
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning Add connectionstring for migrations
            //var connectionstring = "User ID=;Password=;Host=;Port=5432;Database=StatsDB;Pooling=true;";
            //optionsBuilder.UseNpgsql(connectionstring, x => x.MigrationsHistoryTable("_EFMigrationsHistory", "shared"));
        }
    }
}
