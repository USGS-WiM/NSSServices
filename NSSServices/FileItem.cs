using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSSDB.Resources;

namespace NSSServices
{
    public class FileItem
    {
        public IFormFile File { get; set; }
        public RegressionRegion RegressionRegions { get; set; }
    }
}
