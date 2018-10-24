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
//              see https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-4-a-more-general-abstraction/ 
//              
//
//   

using System;
using System.ComponentModel.DataAnnotations;
using SharedDB.Resources;
using WiM.Utilities;

namespace NSSDB.Resources
{
    public partial class VariableUnitType : IJoinEntity<Variable>, IJoinEntity<UnitType>
    {
        [Required]
        public int VariableID { get; set; }
        [Required]
        public int UnitTypeID { get; set; }

        public virtual Variable Variable { get; set; }
        public virtual UnitType UnitType { get; set; }

        Variable IJoinEntity<Variable>.Navigation { get => Variable; set => Variable = value; }
        UnitType IJoinEntity<UnitType>.Navigation { get => UnitType; set => UnitType = value; }
    }
}
