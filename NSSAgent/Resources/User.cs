using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiM.Security.Authentication.Basic;

namespace NSSAgent.Resources
{
    public class User : IBasicUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string password { get; set; }
    }
}
