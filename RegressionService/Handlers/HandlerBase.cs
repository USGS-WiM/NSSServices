using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using OpenRasta.Web;
using OpenRasta.Security;

namespace WiM.Handlers
{
    public abstract class HandlerBase
    {
        // will be automatically injected by DI in OpenRasta
        // must be public
        public ICommunicationContext Context { get; set; }

    }//end class
}//end namespace