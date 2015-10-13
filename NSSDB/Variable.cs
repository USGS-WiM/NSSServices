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
    
    public partial class Variable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Variable()
        {
            this.VariableUnitTypes = new HashSet<VariableUnitType>();
        }
    
        public int ID { get; set; }
        public int EquationID { get; set; }
        public int VariableTypeID { get; set; }
        public int UnitTypeID { get; set; }
        public Nullable<double> MinValue { get; set; }
        public Nullable<double> MaxValue { get; set; }
        public string Comments { get; set; }
        public Nullable<int> RegressionTypeID { get; set; }
    
        public virtual Equation Equation { get; set; }
        public virtual VariableType VariableType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VariableUnitType> VariableUnitTypes { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual RegressionType RegressionType { get; set; }
    }
}
