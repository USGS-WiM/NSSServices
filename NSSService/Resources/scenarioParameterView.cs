using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSService.Resources
{
    public class ScenarioParameterView
    {
            public int EquationID { get; set; }
            public int RegressionTypeID { get; set; }
            public int StatisticGroupTypeID { get; set; }
            public int RegressionRegionID { get; set; }
            public string RegressionRegionName { get; set; }
            public string RegressionRegionCode { get; set; }
            public string StatisticGroupTypeCode { get; set; }
            public string RegressionTypeCode { get; set; }
            public string StatisticGroupTypeName { get; set; }
            public int VariableID { get; set; }
            public int UnitTypeID { get; set; }
            public String UnitName { get; set; }
            public string UnitAbbr { get; set; }
            public int UnitSystemTypeID { get; set; }
            public Double VariableMaxValue { get; set; }
            public Double VariableMinValue { get; set; }
            public string VariableName { get; set; }
            public string VariableCode { get; set; }
            public string VariableDescription { get; set; }
    }//end class
}//end namespace
