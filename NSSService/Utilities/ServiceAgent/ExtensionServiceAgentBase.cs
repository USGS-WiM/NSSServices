//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

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

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp.Serializers;
using RestSharp;
using WiM.TimeSeries;
using WiM.Utilities.ServiceAgent;
using NSSService.Resources;
using WiM.Utilities;
using WiM.Resources;

namespace NSSService.Utilities.ServiceAgent
{
    public abstract class ExtensionServiceAgentBase:IMessage
    {
        #region "Properties"       
        public IExtensionResult Result { get; protected set; }
        public bool isInitialized { get; protected set; }
        #endregion
        #region "Collections & Dictionaries"
        private List<Message> _message = new List<Message>();
        public List<Message> Messages
        {
            get { return _message.Distinct().ToList(); }
        }
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
        protected void sm(MessageType t, string msg)
        {
            this._message.Add(new Message() { type = t, msg = msg });
        }
        protected void sm(List<Message> msg)
        {
            this._message.AddRange(msg);
        }
        #endregion
    }//end class
    
}//end namespace