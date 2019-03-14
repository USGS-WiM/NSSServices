using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

//https://benjii.me/2018/01/expression-projection-magic-entity-framework-core/

namespace NSSDB.Resources
{
    //public partial class Equation
    //{
    //    public static Expression<Func<Equation, Equation>> Projection
    //    {
    //        get
    //        {
    //            return x => new Equation()
    //            {
    //                ID = x.ID,
    //                RegressionRegionID = x.RegressionRegionID,
    //                PredictionIntervalID = x.PredictionIntervalID,
    //                UnitTypeID = x.UnitTypeID,
    //                Expression = x.Expression,
    //                DA_Exponent = x.DA_Exponent,
    //                OrderIndex = x.OrderIndex,
    //                RegressionTypeID = x.RegressionTypeID,
    //                StatisticGroupTypeID = x.StatisticGroupTypeID,
    //                EquivalentYears = x.EquivalentYears,

    //                Variables = x.Variables != null ? x.Variables.Select(v=>new Variable()
    //                                                                                  {
    //                                                                                    Coefficient = v.Coefficient,
    //                                                                                    CoefficientID = v.CoefficientID,
    //                                                                                    Comments = v.Comments,
    //                                                                                    EquationID = v.EquationID,
    //                                                                                    Equation = v.Equation,
    //                                                                                    ID = v.ID,
    //                                                                                    Limitation = v.Limitation,
    //                                                                                    LimitationID = v.LimitationID,
    //                                                                                    MaxValue = v.MaxValue,
    //                                                                                    MinValue = v.MinValue,
    //                                                                                    RegressionType = v.RegressionType,
    //                                                                                    RegressionTypeID = v.RegressionTypeID,
    //                                                                                    UnitType = v.UnitType,
    //                                                                                    UnitTypeID = v.UnitTypeID,
    //                                                                                    VariableType = v.VariableType,
    //                                                                                    VariableTypeID = v.VariableTypeID,
    //                                                                                    VariableUnitTypes = v.VariableUnitTypes
    //                                                                                   }).ToList():null,
    //                PredictionInterval = x.PredictionInterval,
    //                EquationErrors = x.EquationErrors!=null?x.EquationErrors.Select(er=> new EquationError()
    //                                                                                        {
    //                                                                                            Equation = er.Equation,
    //                                                                                            EquationID = er.EquationID,
    //                                                                                            ErrorType = er.ErrorType != null? new SharedDB.Resources.ErrorType()
    //                                                                                            {
    //                                                                                                 ID = er.ErrorTypeID,
    //                                                                                                  Code = er.ErrorType.Code,
    //                                                                                                   Name = er.ErrorType.Name
    //                                                                                            }:null,
    //                                                                                            ErrorTypeID = er.ErrorTypeID,
    //                                                                                            ID = er.ID, Value = er.Value
    //                                                                                        }).ToList():null,
    //                UnitType = x.UnitType,

    //                EquationUnitTypes = x.EquationUnitTypes,

    //                RegressionRegion = x.RegressionRegion != null ? new RegressionRegion()
    //                {
    //                    ID = x.RegressionRegion.ID,
    //                    Name = x.RegressionRegion.Name,
    //                    Code = x.RegressionRegion.Code,
    //                    Description = x.RegressionRegion.Description,
    //                    CitationID = x.RegressionRegion.CitationID,
    //                    Citation = x.RegressionRegion.Citation,
    //                    Equations = x.RegressionRegion.Equations,
    //                    RegionRegressionRegions = x.RegressionRegion.RegionRegressionRegions,
    //                    Limitations = x.RegressionRegion.Limitations,
    //                    RegressionRegionCoefficients = x.RegressionRegion.RegressionRegionCoefficients
    //                } : null,
    //                StatisticGroupType = x.StatisticGroupType,
    //                RegressionType = x.RegressionType
    //            };
    //        }
    //    }
    //}


    //public partial class RegressionRegion
    //{
    //    public static Expression<Func<RegressionRegion, RegressionRegion>> Projection
    //    {
    //        get
    //        {
    //            return rr => new RegressionRegion()
    //            {
    //                ID = rr.ID,
    //                Name = rr.Name,
    //                Code = rr.Code,
    //                Description = rr.Description,
    //                CitationID = rr.CitationID,
    //                Citation = rr.Citation,
    //                Equations = rr.Equations,
    //                RegionRegressionRegions = rr.RegionRegressionRegions,
    //                Limitations = rr.Limitations,
    //                RegressionRegionCoefficients = rr.RegressionRegionCoefficients
    //            };
    //        }
    //    }
    //    public static RegressionRegion FromEntity(RegressionRegion entity)
    //    {
    //        return Projection.Compile().Invoke(entity);
    //    }

    //}

    //public partial class Variable
    //{
    //    public static Expression<Func<Variable, Variable>> Projection
    //    {
    //        get
    //        {
    //            return rr => new Variable()
    //            {
                    
    //            };
    //        }
    //    }

    //}
}
