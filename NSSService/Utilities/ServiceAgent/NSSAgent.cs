﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Diagnostics;

using WiM.Utilities;
using WiM.Utilities.ServiceAgent;
using WiM.Security;

using NSSDB;

using Newtonsoft.Json;

using NSSService.Resources;


namespace NSSService.Utilities.ServiceAgent
{
    internal class NSSAgent: DBAgentBase
    {
        #region "Events"
        #endregion
        #region "Properties"
        public UserType user { get; private set; }
        #endregion
        #region "Collections & Dictionaries"
        private List<UnitConversionFactor> unitConversionFactors { get; set; }
        private List<Limitation> limitations { get; set; }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        internal NSSAgent(Boolean include = false, Int32 usertypeid = 1)
            : this(ConfigurationManager.AppSettings["dbuser"],new EasySecureString(ConfigurationManager.AppSettings["dbpassword"]), include, usertypeid)
        {        
        }
        internal NSSAgent(string username, EasySecureString password, Boolean include = false, Int32 usertypeid = 1)
            : base(ConfigurationManager.AppSettings["dbconnectionstring"])
        {
            this.context = new nssEntities(string.Format(connectionString, username, password.decryptString()));
            this.context.Configuration.ProxyCreationEnabled = include;
            this.user = Select<UserType>().FirstOrDefault(u => u.ID == usertypeid);
        }
        #endregion
        #region IDisposable Support
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO:Dispose managed resources here.
                    //ie component.Dispose();

                }//EndIF

                // TODO:Call the appropriate methods to clean up
                // unmanaged resources here.
                //ComRelease(Extent);

