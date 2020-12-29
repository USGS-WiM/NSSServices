//------------------------------------------------------------------------------
//----- Resource ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Simple Plain Old Class Object (POCO) 
//
//discussion:   POCO's arn't derived from special base classed nor do they return any special types for their properties.
//              
//
//     

using NpgsqlTypes;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using SharedDB.Resources;
using Newtonsoft.Json;
using NSSDB.Resources; // structure might be wrong for some things, like prediction interval and citation (those that are separate but exist in both)
using System;
using NSSAgent.ServiceAgents;
using WIM.Utilities.Resources;

namespace NSSAgent.Resources
{
    public partial class GageStatsStation
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int AgencyID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool? IsRegulated { get; set; }
        [Required]
        public int StationTypeID { get; set; }
        [Required]
        public object Location { get; set; }
        public int? RegionID { get; set; }

        public ICollection<Statistic> Statistics { get; set; }
        public ICollection<Characteristic> Characteristics { get; set; }
        public Agency Agency { get; set; }
        public StationType StationType { get; set; }
        public Region Region { get; set; }
    }

    public partial class Statistic
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int StatisticGroupTypeID { get; set; }
        [Required]
        public int RegressionTypeID { get; set; }
        [Required]
        public int StationID { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public int UnitTypeID { get; set; }
        public string Comments { get; set; }
        [Required]
        public bool IsPreferred { get; set; }
        public double? YearsofRecord { get; set; }
        public int? CitationID { get; set; }
        public int? PredictionIntervalID { get; set; }

        public ICollection<StatisticError> StatisticErrors { get; set; }
        public virtual Citation Citation { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual StatisticGroupType StatisticGroupType { get; set; }
        public virtual RegressionType RegressionType { get; set; }
        public virtual GageStatsStation Station { get; set; }
        public virtual PredictionInterval PredictionInterval { get; set; }
    }

    public partial class Characteristic
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int StationID { get; set; }
        [Required]
        public int VariableTypeID { get; set; }
        [Required]
        public int UnitTypeID { get; set; }
        public int? CitationID { get; set; }
        [Required]
        public double Value { get; set; }
        public string Comments { get; set; }

        public virtual VariableType VariableType { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual Citation Citation { get; set; }
        public virtual GageStatsStation Station { get; set; }
    }

    public partial class StatisticError
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int StatisticID { get; set; }
        [Required]
        public int ErrorTypeID { get; set; }
        [Required]
        public double Value { get; set; }

        public virtual Statistic Statistic { get; set; }
        public virtual ErrorType ErrorType { get; set; }
    }

    public partial class Agency
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        [JsonIgnore]
        public ICollection<GageStatsStation> Stations { get; set; }

    }

    public partial class StationType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        [JsonIgnore]
        public ICollection<GageStatsStation> Stations { get; set; }

    }

}
