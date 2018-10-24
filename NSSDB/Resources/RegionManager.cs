﻿//------------------------------------------------------------------------------
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
//              see https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-4-a-more-general-abstraction/        
//
//   

using System.ComponentModel.DataAnnotations;
using WiM.Utilities;
namespace NSSDB.Resources
{
    public partial class RegionManager:IJoinEntity<Region>, IJoinEntity<Manager>
    {
        [Required]
        public int RegionID { get; set; }
        [Required]
        public int ManagerID { get; set; }

        public Region Region { get; set; }
        public Manager Manager { get; set; }

        Region IJoinEntity<Region>.Navigation { get => Region; set => Region = value; }
        Manager IJoinEntity<Manager>.Navigation { get => Manager; set => Manager = value; }
    }
}