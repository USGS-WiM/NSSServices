using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NSSDB.Resources
{
    public class Location
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public Geometry Geometry { get; set; }
    }
}
