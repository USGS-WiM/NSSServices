using System;
using System.ComponentModel.DataAnnotations;
namespace NSSServices.Resources
{
    public class FCTimeSeries
    {
        [Required]
        public string FacilityCode { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public int UnitTypeID { get; set; }
    }
}
