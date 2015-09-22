using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;

using WiM.Utilities;
using WiM.Utilities.ServiceAgent;
using WiM.Authentication;

using NSSDB;

using NSSService.Resources;


namespace NSSService.Utilities.ServiceAgent
{
    internal class NSSAgent: DBAgentBase
    {
        #region "Events"
        #endregion
        #region "Properties"

        #endregion
        #region "Collections & Dictionaries"
        private List<UnitConversionFactor> unitConversionFactors { get; set; }
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        internal NSSAgent(Boolean include = false)
            : this("**username**",new EasySecureString("***REMOVED***"), include)
        {
        }
        internal NSSAgent(string username, EasySecureString password, Boolean include = false)
            : base(ConfigurationManager.ConnectionStrings["nssEntities"].ConnectionString)
        {
            this.context = new nssEntities(string.Format(connectionString, username, password.decryptString()));
            this.context.Configuration.ProxyCreationEnabled = include;
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
        internal IQueryable<Equation> GetEquations(string region, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> equationTypeIDList = null)
        {
            IQueryable<Equation> equery = null;
            equery = Select<RegionRegressionRegion>()
                       .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                               || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).SelectMany(rr => rr.RegressionRegion.Equations).Include(e=>e.EquationType).AsQueryable();

            if (regionEquationList != null && regionEquationList.Count() > 0)
                equery = equery.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString().Trim())
                                            || regionEquationList.Contains(e.RegressionRegion.Code.ToLower().Trim()));

