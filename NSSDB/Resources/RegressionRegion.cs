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
//using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
using WiM.Utilities;

namespace NSSDB.Resources
{
    public partial class RegressionRegion
    {
        public RegressionRegion() {
            RegionRegressionRegions = new List<RegionRegressionRegion>();
            Regions = new JoinCollectionFacade<Region, RegressionRegion, RegionRegressionRegion>(this, RegionRegressionRegions);
        }
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public int CitationID { get; set; }
        //[Required]
        //public PostgisPolygon Location { get; set; }

        public virtual Citation Citation { get; set; }
        public virtual ICollection<Equation> Equations { get; set; }
        private ICollection<RegionRegressionRegion> RegionRegressionRegions { get;}
        public virtual ICollection<Limitation> Limitations { get; set; }
        public virtual ICollection<RegressionRegionCoefficient> RegressionRegionCoefficients { get; set; }
        
        [NotMapped]
        public ICollection<Region> Regions { get;}

    }
}