                // Note disposing has been done.
                disposed = true;


            }//EndIf
        }//End Dispose
        #endregion
        #endregion
        #region "Methods"
        internal IQueryable<Equation> GetEquations(string region, List<string> regionEquationList = null, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null)
        {
            IQueryable<Equation> equery = null;
            equery = Select<RegionRegressionRegion>()
                       .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                               || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).SelectMany(rr => rr.RegressionRegion.Equations).Include(e=>e.RegressionType).AsQueryable();

            if (regionEquationList != null && regionEquationList.Count() > 0)
                equery = equery.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString().Trim())
                                            || regionEquationList.Contains(e.RegressionRegion.Code.ToLower().Trim()));

            if (statisticgroupList != null && statisticgroupList.Count() > 0)
                equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                        || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim()));

            if (regressionTypeIDList != null && regressionTypeIDList.Count() > 0)
                equery = equery.Where(e => regressionTypeIDList.Contains(e.RegressionTypeID.ToString().Trim())
                                        || regressionTypeIDList.Contains(e.RegressionType.Code.ToLower().Trim()));

            return equery.OrderBy(e=>e.OrderIndex);
        }
        internal IQueryable<Scenario> GetScenarios(string region, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0)
        {
            IQueryable<ScenarioParameterView> equery = null;
            this.limitations = new List<Limitation>();
            List<RegressionRegionCoefficient> flowCoefficents = new List<RegressionRegionCoefficient>();
            try
            {
                this.unitConversionFactors = Select<UnitConversionFactor>().Include("UnitTypeIn.UnitConversionFactorsIn.UnitTypeOut").ToList();
                if (this.user.ID == 2)
                {
                    this.limitations = Select<Limitation>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                    flowCoefficents = Select<RegressionRegionCoefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                }//end if

                List<RegionRegressionRegion> SelectedRegionRegressions = Select<RegionRegressionRegion>().Include("RegressionRegion")
                           .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                                   || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).ToList();

                if (regionEquationList != null && regionEquationList.Count() > 0)
                    SelectedRegionRegressions = SelectedRegionRegressions.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString())|| regionEquationList.Contains(e.RegressionRegion.Code.ToLower().Trim())).ToList();

                equery = getTable<ScenarioParameterView>(new Object[0]).Where(s => SelectedRegionRegressions.Select(rr => rr.RegressionRegionID).Contains(s.RegressionRegionID));

                

                if (statisticgroupList != null && statisticgroupList.Count() > 0)
                    equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                            || statisticgroupList.Contains(e.StatisticGroupTypeCode.ToLower().Trim()));

                if (regressionTypeIDList != null && regressionTypeIDList.Count() > 0)
                    equery = equery.Where(e => regressionTypeIDList.Contains(e.RegressionTypeID.ToString().Trim())
                                            || regressionTypeIDList.Contains(e.RegressionTypeCode.ToLower().Trim()));


                return equery.ToList().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { groupkey = key, groupedparameters = g })
                    .Select(s => new Scenario()
                    {                       
                        StatisticGroupID = s.groupkey,
                        StatisticGroupName = s.groupedparameters.First().StatisticGroupTypeName,
                        RegressionRegions = s.groupedparameters.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { groupkey = key, groupedparameters = g }).ToList()
                        .Select(r => new SimpleRegionEquation()
                        { 
                            Extensions = extensionMethodList.Where(ex => canIncludeExension(ex, s.groupkey)).Select(ex => getScenarioExtensionDef(ex, s.groupkey)).ToList(),
                            ID = r.groupkey,
                            Name = r.groupedparameters.First().RegressionRegionName,
                            Code = r.groupedparameters.First().RegressionRegionCode,
                            Parameters = r.groupedparameters.Select(p => new Parameter()
                            {  
                               ID = p.VariableID,
                               UnitType = getUnit( new UnitType() { ID = p.UnitTypeID, Unit = p.UnitName, Abbr = p.UnitAbbr, UnitSystemTypeID = p.UnitSystemTypeID}, systemtypeID >0? systemtypeID: p.UnitSystemTypeID),
                               Limits = new Limit() { Min = p.VariableMinValue*this.getUnitConversionFactor(p.UnitTypeID,systemtypeID > 0 ?systemtypeID:p.UnitSystemTypeID), Max = p.VariableMaxValue*this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID) },
                               Code = p.VariableCode,
                               Description = p.VariableDescription,
                               Name = p.VariableName,
                               Value = -999.99
                            }).Union(limitations.Where(l=>l.RegressionRegionID == r.groupkey).SelectMany(l=>l.Variables).Select(v=>new Parameter() {
                                ID =v.VariableType.ID,
                                UnitType = getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
                                Code = v.VariableType.Code,
                                Description = v.VariableType.Description,
                                Name =v.VariableType.Name,
                                Value =-999.99 })).Union(flowCoefficents.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
                                {
                                    ID = v.VariableType.ID,
                                    UnitType = this.getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
                                    Code = v.VariableType.Code,
                                    Description = v.VariableType.Description,
                                    Name = v.VariableType.Name,
                                    Value = -999.99
                                })).Distinct().ToList()                            
                        }).ToList()
                    }).AsQueryable();
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, "Error getting Scenario: " + ex.Message);
                throw;
            }
        }
        internal IQueryable<Scenario> EstimateScenarios(string region, List<Scenario> scenarioList, List<string> regionEquationList, List<string> statisticgroupList, List<string> regressiontypeList, List<string> extensionMethodList, Int32 systemtypeID = 0)
        {
            IQueryable<Equation> equery = null;
            List<Equation> EquationList = null;
            ExpressionOps eOps = null;
            this.limitations = new List<Limitation>();
            List<RegressionRegionCoefficient> regressionregionCoeff = new List<RegressionRegionCoefficient>();
            try
            {
                this.unitConversionFactors = Select<UnitConversionFactor>().Include("UnitTypeIn.UnitConversionFactorsIn.UnitTypeOut").ToList();
                if (this.user.ID == 2)
                {
                    this.limitations = Select<Limitation>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                    regressionregionCoeff = Select<RegressionRegionCoefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                }//end if
                
                equery = GetEquations(region, regionEquationList, statisticgroupList, regressiontypeList);
                equery = equery.Include("UnitType.UnitConversionFactorsIn.UnitTypeOut").Include("EquationErrors.ErrorType").Include("PredictionInterval").Include("Variables.VariableType").Include("Variables.UnitType");
 
                foreach (Scenario scenario in scenarioList)
                {
                    //remove if invalid
                    scenario.RegressionRegions.RemoveAll(rr => !valid(rr));              

                    foreach (SimpleRegionEquation regressionregion in scenario.RegressionRegions)
                    {   
                        regressionregion.Results = new List<RegressionResultBase>();
                        EquationList = equery.Where(e => scenario.StatisticGroupID == e.StatisticGroupTypeID && regressionregion.ID == e.RegressionRegionID).Select(e=>e).ToList();
 
                        foreach (Equation equation in EquationList)
                        {
                            Boolean paramsOutOfRange = regressionregion.Parameters.Any(x => x.OutOfRange);
                            if (paramsOutOfRange) sm(WiM.Resources.MessageType.warning, "One or more of the parameters is outside the suggested range. Estimates were extrapolated with unknown errors");
                            //equation variables, computed in native units
                            var variables = regressionregion.Parameters.Where(e=>equation.Variables.Any(v=>v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, equation.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code,true)==0).UnitType.UnitSystemTypeID));
                            //var variables = regressionregion.Parameters.ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, equation.UnitType.UnitSystemTypeID));
                            eOps = new ExpressionOps(equation.Equation1,variables);
                            
                            if (!eOps.IsValid) break;// next equation

                            var unit = getUnit(equation.UnitType, systemtypeID>0?systemtypeID:equation.UnitType.UnitSystemTypeID);
                            
                            regressionregion.Results.Add(new RegressionResult()
                            {
                                Equation = eOps.InfixExpression,
                                Name = equation.RegressionType.Name, 
                                code = equation.RegressionType.Code,
                                Description = equation.RegressionType.Description,
                                Unit = unit,
                                Errors = paramsOutOfRange ? null : equation.EquationErrors.Select(e => new Error() { Name = e.ErrorType.Name, Value = e.Value, Code = e.ErrorType.Code }).ToList(), 
                                EquivalentYears = paramsOutOfRange? null: equation.EquivalentYears,
                                IntervalBounds = paramsOutOfRange? null: evaluateUncertainty(equation.PredictionInterval, variables, eOps.Value*unit.factor),
                                Value = eOps.Value*unit.factor                              
                            });
                        }//next equation
                        regressionregion.Extensions.ForEach(ext => evaluateExtension(ext, regressionregion));
                    }//next regressionregion
                    if (canAreaWeight(scenario.RegressionRegions))
                    {
                        var weightedRegion = evaluateWeightedAverage(scenario.RegressionRegions);
                        if (weightedRegion!= null)scenario.RegressionRegions.Add(weightedRegion);
                    }//endif
                    
                    var tz = evaluateTransitionBetweenFlowZones(scenario.RegressionRegions, regressionregionCoeff);
                    if (tz != null) scenario.RegressionRegions.Add(tz);

                }//next scenario

                return scenarioList.AsQueryable();
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, "Error Estimating Scenarios: " + ex.Message);
                throw;
            }
        }
        #endregion
        #region "Helper Methods"
        private IQueryable<T> getTable<T>(object[] args) where T : class,new()
        {
            try
            {
                string sql = String.Format(getSQLStatement(typeof(T).Name),args);
                return context.Database.SqlQuery<T>(sql).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string getSQLStatement(string type) 
        {
            string sql = string.Empty;
            switch (type)
            {
                case "ScenarioParameterView":
                    return @"    SELECT 
	                                `e`.`ID` AS `EquationID`,
	                                `e`.`RegressionTypeID` AS `RegressionTypeID`,
	                                `e`.`StatisticGroupTypeID` AS `StatisticGroupTypeID`,
	                                `e`.`RegressionRegionID` AS `RegressionRegionID`,
	                                `rt`.`Code` AS `RegressionTypeCode`,
	                                `rr`.`Name` AS `RegressionRegionName`,
	                                `rr`.`Code` AS `RegressionRegionCode`,
	                                `st`.`Code` AS `StatisticGroupTypeCode`,
	                                `st`.`Name` AS `StatisticGroupTypeName`,
	                                `v`.`ID` AS `VariableID`,
                                    `v`.`UnitTypeID` AS `UnitTypeID`,
                                    `u`.`Abbr` AS `UnitAbbr`,
                                    `u`.`Unit` AS `UnitName`,
									`u`.`UnitSystemTypeID` AS `UnitSystemTypeID`,
                                    `v`.`MaxValue` AS `VariableMaxValue`,
                                    `v`.`MinValue` AS `VariableMinValue`, 
	                                `vt`.`Name` AS `VariableName`,
	                                `vt`.`Code` AS `VariableCode`,
	                                `vt`.`Description` AS `VariableDescription`
    
                                FROM
	                                ((((((`Equation` `e`
	                                JOIN `Variable` `v`)
	                                JOIN `UnitType` `u`)
	                                JOIN `VariableType` `vt`)
	                                JOIN `RegressionRegion` `rr`)
	                                JOIN `RegressionType` `rt`)
	                                JOIN `StatisticGroupType` `st`)
    
                                WHERE
	                                ((`v`.`EquationID` = `e`.`ID`)
		                                AND (`u`.`ID` = `v`.`UnitTypeID`)
		                                AND (`st`.`ID` = `e`.`StatisticGroupTypeID`)
		                                AND (`rr`.`ID` = `e`.`RegressionRegionID`)
		                                AND (`rt`.`ID` = `e`.`RegressionTypeID`)
		                                AND (`vt`.`ID` = `v`.`VariableTypeID`))";
                    
                default:
                    throw new Exception("No sql for table " + type);
            }//end switch;
        
        }
        private IntervalBounds evaluateUncertainty(PredictionInterval predictionInterval, Dictionary<string, double?> variables, Double Q)
        {
            //Prediction Intervals for the true value of a streamflow statistic obtained for an ungaged site can be 
            //computed by use of a weighted regression equations corrected for bias by:
            //                 1/T(Q/BCF) < Q < T(Q/BCF)
            // Where:   BCF is the bias correction factor for the equation
            //          T = 10^[studentT*Si)
            //          Si = [γ² + xi*U*xi']^0.5
            //              where   γ² is the model error variance
            //                      xi is a row vector of the logarithms of the basin characteristics for site i, augemented by a 1 as the first element
            //                      U is the covariance matrix for site i, 
            //                      xi' is the transposed xi.
            //
            //Tasker, G.D., and Driver, N.E., 1988, Nationwide regression models for predicting urban runoff water 
            //              quality at unmonitored sites: Water Resources Bulletin, v. 24, no. 5, p. 1091–1101.
            //Ries, K.G., and Friesz, P.J., Methods for Estimating Low-Flow Statistics for Massachusetts Streams (pg34) http://pubs.usgs.gov/wri/wri004135/
            //
            double γ2 = -999;
            double studentT = -999;
            double[,] U = null;
            List<double> rowVectorList;
            double Si =-999;
            Double BCF = 1;
            double[,] xi = null;
            double[,] xiprime = null;

            double[,] xiu = null;
            Double xiuxiprime = -999;
            Double T = -999;

            double lowerBound = -999;
            double upperBound = -999;
            try
            {
                if (predictionInterval== null || !predictionInterval.Variance.HasValue || !predictionInterval.Student_T_Statistic.HasValue ||
                    String.IsNullOrEmpty(predictionInterval.CovarianceMatrix) || string.IsNullOrEmpty(predictionInterval.XIRowVector) ||
                    !predictionInterval.BiasCorrectionFactor.HasValue) return null;

                rowVectorList = JsonConvert.DeserializeObject<List<string>>(predictionInterval.XIRowVector)
                    .Select(x => new ExpressionOps(x, variables).Value).ToList();
                //augement Xi with 1 as the first element
                rowVectorList.Insert(0, 1);

                xi = new double[1,rowVectorList.Count()];
                xiprime = new double[rowVectorList.Count(),1];
                for (int i = 0; i < rowVectorList.Count(); i++)
                {
                    xi[0,i] = rowVectorList[i];
                    xiprime[i,0] = rowVectorList[i];
                }//next i

                BCF = predictionInterval.BiasCorrectionFactor.Value;
                γ2 = predictionInterval.Variance.Value;
                studentT = predictionInterval.Student_T_Statistic.Value;
                U = JsonConvert.DeserializeObject<double[,]>(predictionInterval.CovarianceMatrix);
                
                xiu = MathOps.MatrixMultiply(xi, U);
                xiuxiprime = MathOps.MatrixMultiply(xiu, xiprime)[0,0];

                Si = Math.Pow(γ2 + xiuxiprime,0.5);
        
                T = Math.Pow(10, studentT * Si);

                lowerBound = 1 / T * (Q / BCF);
                upperBound = T * (Q / BCF);           
                
                return new IntervalBounds() { Lower = lowerBound, Upper = upperBound };
            }
            catch (Exception)
            {
                return null;
            }//end try
        }
        //[DebuggerHidden]
        private SimpleUnitType getUnit(UnitType inUnitType, int OutSystemtypeID)
        {
            try 
	        {
                if (inUnitType.UnitSystemTypeID != OutSystemtypeID)
                {
                    return inUnitType.UnitConversionFactorsIn.Where(u => u.UnitTypeOut.UnitSystemTypeID == OutSystemtypeID)
                        .Select(u => new SimpleUnitType()
                                        {
                                            ID = u.UnitTypeOut.ID,
                                            Abbr = u.UnitTypeOut.Abbr,
                                            Unit = u.UnitTypeOut.Unit,
                                            factor = u.Factor
                                        }).First();

                }//end if
                
                return new SimpleUnitType() { Abbr = inUnitType.Abbr, Unit = inUnitType.Unit, factor = 1 };
	        }
	        catch (Exception)
	        {
                return new SimpleUnitType() { Abbr = inUnitType.Abbr, Unit = inUnitType.Unit, factor = 1 };
	        }         
        }
        //[DebuggerHidden]
        private double getUnitConversionFactor(int inUnitID, int OutUnitSystemTypeID)
        {
            try
            {
                var tr = this.unitConversionFactors.Where(uf => uf.UnitTypeInID == inUnitID).FirstOrDefault(r => r.UnitTypeOut.UnitSystemTypeID == OutUnitSystemTypeID);
                if (tr != null) return tr.Factor;
                else return 1;
             
            }
            catch (Exception)
            {
                return 1;
            }
        }
        private bool canAreaWeight(List<SimpleRegionEquation> regressionRegions)
        {
            double? areaSum;
            try
            {
                if(regressionRegions.Count() <=1) return false;

                areaSum = regressionRegions.Sum(r => r.PercentWeight);
                if (!areaSum.HasValue || areaSum <= 0) return false;

                if (areaSum.HasValue && Math.Round(areaSum.Value) < 100)
                {
                    sm(WiM.Resources.MessageType.warning, @"Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates. Percentage of area falls outside where region is undefined. Whole estimates have been provided using available regional equations.");
                    return false;
                }
                else if (areaSum.HasValue && Math.Round(areaSum.Value) > 100)
                {
                    //area weight internal components to see if match
                    return regressionRegions.SelectMany(rr => rr.Results.Select(r=> new {weight = rr.PercentWeight, code = r.code }))
                                .GroupBy(k => k.code).All(su => Math.Round(su.Sum(y => y.weight.Value)) == 100);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        internal SimpleRegionEquation evaluateWeightedAverage(List<SimpleRegionEquation> regressionRegions)
        {
            SimpleRegionEquation weightedRR = null;
            try
            {
                
                weightedRR = new SimpleRegionEquation();
                weightedRR.Name = "Area-Averaged";
                var Results = regressionRegions.SelectMany(x => x.Results.Select(r => r.Clone())
                                .Select(r => {
                                    r.Value = r.Value * x.PercentWeight / 100;
                                    r.Errors.ForEach(e => { e.Value = e.Value * x.PercentWeight / 100; });
                                    if (r.IntervalBounds != null) {
                                        r.IntervalBounds.Lower = r.IntervalBounds.Lower *x.PercentWeight.Value / 100;
                                        r.IntervalBounds.Upper = r.IntervalBounds.Upper * x.PercentWeight.Value / 100;
                                    }
                                    return r; }))
                                .OfType<RegressionResult>().ToList();

                weightedRR.Results = this.AccumulateRegressionResults(Results).ToList();                    

                return weightedRR;
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, ex.Message);
                sm(WiM.Resources.MessageType.warning, "Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.");
                return null;
            }
        }
        private IEnumerable<RegressionResultBase> AccumulateRegressionResults(IEnumerable<RegressionResult> regressionresults)
        {
            try
            {
                bool intervalBoundsOK = regressionresults.All(i => i.IntervalBounds != null);
                var ok = regressionresults.All(i => i.Errors.Select(e => e.Code).Except(regressionresults.SelectMany(rr => rr.Errors.Select(er => er.Code)).Distinct()).Count() > 0);

                var Results = regressionresults.GroupBy(e =>
                    e.Name)
                    .Select(i => i.Aggregate((accumulator, it) =>
                    {
                        accumulator.Value += it.Value;
                        accumulator.Unit = it.Unit;
                        accumulator.Equation = "Weighted Average";
                        accumulator.EquivalentYears += it.EquivalentYears;
                        if (intervalBoundsOK)
                        {
                            accumulator.IntervalBounds.Lower += it.IntervalBounds.Lower;
                            accumulator.IntervalBounds.Upper += it.IntervalBounds.Upper;
                        }
                        else accumulator.IntervalBounds = null;
                        if (ok)
                        {
                            accumulator.Errors.ForEach(a => a.Value += it.Errors.FirstOrDefault(ia => ia.Code == a.Code).Value);
                        }
                        else accumulator.Errors = null;
                        return accumulator;
                    }));

                return Results;
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, ex.Message);
                sm(WiM.Resources.MessageType.warning, "Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.");
                return null;
            }
        } 
        internal SimpleRegionEquation evaluateTransitionBetweenFlowZones(List<SimpleRegionEquation> regressionRegions, List<RegressionRegionCoefficient> regressionregionCoeff) {
            SimpleRegionEquation RRTransZone = null;
            ExpressionOps eOps = null;
            Dictionary<int, double> TransitionZoneCoeff = new Dictionary<int, double>();
            try
            {
                var coef = regressionregionCoeff.Where(rr => regressionRegions.Select(r => r.ID).Contains(rr.RegressionRegionID));
                if (coef.Count() <= 0 || coef.Count() != regressionRegions.Count() ) return null;

                foreach (var rRegion in coef)
                {
                    var vars = regressionRegions.FirstOrDefault(e=>e.ID == rRegion.RegressionRegionID).Parameters.Where(e => rRegion.Variables.Any(v => v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, rRegion.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code, true) == 0).UnitType.UnitSystemTypeID));
                    eOps = new ExpressionOps(rRegion.Criteria, vars);
                    if (!eOps.IsValid) throw new Exception();
                    if (!Convert.ToBoolean(eOps.Value)) return null;
                    //use coeff value to compute
                    eOps = new ExpressionOps(rRegion.Value, vars);
                    if (!eOps.IsValid) throw new Exception();
                    TransitionZoneCoeff[rRegion.RegressionRegionID] = eOps.Value;
                }//next regRegion            
                
                

                RRTransZone = new SimpleRegionEquation();
                RRTransZone.Name = "Weighted-Average";
                var Results = regressionRegions.SelectMany(x => x.Results.Select(r => r.Clone())
                    .Select(r => { r.Value = r.Value * TransitionZoneCoeff[x.ID]; return r; }))
                    .OfType<RegressionResult>();

                RRTransZone.Results = this.AccumulateRegressionResults(Results).ToList();

                return RRTransZone;
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, ex.Message);
                sm(WiM.Resources.MessageType.warning, "Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.");
                return null;
            }
        }
        private Extension getScenarioExtensionDef(string ex, int statisticCode)
        {
           switch (ex.ToUpper())  {
               case "QPPQ":
               case "FDCTM":
                   return new Extension()
                   {
                       Code = "QPPQ",
                       Description = "Estimats the flow at an ungaged site given the reference streamgage",
                       Name = "Flow Duration Curve Transfer Method",
                       Parameters = new List<ExtensionParameter>{new ExtensionParameter() { Code = "sid", Name="NWIS Station ID", Description="USGS NWIS Station Identifier", Value="01234567" },
                                                        new ExtensionParameter() { Code = "sdate", Name="Start Date", Description="start date of returned flow estimate", Value=  JsonConvert.SerializeObject(DateTime.MinValue) },
                                                        new ExtensionParameter() { Code = "edate", Name ="End Date", Description="end date of returned flow estimate", Value= JsonConvert.SerializeObject(DateTime.Now) }                                                       
                       }

                   };
           
           }//end switch
           return null;
        }
        private bool canIncludeExension(string ex, int statisticCode)
        {
            switch (ex.ToUpper()){
               case "QPPQ":
               case "FDCTM":
                   if (statisticCode == 5) return true;
                   break;
           }//end switch
           return false;
        }
        private void evaluateExtension(Extension ext, SimpleRegionEquation regressionregion)
        {
            ExtensionServiceAgentBase sa = null;
            try
            {
                switch (ext.Code.ToUpper()) {
                    case "QPPQ":case "FDCTM":
                        sa = new FDCTMServiceAgent(ext, new SortedDictionary<double,double>(regressionregion.Results.ToDictionary(k => Convert.ToDouble(k.Name.Replace("Percent Duration", "").Trim())/100, v => v.Value.Value)));
                        break;
                }//end switch

                if (sa.isInitialized && sa.Execute())
                    ext.Result = sa.Result;
                this.sm(sa.Messages);
            }
            catch (Exception ex)
            {
                this.sm(WiM.Resources.MessageType.error, "Error evaluating extension: "+ex.Message);
                this.sm(sa.Messages);
            }
        }
        private bool valid(SimpleRegionEquation regressionRegion) {
            ExpressionOps eOps = null;
            try
            {
                if (regressionRegion.Parameters.Any(p => p.Value <= -999)) throw new Exception("One or more parameters are invalid");
                //check limitations
                foreach (var item in limitations.Where(l => l.RegressionRegionID == regressionRegion.ID))
                {                
                    var variables = regressionRegion.Parameters.Where(e => item.Variables.Any(v => v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, item.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code, true) == 0).UnitType.UnitSystemTypeID));
                    eOps = new ExpressionOps(item.Criteria, variables);
                    if (!eOps.IsValid) throw new Exception("Validation equation invalid.");
                    if (!Convert.ToBoolean(eOps.Value)) {
                        sm(WiM.Resources.MessageType.info, regressionRegion.Name + " removed; limitation criteria: " + item.Criteria);
                        return false;
                    } 
                }//next item                
                return true;
            }
            catch (Exception ex)
            {
                sm(WiM.Resources.MessageType.error, regressionRegion.Name + " is not valid: " + ex.Message);
                return false;
            }
        }

        #endregion
        #region "Structures"
        //A structure is a value type. When a structure is created, the variable to which the struct is assigned holds
        //the struct's actual data. When the struct is assigned to a new variable, it is copied. The new variable and
        //the original variable therefore contain two separate copies of the same data. Changes made to one copy do not
        //affect the other copy.

        //In general, classes are used to model more complex behavior, or data that is intended to be modified after a
        //class object is created. Structs are best suited for small data structures that contain primarily data that is
        //not intended to be modified after the struct is created.
        #endregion
        #region "Asynchronous Methods"

        #endregion
        #region "Enumerated Constants"
        #endregion
    }//end class
}//end namespace