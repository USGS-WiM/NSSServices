using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GeoAPI.Geometries;

namespace NSSDB.Resources
{
    public class Location
    {
        [Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public IGeometry Geometry { get; set; }
        public string AssociatedCodes { get; set; }
    }
}
