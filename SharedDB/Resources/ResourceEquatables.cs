//------------------------------------------------------------------------------
//----- Equality ---------------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2018 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Overrides Equatable
//
//discussion:   https://blogs.msdn.microsoft.com/ericlippert/2011/02/28/guidelines-and-rules-for-gethashcode/    
//              http://www.aaronstannard.com/overriding-equality-in-dotnet/
//
//              var hashCode = 13;
//              hashCode = (hashCode * 397) ^ MyNum;
//              var myStrHashCode = !string.IsNullOrEmpty(MyStr) ?
//                                      MyStr.GetHashCode() : 0;
//              hashCode = (hashCode * 397) ^ MyStr;
//              hashCode = (hashCode * 397) ^ Time.GetHashCode();
// 
using System;

namespace SharedDB.Resources
{
    public partial class ErrorType : IEquatable<ErrorType>
    {
        public bool Equals(ErrorType other)
        {
            return String.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                String.Equals(this.Code.ToLower(), other.Code.ToLower());

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as ErrorType);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code).GetHashCode();
        }
    }

    public partial class RegressionType : IEquatable<RegressionType>
    {
        public bool Equals(RegressionType other)
        {
            return String.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                String.Equals(this.Code.ToLower(), other.Code.ToLower());

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as RegressionType);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code).GetHashCode();
        }
    }

    public partial class StatisticGroupType : IEquatable<StatisticGroupType>
    {
        public bool Equals(StatisticGroupType other)
        {
            return String.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                String.Equals(this.Code.ToLower(), other.Code.ToLower());

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as StatisticGroupType);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code).GetHashCode();
        }
    }

    public partial class UnitConversionFactor : IEquatable<UnitConversionFactor>
    {
        public bool Equals(UnitConversionFactor other)
        {
            return String.Equals(this.UnitTypeInID, other.UnitTypeInID) &&
                String.Equals(this.UnitTypeOutID, other.UnitTypeOutID);

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as UnitConversionFactor);
        }
        public override int GetHashCode()
        {
            return (this.UnitTypeInID + this.UnitTypeOutID).GetHashCode();
        }
    }

    public partial class UnitType : IEquatable<UnitType>
    {
        public bool Equals(UnitType other)
        {
            return String.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                String.Equals(this.Abbreviation.ToLower(), other.Abbreviation.ToLower()) &&
                this.UnitSystemTypeID == other.UnitSystemTypeID;

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as UnitType);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Abbreviation + UnitSystemTypeID).GetHashCode();
        }
    }

    public partial class VariableType : IEquatable<VariableType>
    {
        public bool Equals(VariableType other)
        {
            return String.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                String.Equals(this.Code.ToLower(), other.Code.ToLower());

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as VariableType);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code).GetHashCode();
        }
    }
}
