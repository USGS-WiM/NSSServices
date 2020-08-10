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
using SharedDB.Resources;

namespace NSSDB.Resources
{
    public partial class Variable
    {
        [Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? EquationID { get; set; }
        [Required]
        public int VariableTypeID { get; set; }
        public int? RegressionTypeID { get; set; }
        [Required]
        public int UnitTypeID { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public string Comments { get; set; }
        public int? LimitationID { get; set; }
        public int? CoefficientID { get; set; }

        public virtual Equation Equation { get; set; }
        public virtual VariableType VariableType { get; set; }
        public virtual UnitType UnitType { get; set; }

        public ICollection<VariableUnitType> VariableUnitTypes { get; set; }

        public virtual RegressionType RegressionType { get; set; }
        public virtual Limitation Limitation { get; set; }
        public virtual Coefficient Coefficient { get; set; }
    }
}
