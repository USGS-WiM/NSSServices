using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiM.Hypermedia;

namespace NSSDB
{
    public partial class Citation:IHypermedia
    {
        public List<Link> Links{get;set;}        
    }
    public partial class Equation : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class EquationError : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class RegressionType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class RegressionTypeDisplayName : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class EquationUnitType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class ErrorType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class PredictionInterval : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class Region : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class RegionRegressionRegion : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class RegressionRegion : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class StatisticGroupType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class UnitConversionFactor : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class UnitSystemType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class UnitType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class UserType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class Variable : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class VariableType : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
    public partial class VariableUnittype : IHypermedia
    {
        public List<Link> Links { get; set; }
    }
}

