using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WIM.Hypermedia;
using WIM.Resources;

namespace SharedDB.Resources
{
    public partial class ErrorType : IHypermedia {[NotMapped] public List<Link> Links { get; set; }}
    public partial class RegressionType : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class StatisticGroupType : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class UnitSystemType : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class UnitType : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class VariableType : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class Manager : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class Region : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
    public partial class RegionManager : IHypermedia {[NotMapped] public List<Link> Links { get; set; } }
}
