//------------------------------------------------------------------------------
//----- JsonDotNetCodec -----------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2012 WiM - USGS

//    authors:  Jon Baier USGS Wisconsin Internet Mapping
//  
//   purpose:   Created a JSON Codec that works with EF. JsonDataContractCodec 
//              does not work because IsReference is set
//
//discussion:   A Codec is an enCOder/DECoder for a resources in 
//              this case the resources are POCO classes derived from the EF. 
//              https://github.com/openrasta/openrasta/wiki/Codecs
//
//     

#region Comments
// 02.03.12 - JB - Created to properly de/serialize JSON
#endregion

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using OpenRasta.TypeSystem;
using OpenRasta.Web;
using OpenRasta.Codecs;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.Reflection;

namespace WiM.Codecs
{
    [MediaType("application/json;q=0.5", "json")]
    public class JsonDotNetCodec : IMediaTypeReader, IMediaTypeWriter
    {
        public object Configuration { get; set; }

        public object ReadFrom(IHttpEntity request, IType destinationType, string paramName)
        {
            if (destinationType.StaticType == null)
                throw new InvalidOperationException();

             // Create a serializer
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader streamReader = new StreamReader(request.Stream, new UTF8Encoding(false, true) ))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                {
                    object ds = serializer.Deserialize(jsonTextReader, destinationType.StaticType);
                    return ds;
                }
            }
        }

        public virtual void WriteTo(object entity, IHttpEntity response, string[] paramneters)
        {
            if (entity == null)
                return;

            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(response.Stream, new UTF8Encoding(false, true))) { CloseOutput = false })
            {
                jsonTextWriter.Formatting = Formatting.Indented;
                //http://blog.greatrexpectations.com/2012/08/30/deserializing-interface-properties-using-json-net/
                //https://www.google.com/search?q=jsonConverter&ie=utf-8&oe=utf-8&aq=t&rls=org.mozilla:en-US:official&client=firefox-a&channel=fflb#channel=fflb&q=json.net%20jsonconverter%20interface&rls=org.mozilla:en-US:official

                // Create a serializer
                JsonSerializer serializer = new JsonSerializer();
                //serializer.PreserveReferencesHandling = PreserveReferencesHandling.none;
                //serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.TypeNameHandling = TypeNameHandling.None;
                serializer.ContractResolver = new ContractResolver();
                serializer.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full;
                
                serializer.Serialize(jsonTextWriter, entity);
                jsonTextWriter.Flush();
            }
                        
        }

    }
}//end class

public class ContractResolver : DefaultContractResolver
{
    //removes the $id reference
    public override JsonContract ResolveContract(Type type)
    {
        var contract = base.ResolveContract(type);
        contract.IsReference = false;
        return contract;
    }

    protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    {
        var serializableMembers = base.GetSerializableMembers(objectType);
        serializableMembers.RemoveAll(memberInfo => (memberInfo.Name.Equals("EntityKey", StringComparison.OrdinalIgnoreCase)));
        return serializableMembers;
    }

    private static bool IsMemberEntityWrapper(MemberInfo memberInfo)
    {
        return memberInfo.Name == "EntityKey";
    }

}

