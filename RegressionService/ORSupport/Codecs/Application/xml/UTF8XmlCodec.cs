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
//   purpose:   Create in place of OpenRasta's XmlCodec which does not properly handle the BOM in UTF8 encoding.
//
//discussion:   A Codec is an enCOder/DECoder for a resources in 
//              this case the resources are POCO classes derived from the EF. 
//              https://github.com/openrasta/openrasta/wiki/Codecs
//
//     

#region Comments
// 06.01.12 - JB - Created from WiMXmlCodec
#endregion



using System;
using System.Text;
using System.Xml;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace WiM.Codecs
{
    public abstract class UTF8XmlCodec : IMediaTypeReader, IMediaTypeWriter
    {
        public object Configuration { get; set; }
        protected XmlWriter Writer { get; private set; }

        public abstract void WriteToCore(object entity, IHttpEntity response);
        public abstract object ReadFrom(IHttpEntity request, IType destinationType, string memberName);

        public virtual void WriteTo(object entity, IHttpEntity response, string[] parameters)
        {
            var responseStream = response.Stream;
            using (Writer = XmlWriter.Create(responseStream, 
                                             new XmlWriterSettings
                                             {
                                                 ConformanceLevel =
                                                     ConformanceLevel.Document, 
                                                 Indent = true, 
                                                 Encoding = new UTF8Encoding(false),
                                                 NewLineOnAttributes = true, 
                                                 OmitXmlDeclaration = false, 
                                                 CloseOutput = true, 
                                                 CheckCharacters = true
                                             }))
            {
                WriteToCore(entity, response);
            }
        }
    }
}