            if (statisticgroupList != null && statisticgroupList.Count() > 0)
                equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                        || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim()));

            if (equationTypeIDList != null && equationTypeIDList.Count() > 0)
                equery = equery.Where(e => equationTypeIDList.Contains(e.EquationTypeID.ToString().Trim())
                                        || equationTypeIDList.Contains(e.EquationType.Code.ToLower().Trim()));

            return equery.OrderBy(e=>e.OrderIndex);
        }
        internal IQueryable<Scenario> GetScenarios(string region, Int32 systemtypeID, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> equationTypeIDList = null)
        {
            IQueryable<ScenarioParameterView> equery = null;
            try
            {
                this.unitConversionFactors = Select<UnitConversionFactor>().ToList();
                
                List<RegionRegressionRegion> SelectedRegionRegressions = Select<RegionRegressionRegion>()
                           .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                                   || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).ToList();

                if (regionEquationList != null && regionEquationList.Count() > 0)
                    SelectedRegionRegressions = SelectedRegionRegressions.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString())).ToList();

                equery = getTable<ScenarioParameterView>(new Object[1]{systemtypeID}).Where(s => SelectedRegionRegressions.Select(rr => rr.RegressionRegionID).Contains(s.RegressionRegionID));

                

                if (statisticgroupList != null && statisticgroupList.Count() > 0)
                    equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                            || statisticgroupList.Contains(e.StatisticGroupTypeCode.ToLower().Trim()));

                if (equationTypeIDList != null && equationTypeIDList.Count() > 0)
                    equery = equery.Where(e => equationTypeIDList.Contains(e.EquationTypeID.ToString().Trim())
                                            || equationTypeIDList.Contains(e.EquationTypeCode.ToLower().Trim()));


                return equery.ToList().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { groupkey = key, groupedparameters = g })
                    .Select(s => new Scenario()
                    { 
                        StatisticGroupID = s.groupkey,
                        StatisticGroupName = s.groupedparameters.First().StatisticGroupTypeName,
                        RegressionRegions = s.groupedparameters.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { groupkey = key, groupedparameters = g }).ToList()
                        .Select(r => new SimpleRegionEquation()
                        {
                            ID = r.groupkey,
                            Name = r.groupedparameters.First().RegressionRegionName,
                            Parameters = r.groupedparameters.Select(p => new Parameter()
                            {  
                               ID = p.VariableID,
                               UnitType = new SimpleUnitType() { ID = p.UnitTypeID, Unit = p.UnitName, Abbr = p.UnitAbbr},
                               Limits = new Limit() { Min = p.VariableMinValue, Max = p.VariableMaxValue },
                               Code = p.VariableCode,
                               Description = p.VariableDescription,
                               Name = p.VariableName,
                               Value = -999.99
                            }).Distinct().ToList()
                        }).ToList()
                    }).AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal IQueryable<Scenario> EstimateScenarios(string region, Int32 systemtypeID, List<Scenario> scenarioList, List<string> regionEquationList, List<string> statisticgroupList, List<string> equationtypeList)
        {
            IQueryable<Equation> equery = null;
            List<Equation> EquationList = null;
            ExpressionOps eOps = null;
           
            try
            {
                this.unitConversionFactors = Select<UnitConversionFactor>().Include("UnitTypeIn.UnitConversionFactorsIn.UnitTypeOut").ToList();
                equery = GetEquations(region, regionEquationList, statisticgroupList, equationtypeList);
                equery = equery.Include("UnitType.UnitConversionFactorsIn.UnitTypeOut").Include("EquationErrors.ErrorType");

                foreach (Scenario scenario in scenarioList)
                {
                    foreach (SimpleRegionEquation regressionregion in scenario.RegressionRegions)
                    {
                        regressionregion.Results = new List<RegressionResultBase>();                       
                     
                        EquationList = equery.Where(e => scenario.StatisticGroupID == e.StatisticGroupTypeID && regressionregion.ID == e.RegressionRegionID).Select(e=>e).ToList();
                        
                        foreach (Equation equation in EquationList)
                        {
                            eOps = new ExpressionOps(equation.Equation1, regressionregion.Parameters.ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, equation.UnitType.UnitSystemTypeID)));
                            
                            if (!eOps.IsValid) break;// next equation

                            var unit = getUnit(equation.UnitType, systemtypeID);
                            regressionregion.Results.Add(new RegressionResult()
                            {
                                Equation = eOps.InfixExpression,
                                Name = equation.EquationType.Name,
                                Description = equation.EquationType.Description,
                                Unit = unit,
                                Errors = equation.EquationErrors.Select(e => new Error() {Name = e.ErrorType.Name, Value = e.Value }).ToList(),
                                Value = eOps.Value*unit.factor                               
                            });

                        }//next equation
                    }//next regressionregion
                }//next scenario
                return scenarioList.AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region "Helper Methods"
        IQueryable<T> getTable<T>(object[] args) where T : class,new()
        {
            try
            {
                string sql = String.Format(getSQLStatement(typeof(T).Name),args);
                return context.Database.SqlQuery<T>(sql).AsQueryable();
            }
            catch (Exception ex)
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
	                                `e`.`EquationTypeID` AS `EquationTypeID`,
	                                `e`.`StatisticGroupTypeID` AS `StatisticGroupTypeID`,
	                                `e`.`RegressionRegionID` AS `RegressionRegionID`,
	                                `et`.`Code` AS `EquationTypeCode`,
	                                `rr`.`Name` AS `RegressionRegionName`,
	                                `rr`.`Code` AS `RegressionRegionCode`,
	                                `st`.`Code` AS `StatisticGroupTypeCode`,
	                                `st`.`Name` AS `StatisticGroupTypeName`,
	                                `v`.`ID` AS `VariableID`,
                                    IF(`u`.`UnitSystemTypeID` != {0} AND `u2`.`UnitSystemTypeID` = {0},`u2`.`ID`,`v`.`UnitTypeID`) AS `UnitTypeID`,
                                    IF(`u`.`UnitSystemTypeID` != {0} AND `u2`.`UnitSystemTypeID` = {0},`u2`.`Abbr`,`u`.`Abbr`) AS `UnitAbbr`,
                                    IF(`u`.`UnitSystemTypeID` != {0} AND `u2`.`UnitSystemTypeID` = {0},`u2`.`Unit`,`u`.`Unit`) AS `UnitName`,
                                    IF(`u`.`UnitSystemTypeID` != {0} AND `u2`.`UnitSystemTypeID` = {0},`v`.`MaxValue`*`cf`.`Factor`,`v`.`MaxValue`) AS `VariableMaxValue`,
                                    IF(`u`.`UnitSystemTypeID` != {0} AND `u2`.`UnitSystemTypeID` = {0},`v`.`MinValue`*`cf`.`Factor`,`v`.`MinValue`) AS `VariableMinValue`,    
	                                `vt`.`Name` AS `VariableName`,
	                                `vt`.`Code` AS `VariableCode`,
	                                `vt`.`Description` AS `VariableDescription`
    
                                FROM
	                                ((((((`nss`.`Equation` `e`
	                                JOIN `nss`.`Variable` `v`)
	                                JOIN `nss`.`UnitType` `u`)
	                                JOIN `nss`.`VariableType` `vt`)
	                                JOIN `nss`.`RegressionRegion` `rr`)
	                                JOIN `nss`.`EquationType` `et`)
	                                JOIN `nss`.`StatisticGroupType` `st`)
                                    Left Join `nss`.`UnitConversionFactor` `cf` ON (`cf`.`UnitTypeInID` = `u`.`ID`)
	                                Left Join `nss`.`UnitType` `u2` on (`cf`.`UnitTypeOutID` = `u2`.`ID`)
    
                                WHERE
	                                ((`v`.`EquationID` = `e`.`ID`)
		                                AND (`u`.`ID` = `v`.`UnitTypeID`)
		                                AND (`st`.`ID` = `e`.`StatisticGroupTypeID`)
		                                AND (`rr`.`ID` = `e`.`RegressionRegionID`)
		                                AND (`et`.`ID` = `e`.`EquationTypeID`)
		                                AND (`vt`.`ID` = `v`.`VariableTypeID`))";
                    
                default:
                    throw new Exception("No sql for table " + type);
            }//end switch;
        
        }

        [DebuggerHidden]
        private SimpleUnitType getUnit(UnitType unitType, int systemtypeID)
        {
            try 
	        {
                if (unitType.UnitSystemTypeID != systemtypeID)
                {
                    return unitType.UnitConversionFactorsIn.Where(u => u.UnitTypeOut.UnitSystemTypeID == systemtypeID)
                        .Select(u => new SimpleUnitType()
                                        {
                                            Abbr = u.UnitTypeOut.Abbr,
                                            Unit = u.UnitTypeOut.Unit,
                                            factor = u.Factor
                                        }).First();

                }//end if
                
                return new SimpleUnitType() { Abbr = unitType.Abbr, Unit = unitType.Unit, factor = 1 };
	        }
	        catch (Exception)
	        {

                return new SimpleUnitType() { Abbr = unitType.Abbr, Unit = unitType.Unit, factor = 1 };
	        }         
        }
        [DebuggerHidden]
        private double getUnitConversionFactor(int inUnitID, int OutUnitSystemTypeID)
        {
            try
            {
                return this.unitConversionFactors.Where(uf => uf.UnitTypeOutID == inUnitID && uf.UnitTypeIn.UnitSystemTypeID == OutUnitSystemTypeID).First().Factor;
            }
            catch (Exception)
            {
                return 1;
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