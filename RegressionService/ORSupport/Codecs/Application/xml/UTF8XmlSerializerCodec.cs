//------------------------------------------------------------------------------
//----- UTF8XmlCodec -----------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2012 WiM - USGS

//    authors:  Jon Baier USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   Create in place of OpenRasta's XmlSerializerCodec which does not properly handle the BOM in UTF8 encoding.
//
//discussion:   A Codec is an enCOder/DECoder for a resources in 
//              this case the resources are POCO classes derived from the EF. 
//              https://github.com/openrasta/openrasta/wiki/Codecs
//
//     

#region Comments
// 06.01.12 - JB - Created from WiMXmlSerializerCodec
#endregion


using System;
using System.Collections;
using System.Xml.Serialization;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;
using System.Linq;
using System.Collections.Generic;

namespace WiM.Codecs
{
    [MediaType("application/xml;q=0.4", ".xml")]
    public class UTF8XmlSerializerCodec : UTF8XmlCodec
    {
        public override object ReadFrom(IHttpEntity request, IType destinationType, string parameterName)
        {
            if (destinationType.StaticType == null)
                throw new InvalidOperationException();
                        
            return new XmlSerializer(destinationType.StaticType).Deserialize(request.Stream);            
        }

        public override void WriteToCore(object obj, IHttpEntity response)
        {
            try
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(Writer, obj);
            }
            catch (Exception e)
            {
                
                throw;
            }
           
        }
        #region Helper Methods

        #endregion
    }
}
