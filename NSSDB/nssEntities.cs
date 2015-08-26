using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace NSSDB
{
    public partial class nssEntities : DbContext
    {
        public nssEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
}
