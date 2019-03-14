using System;
using System.Collections.Generic;
using System.Text;

namespace NSSAgent.Resources
{
    public class RegressionRegion:NSSDB.Resources.RegressionRegion
    {
        public Double? PercentWeight { get; set; }
        public Double? MaskAreaSqMeter { get; set; }
        public Double? AreaSqMeter { get; set; }
    }
}
