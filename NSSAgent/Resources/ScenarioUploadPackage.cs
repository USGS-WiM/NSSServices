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
        public Int32 RegressionRegionID { get; set; }
        [Required]
        public Int32 RegressionTypeID { get; set; }
        [Required]
        public string Expression { get; set; }
        [Required]
        public Int32 StatisticGroupID { get; set; }
        public double? EquivalentYears { get; set; }
        public PredictionInterval PredictionInterval { get; set; }
        public Int32 UnitID { get; set; }
        public Dictionary<Int32, double> Errors { get; set; }
        [Required]
        public Dictionary<Int32, Variable> Variables { get; set; }
        [Required]
        public ExpectedValue Expected { get; set; }


        public struct Variable
        {
            [Required]
            public double MaximumValue { get; set; }
            [Required]
            public double MinimumValue { get; set; }
            [Required]
            public Int32 UnitID { get; set; }
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
