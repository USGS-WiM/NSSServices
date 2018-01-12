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
using System.ComponentModel.DataAnnotations.Schema;
using WiM.Utilities;

namespace NSSDB.Resources
{
    public partial class Region
    {
        public Region() {
            RegionRegressionRegions = new List<RegionRegressionRegion>();
            RegionManagers = new List<RegionManager>();
            RegressionRegions = new JoinCollectionFacade<RegressionRegion, Region, RegionRegressionRegion>(this, RegionRegressionRegions);
            Managers = new JoinCollectionFacade<Manager, Region, RegionManager>(this, RegionManagers);
        }

        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }

        private ICollection<RegionRegressionRegion> RegionRegressionRegions { get;}        
        private ICollection<RegionManager> RegionManagers { get;}


        [NotMapped]
        public ICollection<RegressionRegion> RegressionRegions { get;}
        [NotMapped]
        public ICollection<Manager> Managers { get;}
    }
}
