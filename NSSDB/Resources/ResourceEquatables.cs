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

namespace NSSDB.Resources
{
    public partial class Citation : IEquatable<Citation>
    {
        public bool Equals(Citation other)
        {
            return String.Equals(this.Title.ToLower(), other.Title.ToLower()) &&
                String.Equals(this.Author.ToLower(), other.Author.ToLower());

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Citation);
        }
        public override int GetHashCode()
        {
            return (this.Title + this.Author).GetHashCode();
        }
    }
    public partial class Coefficient : IEquatable<Coefficient>
    {
        public bool Equals(Coefficient other)
        {
            return String.Equals(Criteria.ToLower(), other.Criteria.ToLower()) &&
                String.Equals(this.Value.ToLower(), other.Value.ToLower())&&
                this.RegressionRegionID == other.RegressionRegionID;

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Coefficient);
        }
        public override int GetHashCode()
        {
            return (this.Criteria + this.Value + this.RegressionRegionID).GetHashCode();
        }

    }
    public partial class Equation : IEquatable<Equation>
    {
        public bool Equals(Equation other)
        {
            return this.RegressionRegionID == other.RegressionRegionID &&
                String.Equals(this.Expression.ToLower(), other.Expression.ToLower()) &&
                this.RegressionTypeID == other.RegressionTypeID &&
                this.StatisticGroupTypeID == other.StatisticGroupTypeID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Equation);
        }
        public override int GetHashCode()
        {
            return (this.RegressionRegionID + this.Expression + this.RegressionRegionID + this.StatisticGroupTypeID).GetHashCode();
        }
    }
    public partial class EquationError : IEquatable<EquationError>
    {
        public bool Equals(EquationError other)
        {
            return this.EquationID==other.EquationID &&
                this.ErrorTypeID == other.ErrorTypeID &&
                this.Value == this.Value;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as EquationError);
        }
        public override int GetHashCode()
        {
            return (this.EquationID + this.Value + this.ErrorTypeID).GetHashCode();
        }
    }
    public partial class EquationUnitType : IEquatable<EquationUnitType>
    {
        public bool Equals(EquationUnitType other)
        {
            return this.EquationID == other.EquationID &&
                this.UnitTypeID == other.UnitTypeID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as EquationUnitType);
        }
        public override int GetHashCode()
        {
            return (this.EquationID + this.UnitTypeID).GetHashCode();
        }
    }
    public partial class Limitation : IEquatable<Limitation>
    {
        public bool Equals(Limitation other)
        {
            return String.Equals(this.Criteria.ToLower(), other.Criteria.ToLower()) &&
                this.RegressionRegionID == other.RegressionRegionID;

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Limitation);
        }
        public override int GetHashCode()
        {
            return (this.Criteria + this.RegressionRegionID).GetHashCode();
        }
    }
    public partial class Manager : IEquatable<Manager>
    {
        public bool Equals(Manager other)
        {
            return String.Equals(this.Username.ToLower(), other.Username.ToLower()) || (String.Equals(this.FirstName.ToLower(), other.FirstName.ToLower()) &&
                String.Equals(this.LastName.ToLower(), other.LastName.ToLower()) &&
                String.Equals(this.Email.ToLower(), other.Email.ToLower()));

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Manager);
        }
        public override int GetHashCode()
        {
            return (this.Username + this.FirstName + this.LastName + Email).GetHashCode();
        }
    }//end 
    public partial class PredictionInterval : IEquatable<PredictionInterval>
    {
        public bool Equals(PredictionInterval other)
        {
            return ((this.BiasCorrectionFactor.HasValue && other.BiasCorrectionFactor.HasValue && this.BiasCorrectionFactor.Value == other.BiasCorrectionFactor.Value) &&
               (this.Student_T_Statistic.HasValue && other.Student_T_Statistic.HasValue && this.Student_T_Statistic.Value == other.Student_T_Statistic.Value) &&
               (this.Variance.HasValue && other.Variance.HasValue && this.Variance.Value == other.Variance.Value) &&
               String.Equals(this.XIRowVector.ToLower(), other.XIRowVector.ToLower()) &&
               String.Equals(this.CovarianceMatrix.ToLower(), other.CovarianceMatrix.ToLower()));


        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as PredictionInterval);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public partial class Region : IEquatable<Region>
    {
        public bool Equals(Region other)
        {
            return string.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
            String.Equals(this.Code.ToLower(), other.Code.ToLower());
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Region);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code).GetHashCode();
        }
    }//end 
    public partial class RegionManager : IEquatable<RegionManager>
    {
        public bool Equals(RegionManager other)
        {
            return this.RegionID == other.RegionID && this.ManagerID == other.ManagerID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as RegionManager);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }//end   
    public partial class RegionRegressionRegion : IEquatable<RegionRegressionRegion>
    {
        public bool Equals(RegionRegressionRegion other)
        {
            return this.RegionID == other.RegionID &&
                this.RegressionRegionID == other.RegressionRegionID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as RegionRegressionRegion);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public partial class RegressionRegion : IEquatable<RegressionRegion>
    {
        public bool Equals(RegressionRegion other)
        {
            return string.Equals(this.Name.ToLower(), other.Name.ToLower()) &&
                   string.Equals(this.Code.ToLower(), other.Code.ToLower()) &&
                   this.CitationID == other.CitationID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as RegressionRegion);
        }
        public override int GetHashCode()
        {
            return (this.Name + this.Code + this.CitationID).GetHashCode();
        }
    }
    public partial class Role : IEquatable<Role>
    {
        public bool Equals(Role other)
        {
            return string.Equals(this.Name.ToLower(), other.Name.ToLower());
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Role);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }//end
    public partial class Variable : IEquatable<Variable>
    {
        public bool Equals(Variable other)
        {
            return this.VariableTypeID == other.VariableTypeID &&
                this.UnitTypeID == other.UnitTypeID &&
                ((this.EquationID.HasValue && other.EquationID.HasValue &&
                this.EquationID.Value == other.EquationID.Value) || 
                (this.LimitationID.HasValue && other.LimitationID.HasValue &&
                this.LimitationID == other.LimitationID) || 
                (this.CoefficientID.HasValue && other.CoefficientID.HasValue &&
                this.CoefficientID.Value == other.CoefficientID.Value));

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Variable);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public partial class VariableUnitType : IEquatable<VariableUnitType>
    {
        public bool Equals(VariableUnitType other)
        {
            return this.VariableID == other.VariableID &&
                this.UnitTypeID == other.UnitTypeID;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as VariableUnitType);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
