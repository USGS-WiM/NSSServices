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
using WIM.Utilities;

namespace SharedDB.Resources
{
    public partial class UnitType
    {
        [Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [Required]
        public int UnitSystemTypeID { get; set; }

        [InverseProperty("UnitTypeIn")]
        public virtual ICollection<UnitConversionFactor> UnitConversionFactorsIn { get; set; }
        [InverseProperty("UnitTypeOut")]
        public virtual ICollection<UnitConversionFactor> UnitConversionFactorsOut { get; set; }

        public virtual UnitSystemType UnitSystemType { get; set; }





    }
}
