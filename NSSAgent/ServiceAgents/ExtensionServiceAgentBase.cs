//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WIM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   The service agent is responsible for initiating the service call, 
//              capturing the data that's returned and forwarding the data back to 
//              the requestor.
//
//discussion:   delegated hunting and gathering responsibilities.   
//
//    

using NSSAgent.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using WIM.Resources;

namespace NSSAgent.ServiceAgents
{
    public abstract class ExtensionServiceAgentBase
    {
        #region "Properties"       
        public IExtensionResult Result { get; protected set; }
        public bool isInitialized { get; protected set; }
        #endregion
        #region "Collections & Dictionaries"
          #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public ExtensionServiceAgentBase()
        {
            isInitialized = false;
        }
        #endregion
        #endregion
        #region "Methods"
        public virtual Boolean init() {
           throw new  NotImplementedException();
        }
        public virtual Boolean Execute() {
            throw new NotImplementedException();       
        }
        #endregion
        #region "Helper Methods"
        protected virtual Boolean IsValid()
        {
            throw new NotImplementedException();    
        }
        #endregion
    }//end class
    
}//end namespace