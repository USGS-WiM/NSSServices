using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WiM.Resources;

using Newtonsoft.Json;

namespace NSSService.Resources
{
    public class Parameter:IEquatable<Parameter>
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Code { get; set; }
        public SimpleUnitType UnitType { get; set; }
        public Double? Value { get; set; }
        public bool ShouldSerializeValue()
        { return Value.HasValue; }
        public Limit Limits { get; set; }
        
        public bool Equals(Parameter other)
        {
            //Check whether the compared object is null.  
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.  
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.  
            return Name.Equals(other.Name);// && Name.Equals(other.Name);
        }
        // If Equals() returns true for a pair of objects   
        // then GetHashCode() must return the same value for these objects.  

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.  
            int hashProductName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Code field.  
            //int hashProductCode = ID.GetHashCode();

            //Calculate the hash code for the product.  
            return hashProductName;// ^ hashProductCode;
        }
    }//end PARAMETER

    public class Limit
    {
        public Double? Max { get; set; }
        public Double? Min { get; set; }
    }
}