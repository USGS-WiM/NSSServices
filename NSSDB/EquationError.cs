//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NSSDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquationError
    {
        public int ID { get; set; }
        public int EquationID { get; set; }
        public int ErrorTypeID { get; set; }
        public double Value { get; set; }
    
        public virtual Equation Equation { get; set; }
        public virtual ErrorType ErrorType { get; set; }
    }
}
