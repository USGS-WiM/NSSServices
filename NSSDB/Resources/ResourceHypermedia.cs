using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WIM.Resources;
using WIM.Hypermedia;

namespace NSSDB.Resources
{
    public partial class Citation : IHypermedia {[NotMapped] public List<WIM.Resources.Link> Links { get; set; }}
    public partial class EquationError : IHypermedia {[NotMapped] public List<WIM.Resources.Link> Links { get; set; } }
    public partial class Limitation : IHypermedia {[NotMapped] public List<WIM.Resources.Link> Links { get; set; } }
    public partial class RegressionRegion : IHypermedia {[NotMapped] public List<WIM.Resources.Link> Links { get; set; } }

}
