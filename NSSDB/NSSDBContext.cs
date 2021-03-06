﻿//------------------------------------------------------------------------------
//----- DB Context ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Responsible for interacting with Database 
//
//discussion:   The primary class that is responsible for interacting with data as objects. 
//              The context class manages the entity objects during run time, which includes 
//              populating objects with data from a database, change tracking, and persisting 
//              data to the database.
//              
//
//   

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NSSDB.Resources;
using SharedDB.Resources;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSSDB
{
    public class NSSDBContext:DbContext
    {
        public virtual DbSet<Citation> Citations { get; set; }
        public virtual DbSet<Coefficient> Coefficients { get; set; }
        public virtual DbSet<EquationError> EquationErrors { get; set; }
        public virtual DbSet<Equation> Equations { get; set; }        
        public virtual DbSet<EquationUnitType> EquationUnitTypes { get; set; }        
        public virtual DbSet<Limitation> Limitations { get; set; }
        public virtual DbSet<PredictionInterval> PredictionIntervals { get; set; }        
        public virtual DbSet<RegionRegressionRegion> RegionRegressionRegions { get; set; }
        public virtual DbSet<RegressionRegion> RegressionRegions { get; set; }
        public virtual DbSet<Variable> Variables { get; set; }       
        public virtual DbSet<VariableUnitType> VariableUnitTypes { get; set; }     
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Method> Methods { get; set; }

        //Shared views
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

        public NSSDBContext() : base()
        {
        }
        public NSSDBContext(DbContextOptions<NSSDBContext> options) : base(options)
        {            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            //schema
            modelBuilder.HasDefaultSchema("nss");
            modelBuilder.Entity<ErrorType>().ToTable("ErrorType_view");
            modelBuilder.Entity<RegressionType>().ToTable("RegressionType_view");
            modelBuilder.Entity<StatisticGroupType>().ToTable("StatisticGroupType_view");
            modelBuilder.Entity<UnitConversionFactor>().ToTable("UnitConversionFactor_view");
            modelBuilder.Entity<UnitSystemType>().ToTable("UnitSystemType_view");
            modelBuilder.Entity<UnitType>().ToTable("UnitType_view");
            modelBuilder.Entity<VariableType>().ToTable("VariableType_view");
            modelBuilder.Entity<Region>().ToTable("Regions_view");
            modelBuilder.Entity<Manager>().ToTable("Managers_view");
            modelBuilder.Entity<RegionManager>().ToTable("RegionManager_view");

            //unique key based on combination of both keys (many to many tables)
            modelBuilder.Entity<RegionManager>().HasKey(k => new { k.ManagerID, k.RegionID });
            modelBuilder.Entity<VariableUnitType>().HasKey(k => new { k.VariableID, k.UnitTypeID });
            modelBuilder.Entity<EquationUnitType>().HasKey(k => new { k.EquationID, k.UnitTypeID });
            modelBuilder.Entity<RegionRegressionRegion>().HasKey(k => new { k.RegionID, k.RegressionRegionID });

            //Specify other unique constraints
            //EF Core currently does not support changing the value of alternate keys. We do have #4073 tracking removing this restriction though.
            //BTW it only needs to be an alternate key if you want it to be used as the target key of a relationship.If you just want a unique index, 
            //then use the HasIndex() method, rather than AlternateKey().Unique index values can be changed.
            modelBuilder.Entity<RegressionRegion>().HasIndex(k => k.Code);

            //add shadowstate  
            //https://stackoverflow.com/questions/9556474/how-do-i-automatically-update-a-timestamp-in-postgresql
            //https://x-team.com/blog/automatic-timestamps-with-postgresql/
            foreach (var entitytype in modelBuilder.Model.GetEntityTypes())
            {                
                //"EquationError","EquationUnitType","RegionManager","RegionRegressionRegion","VariableUnitType" 
                if (new List<string>() { typeof(EquationError).FullName, typeof(EquationUnitType).FullName,
                                         typeof(RegionManager).FullName,typeof(RegionRegressionRegion).FullName,
                                         typeof(VariableUnitType).FullName,typeof(ErrorType).FullName,typeof(RegressionType).FullName,
                                         typeof(StatisticGroupType).FullName,typeof(UnitConversionFactor).FullName,typeof(UnitSystemType).FullName,
                                         typeof(UnitType).FullName,typeof(VariableType).FullName,typeof(Status).FullName,typeof(Location).FullName,
                                         typeof(Method).FullName,typeof(Region).FullName,typeof(Manager).FullName}
                .Contains(entitytype.Name))
                { continue; }                 
                modelBuilder.Entity(entitytype.Name).Property<DateTime>("LastModified");
            }//next entitytype

            modelBuilder.Entity<Location>().Property<string>("AssociatedCodes");

            //cascade delete is default, rewrite behavior
            modelBuilder.Entity(typeof(EquationError).ToString(), b =>
            {
                b.HasOne(typeof(ErrorType).ToString(), "ErrorType")
                    .WithMany()
                    .HasForeignKey("ErrorTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(Equation).ToString(), b =>
            {
                b.HasOne(typeof(UnitType).ToString(), "UnitType")
                    .WithMany()
                    .HasForeignKey("UnitTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(RegressionRegion).ToString(), b =>
            {
                b.HasOne(typeof(Status).ToString(), "Status")
                    .WithMany()
                    .HasForeignKey("StatusID")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(typeof(Location).ToString(), "Location")
                    .WithMany()
                    .HasForeignKey("LocationID")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(typeof(Method).ToString(), "Method")
                    .WithMany()
                    .HasForeignKey("MethodID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Variable>()
                .HasOne(v => v.Equation)
                .WithMany(e => e.Variables)
                .HasForeignKey(v => v.EquationID)
                .OnDelete(DeleteBehavior.Cascade);

            //seed the db
            var path = Path.Combine(Environment.CurrentDirectory, "Data");
            //modelBuilder.Entity<Status>().HasData(JsonConvert.DeserializeObject<Status[]>(File.ReadAllText(Path.Combine(path, "Status.json"))));

            base.OnModelCreating(modelBuilder);
            //Must Comment out after migration
            //modelBuilder.Ignore(typeof(ErrorType));
            //modelBuilder.Ignore(typeof(RegressionType));
            //modelBuilder.Ignore(typeof(StatisticGroupType));
            //modelBuilder.Ignore(typeof(UnitConversionFactor));
            //modelBuilder.Ignore(typeof(UnitSystemType));
            //modelBuilder.Ignore(typeof(UnitType));
            //modelBuilder.Ignore(typeof(VariableType));
            //modelBuilder.Ignore(typeof(Manager));
            //modelBuilder.Ignore(typeof(Region));
            //modelBuilder.Ignore(typeof(RegionManager));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#error Remove connectionstring after migration
            //var connectionstring = "User ID=;Password=;Host=;Port=5432;Database=StatsDB;Pooling=true;";
            //optionsBuilder.UseNpgsql(connectionstring, x => { x.MigrationsHistoryTable("_EFMigrationsHistory", "nss"); x.UseNetTopologySuite(); });
        }
    }
}
