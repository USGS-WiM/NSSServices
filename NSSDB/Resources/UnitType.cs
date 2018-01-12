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
    public partial class UnitType
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [Required]
        public int UnitSystemTypeID { get; set; }

        public virtual ICollection<Equation> Equations { get; set; }
        public virtual ICollection<Variable> Variables { get; set; }
        public virtual ICollection<UnitConversionFactor> UnitConversionFactorsIn { get; set; }
        public virtual ICollection<UnitConversionFactor> UnitConversionFactorsOut { get; set; }
        public virtual UnitSystemType UnitSystemType { get; set; }
        public ICollection<EquationUnitType> EquationUnitTypes { get; set; }
        public ICollection<VariableUnitType> VariableUnitTypes { get; set; }




    }
}
