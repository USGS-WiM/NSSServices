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
    
    public partial class Equation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equation()
        {
            this.Variables = new HashSet<Variable>();
            this.EquationUnitTypes = new HashSet<EquationUnitType>();
            this.EquationErrors = new HashSet<EquationError>();
        }
    
        public int ID { get; set; }
        public Nullable<int> PredictionIntervalID { get; set; }
        public int UnitTypeID { get; set; }
        public string Equation1 { get; set; }
        public Nullable<double> DA_Exponent { get; set; }
        public Nullable<int> OrderIndex { get; set; }
        public int EquationTypeID { get; set; }
        public int StatisticGroupTypeID { get; set; }
        public int RegressionRegionID { get; set; }
        public Nullable<double> EquivalentYears { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Variable> Variables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquationUnitType> EquationUnitTypes { get; set; }
        public virtual EquationType EquationType { get; set; }
        public virtual StatisticGroupType StatisticGroupType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquationError> EquationErrors { get; set; }
        public virtual PredictionInterval PredictionInterval { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual RegressionRegion RegressionRegion { get; set; }
    }
}
