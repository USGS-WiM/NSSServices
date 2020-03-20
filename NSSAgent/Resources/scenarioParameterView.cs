using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSAgent.Resources
{
    public class ScenarioParameterView
    {
        public int EquationID { get; set; }
        public int RegressionTypeID { get; set; }
        public int StatisticGroupTypeID { get; set; }
        public int RegressionRegionID { get; set; }
        public string RegressionTypeCode { get; set; }
        public string RegressionRegionName { get; set; }
        public string RegressionRegionCode { get; set; }
        public string StatisticGroupTypeCode { get; set; }            
        public string StatisticGroupTypeName { get; set; }
        public int VariableID { get; set; }
        public int UnitTypeID { get; set; }
        public string UnitAbbr { get; set; }
        public string UnitName { get; set; }            
        public int UnitSystemTypeID { get; set; }
        public Double VariableMaxValue { get; set; }
        public Double VariableMinValue { get; set; }
        public string VariableName { get; set; }
        public string VariableCode { get; set; }
        public string VariableDescription { get; set; }

        public static ScenarioParameterView FromDataReader(System.Data.IDataReader r)
        {
            int index = 0;
            return new ScenarioParameterView()
            {
                EquationID = r.GetInt32(index++),
                RegressionTypeID = r.GetInt32(index++),
                StatisticGroupTypeID = r.GetInt32(index++),
                RegressionRegionID = r.GetInt32(index++),
                RegressionTypeCode = r.GetString(index++),
                RegressionRegionName = r.GetString(index++),
                RegressionRegionCode = r.GetString(index++),
                StatisticGroupTypeCode = r.GetString(index++),
                StatisticGroupTypeName = r.GetString(index++),
                VariableID = r.GetInt32(index++),
                UnitTypeID = r.GetInt32(index++),
                UnitAbbr = r.GetString(index++),
                UnitName = r.GetString(index++),
                UnitSystemTypeID = r.GetInt32(index++),
                VariableMaxValue = r.GetDouble(index++),
                VariableMinValue = r.GetDouble(index++),
                VariableName = r.GetString(index++),
                VariableCode = r.GetString(index++),
                VariableDescription = r.GetString(index++)
            };
        }
    }//end class
}//end namespace
