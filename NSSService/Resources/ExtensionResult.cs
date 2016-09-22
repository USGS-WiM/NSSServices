using System;
using System.Collections.Generic;
using WiM.TimeSeries;
using System.Xml.Serialization;

namespace NSSService.Resources
{
    public interface IExtensionResult
    {
        String Description { get; set; }
        String Warnings { get; set; }
    }
    [Serializable]
    public class QPPQResult : IExtensionResult {
        public String Description { get; set; }
        public String Warnings { get; set; }
        public SortedDictionary<Double, Double> ExceedanceProbabilities { get; set; }
        public Station ReferanceGage { get; set; }
        public FlowTimeSeries EstimatedFlow { get; set; }    
    }
}
