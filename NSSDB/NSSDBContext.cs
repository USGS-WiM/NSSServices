//------------------------------------------------------------------------------
//----- DB Context ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

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
using NSSDB.Resources;
using SharedDB.Resources;
using Microsoft.EntityFrameworkCore.Metadata;
//using SharedDB.Resources;
using System;
using System.Collections.Generic;
//specifying the data provider and connection string
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
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<PredictionInterval> PredictionIntervals { get; set; }        
        public virtual DbSet<RegionRegressionRegion> RegionRegressionRegions { get; set; }
        public virtual DbSet<Region> Regions { get; set; }        
        public virtual DbSet<RegressionRegion> RegressionRegions { get; set; }        
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Variable> Variables { get; set; }       
        public virtual DbSet<VariableUnitType> VariableUnitTypes { get; set; }        
        
        //Shared views
        public virtual DbSet<ErrorType> ErrorTypes { get; set; }
        public virtual DbSet<RegressionType> RegressionTypes { get; set; }
        public virtual DbSet<StatisticGroupType> StatisticGroupTypes { get; set; }
        public virtual DbSet<UnitConversionFactor> UnitConversionFactors { get; set; }
        public virtual DbSet<UnitSystemType> UnitSystemTypes { get; set; }
        public virtual DbSet<UnitType> UnitTypes { get; set; }
        public virtual DbSet<VariableType> VariableTypes { get; set; }

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

            //unique key based on combination of both keys (many to many tables)
            modelBuilder.Entity<RegionManager>().HasKey(k => new { k.ManagerID, k.RegionID });
            modelBuilder.Entity<VariableUnitType>().HasKey(k => new { k.VariableID, k.UnitTypeID });
            modelBuilder.Entity<EquationUnitType>().HasKey(k => new { k.EquationID, k.UnitTypeID });
            modelBuilder.Entity<RegionRegressionRegion>().HasKey(k => new { k.RegionID, k.RegressionRegionID });

            //Specify other unique constraints
            //EF Core currently does not support changing the value of alternate keys. We do have #4073 tracking removing this restriction though.
            //BTW it only needs to be an alternate key if you want it to be used as the target key of a relationship.If you just want a unique index, 
            //then use the HasIndex() method, rather than AlternateKey().Unique index values can be changed.
            modelBuilder.Entity<Manager>().HasIndex(k => k.Username);
            modelBuilder.Entity<Region>().HasIndex(k => k.Code);
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
                                         typeof(UnitType).FullName,typeof(VariableType).FullName }
                .Contains(entitytype.Name))
                { continue; }                 
                modelBuilder.Entity(entitytype.Name).Property<DateTime>("LastModified");
            }//next entitytype

            //cascade delete is default, rewrite behavior
            modelBuilder.Entity(typeof(EquationError).ToString(), b =>
            {
                b.HasOne(typeof(ErrorType).ToString(), "ErrorType")
                    .WithMany()
                    .HasForeignKey("ErrorTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(Manager).ToString(), b =>
            {
                b.HasOne(typeof(Role).ToString(), "Role")
                    .WithMany()
                    .HasForeignKey("RoleID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity(typeof(Equation).ToString(), b =>
            {
                b.HasOne(typeof(UnitType).ToString(), "UnitType")
                    .WithMany()
                    .HasForeignKey("UnitTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
            //Must Comment out after migration
            //modelBuilder.Ignore(typeof(ErrorType));
            //modelBuilder.Ignore(typeof(RegressionType));
            //modelBuilder.Ignore(typeof(StatisticGroupType));
            //modelBuilder.Ignore(typeof(UnitConversionFactor));
            //modelBuilder.Ignore(typeof(UnitSystemType));
            //modelBuilder.Ignore(typeof(UnitType));
            //modelBuilder.Ignore(typeof(VariableType));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning Add connectionstring for migrations
            //var connectionstring = "User ID=;Password=;Host=;Port=5432;Database=StatsDB;Pooling=true;";
            //optionsBuilder.UseNpgsql(connectionstring,x=> { x.MigrationsHistoryTable("_EFMigrationsHistory", "nss"); x.UseNetTopologySuite(); });
        }
    }
}
