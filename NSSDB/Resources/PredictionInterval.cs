//------------------------------------------------------------------------------
//----- Resource ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WiM - USGS

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
namespace NSSDB.Resources
{
    public partial class PredictionInterval
    {
        [Required]
        public int ID { get; set; }
        public double? BiasCorrectionFactor { get; set; }
        public double? Student_T_Statistic { get; set; }
        public double? Variance { get; set; }
        public string XIRowVector { get; set; }
        public string CovarianceMatrix { get; set; }

        public virtual ICollection<Equation> Equation { get; set; }

    }
}
