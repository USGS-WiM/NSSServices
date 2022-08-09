using System;
using System.Collections.Generic;

namespace SharedDB.Resources
{
    public partial class GeneralInfomation
    {
        public string State { get; set; }
        public string ProjectAreaGeneralDescription { get; set; }
        public string ExcludedAreas { get; set; }
        public string TypesOfStatisticsAvailable { get; set; }
        public string ArcGisVersionUsedForProcessing { get; set; }
        public bool? TopogridUsed { get; set; }
        public string ProjectionOfDemAndDerivatives { get; set; }
        public string ResolutionOfDemAndDerivatives { get; set; }
        public string DemSourceAndDate { get; set; }
        public string HydrographySource { get; set; }
        public string InwallsUsedSource { get; set; }
        public string GageBasinBoundariesSource { get; set; }
        public string InteractiveSnappingTolerance { get; set; }
        public string BasinCharacteristicsComputedUsingContinuousParameterGrids { get; set; }
        public string SpecialFunctionality { get; set; }
    }
}
