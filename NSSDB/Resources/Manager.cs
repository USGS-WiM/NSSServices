//------------------------------------------------------------------------------
//----- Resource ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WiM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Simple Plain Old Class Object (POCO) 
//
//discussion:   POCO's arn't derived from special base classed nor do they return any special types for their properties.
//              
//
//   

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WiM.Utilities;
namespace NSSDB.Resources
{
    public partial class Manager
    {
        public Manager() {
            RegionManagers = new List<RegionManager>();
            Regions = new JoinCollectionFacade<Region, Manager, RegionManager>(this, RegionManagers);            
        }
        [Required]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PrimaryPhone { get; set; }
        [Phone]
        public string SecondaryPhone { get; set; }
        [Required]
        public int RoleID { get; set; }        
        public string OtherInfo { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }

        public Role Role { get; set; }

        private ICollection<RegionManager> RegionManagers { get;}

        [NotMapped]
        public ICollection<Region> Regions { get;}

    }
}
