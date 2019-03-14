using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NSSDB.Resources
{
    public class Status
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
