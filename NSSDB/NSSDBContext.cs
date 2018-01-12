//------------------------------------------------------------------------------
//----- DB Context ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WiM - USGS

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
using Microsoft.EntityFrameworkCore.Metadata;
using System;
//specifying the data provider and connection string
namespace NSSDB
{
    public class NSSDBContext:DbContext
    {
        public virtual DbSet<Citation> Citations { get; set; }
        public virtual DbSet<Equation> Equations { get; set; }
        public virtual DbSet<EquationError> EquationErrors { get; set; }
        public virtual DbSet<EquationUnitType> EquationUnitTypes { get; set; }
        public virtual DbSet<ErrorType> ErrorTypes { get; set; }
        public virtual DbSet<Limitation> Limitations { get; set; }
        public virtual DbSet<PredictionInterval> PredictionIntervals { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionRegressionRegion> RegionRegressionRegions { get; set; }
        public virtual DbSet<RegressionRegion> RegressionRegions { get; set; }
        public virtual DbSet<RegressionType> RegressionTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StatisticGroupType> StatisticGroupTypes { get; set; }
        public virtual DbSet<UnitConversionFactor> UnitConversionFactors { get; set; }
        public virtual DbSet<UnitSystemType> UnitSystemTypes { get; set; }
        public virtual DbSet<UnitType> UnitTypes { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Variable> Variables { get; set; }
        public virtual DbSet<VariableType> VariableTypes { get; set; }
        public virtual DbSet<VariableUnitType> VariableUnitTypes { get; set; }
        public virtual DbSet<RegressionRegionCoefficient> RegressionRegionCoefficients { get; set; }

        public NSSDBContext() : base()
        {
        }
        public NSSDBContext(DbContextOptions<NSSDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //admin schema
            modelBuilder.Entity<VariableType>().ToTable("VariableType", "NSSAdmin");
            modelBuilder.Entity<UnitType>().ToTable("UnitType", "NSSAdmin");
            modelBuilder.Entity<UnitConversionFactor>().ToTable("UnitConversionFactor", "NSSAdmin");
            modelBuilder.Entity<UnitSystemType>().ToTable("UnitSystemType", "NSSAdmin");
            modelBuilder.Entity<ErrorType>().ToTable("ErrorType", "NSSAdmin");
            modelBuilder.Entity<StatisticGroupType>().ToTable("StatisticGroupType", "NSSAdmin");
            modelBuilder.Entity<RegressionType>().ToTable("RegressionType", "NSSAdmin");


            //unique key based on combination of both keys (many to many tables)
            modelBuilder.Entity<RegionManager>().HasKey(k => new { k.ManagerID, k.RegionID });
            modelBuilder.Entity<VariableUnitType>().HasKey(k => new { k.VariableID, k.UnitTypeID });
            modelBuilder.Entity<EquationUnitType>().HasKey(k => new { k.EquationID, k.UnitTypeID });
            modelBuilder.Entity<RegionRegressionRegion>().HasKey(k => new { k.RegionID, k.RegressionRegionID });

            //Specify other unique constraints
            //EF Core currently does not support changing the value of alternate keys. We do have #4073 tracking removing this restriction though.
            //BTW it only needs to be an alternate key if you want it to be used as the target key of a relationship.If you just want a unique index, 
            //then use the HasIndex() method, rather than AlternateKey().Unique index values can be changed.
            modelBuilder.Entity<UnitSystemType>().HasIndex(k => k.UnitSystem);
            modelBuilder.Entity<UnitType>().HasIndex(k => k.Name);
            modelBuilder.Entity<UnitType>().HasIndex(k => k.Abbreviation);
            modelBuilder.Entity<Manager>().HasIndex(k => k.Username);
            modelBuilder.Entity<Region>().HasIndex(k => k.Code);
            modelBuilder.Entity<VariableType>().HasIndex(k => k.Code);            
            modelBuilder.Entity<ErrorType>().HasIndex(k => k.Code);
            modelBuilder.Entity<RegressionType>().HasIndex(k => k.Code);
            modelBuilder.Entity<RegressionRegion>().HasIndex(k => k.Code);
            modelBuilder.Entity<StatisticGroupType>().HasIndex(k => k.Code);
    
            //add shadowstate for when models change            
            modelBuilder.Entity("NSSDB.Resources.Equation").Property<DateTime>("LastModified");
            

            //cascade delete is default, rewrite behavior
            modelBuilder.Entity("NSSDB.Resources.EquationError", b =>
            { 
                b.HasOne("NSSDB.Resources.ErrorType", "ErrorType")
                    .WithMany()
                    .HasForeignKey("ErrorTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity("NSSDB.Resources.Manager", b =>
            {
                b.HasOne("NSSDB.Resources.Role", "Role")
                    .WithMany()
                    .HasForeignKey("RoleID")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity("NSSDB.Resources.Equation", b =>
            {
                b.HasOne("NSSDB.Resources.UnitType", "UnitType")
                    .WithMany()
                    .HasForeignKey("UnitTypeID")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);             
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning Add connectionstring for migrations
            var connectionstring = "";
            //optionsBuilder.UseNpgsql(connectionstring);
        }
    }
}
