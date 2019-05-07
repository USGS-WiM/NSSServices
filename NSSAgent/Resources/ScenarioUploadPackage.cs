using NSSDB.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NSSAgent.Resources
{
    public class ScenarioUploadPackage
    {
        [Required]
        public Int32 RegressionRegionID { get; set; } //x
        [Required]
        public Int32 RegressionTypeID { get; set; }//x
        [Required]
        public string Expression { get; set; }//x
        [Required]
        public Int32 StatisticGroupID { get; set; } //x
        public double? EquivalentYears { get; set; }//x
        public PredictionInterval PredictionInterval { get; set; }//x
        public Int32 UnitID { get; set; }//x
        public Dictionary<Int32, double> Errors { get; set; }//x
        [Required]
        public Dictionary<Int32, Variable> Variables { get; set; }//x
        [Required]
        public ExpectedValue Expected { get; set; }//x


        public struct Variable
        {
            [Required]
            public double MaximumValue { get; set; }//x
            [Required]
            public double MinimumValue { get; set; }//x
            [Required]
            public Int32 UnitID { get; set; }//x
            public string Comments { get; set; }
        }
        public struct ExpectedValue
        {
            [Required]
            public double Value { get; set; }
            [Required]
            public Dictionary<Int32, double> Variables { get; set; }
            public IntervalBounds IntervalBounds { get; set; }
        }

    }

}
