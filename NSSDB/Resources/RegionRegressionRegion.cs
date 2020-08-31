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
//              see https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-4-a-more-general-abstraction/ 
//              
//
//   

using System.ComponentModel.DataAnnotations;
using SharedDB.Resources;
namespace NSSDB.Resources
{
    public partial class RegionRegressionRegion
    {
        [Required]
        public int RegionID { get; set; }
        [Required]
        public int RegressionRegionID { get; set; }

        public virtual RegressionRegion RegressionRegion { get; set; }
        public virtual Region Region { get; set; }
    }
}
