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
    
    public partial class PredictionInterval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PredictionInterval()
        {
            this.Equation = new HashSet<Equation>();
        }
    
        public int ID { get; set; }
        public Nullable<double> BiasCorrectionFactor { get; set; }
        public Nullable<double> Student_T_Statistic { get; set; }
        public Nullable<double> Variance { get; set; }
        public string XIRowVector { get; set; }
        public string CovarianceMatrix { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equation> Equation { get; set; }
    }
}
