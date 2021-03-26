using System;
using System.Collections.Generic;
using System.Text;
using WIM.Utilities.Resources;

namespace NSSAgent.Resources
{
    public class NWISResource
    {

        public string baseurl { get; set; }
        public Dictionary<string, string> resources { get; set; }
    }

    public class GageStatsResource
    {

        public string baseurl { get; set; }
        public Dictionary<string, string> resources { get; set; }

    }
}