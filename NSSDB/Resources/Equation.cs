﻿//------------------------------------------------------------------------------
//----- Resource ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Simple Plain Old Class Object (POCO) 
//
//discussion:   POCO's arn't derived from special base classed nor do they return any special types for their properties.
//              
//
//   

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedDB.Resources;
namespace NSSDB.Resources
{
    public partial class Equation
    {
        [Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int RegressionRegionID { get; set; }
        public int? PredictionIntervalID { get; set; }
        [Required]
        public int UnitTypeID { get; set; }
        [Required]
        public string Expression { get; set; }
        public double? DA_Exponent { get; set; }
        public int? OrderIndex { get; set; }
        [Required]
        public int RegressionTypeID { get; set; }
        [Required]
        public int StatisticGroupTypeID { get; set; }
        public double? EquivalentYears { get; set; }

        public virtual ICollection<Variable> Variables { get; set; }
        public virtual PredictionInterval PredictionInterval { get; set; }
        public virtual ICollection<EquationError> EquationErrors { get; set; }
        public virtual UnitType UnitType { get; set; }

        public ICollection<EquationUnitType> EquationUnitTypes { get; set; }

        public virtual RegressionRegion RegressionRegion { get; set; }
        public virtual StatisticGroupType StatisticGroupType { get; set; }
        public virtual RegressionType RegressionType { get; set; }


    }
}
