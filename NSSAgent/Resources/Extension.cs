using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSSAgent.Resources
{
    public class Extension
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Code { get; set; }
        public List<ExtensionParameter> Parameters { get; set; }
        public IExtensionResult Result { get; set; }
    }

    public class ExtensionParameter
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Code { get; set; }
        public string Value { get; set; }
        public bool ShouldSerializeValue()
        { return !String.IsNullOrEmpty(Value); }
    }//end ExtensionParameter
}