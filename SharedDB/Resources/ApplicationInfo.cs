using System;
using System.Collections.Generic;

namespace SharedDB.Resources
{
    public partial class ApplicationInfo
    {
        public string Application { get; set; }
        public string StatisticLabel { get; set; }
        public string Description { get; set; }
        public string ComputationMethod { get; set; }
        public string DataSource { get; set; }
        public string Report { get; set; }
        public string ReportVar { get; set; }
    }
}
