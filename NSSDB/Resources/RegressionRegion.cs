//------------------------------------------------------------------------------
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
using NetTopologySuite.Geometries;
namespace NSSDB.Resources
{
    public partial class RegressionRegion
    {
        [Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CitationID { get; set; }

        public int? StatusID { get; set; }
        public int? LocationID { get; set; }

        public virtual Status Status { get; set; }
        public virtual Location Location { get; set; }
        public virtual Citation Citation { get; set; }
        public virtual ICollection<Equation> Equations { get; set; }
        public ICollection<RegionRegressionRegion> RegionRegressionRegions { get; set; }
        public virtual ICollection<Limitation> Limitations { get; set; }
        public virtual ICollection<Coefficient> RegressionRegionCoefficients { get; set; }


    }
}
