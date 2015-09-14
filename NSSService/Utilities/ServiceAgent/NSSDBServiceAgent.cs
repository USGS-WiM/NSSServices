using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WiM.Utilities.ServiceAgent;
using System.Configuration;
using NSSDB;
using WiM.Authentication;
using NSSService.Resources;

namespace NSSService.Utilities.ServiceAgent
{
    public class NSSDBAgent:DBAgentBase
    {
        #region "Events"
        #endregion
        #region "Properties"
        
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public NSSDBAgent(Boolean include= false)
            : this("nssadmin",new EasySecureString("Lj1ulzxcZvmXPNFmI03u"), include)
        {
        }
        public NSSDBAgent(string username, EasySecureString password, Boolean include = false)
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
        public IQueryable<Equation> GetEquations(string region, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> equationTypeIDList = null)
        {
            IQueryable<Equation> equery = null;
            equery = Select<RegionRegressionRegion>()
                       .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                               || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).SelectMany(rr => rr.RegressionRegion.Equations).AsQueryable();

            if (regionEquationList != null && regionEquationList.Count() > 0)
                equery = equery.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString().Trim())
                                            || regionEquationList.Contains(e.RegressionRegion.Code.ToLower().Trim()));

            if (statisticgroupList != null && statisticgroupList.Count() > 0)
                equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                        || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim()));

            if (equationTypeIDList != null && equationTypeIDList.Count() > 0)
                equery = equery.Where(e => equationTypeIDList.Contains(e.EquationTypeID.ToString().Trim())
                                        || equationTypeIDList.Contains(e.EquationType.Code.ToLower().Trim()));

            return equery;
        }
        public IQueryable<Scenario> GetScenarios(string region, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> equationTypeIDList = null)
        {
            IQueryable<ScenarioParameterView> equery = null;
            try
            {
                
                List<RegionRegressionRegion> SelectedRegionRegressions = Select<RegionRegressionRegion>()
                           .Where(rer => String.Equals(region.Trim().ToLower(), rer.Region.Code.ToLower().Trim())
                                   || String.Equals(region.ToLower().Trim(), rer.RegionID.ToString())).ToList();

                if (regionEquationList != null && regionEquationList.Count() > 0)
                    SelectedRegionRegressions = SelectedRegionRegressions.Where(e => regionEquationList.Contains(e.RegressionRegionID.ToString())).ToList();

                equery = getTable<ScenarioParameterView>().Where(s => SelectedRegionRegressions.Select(rr => rr.RegressionRegionID).Contains(s.RegressionRegionID));

                

                if (statisticgroupList != null && statisticgroupList.Count() > 0)
                    equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                            || statisticgroupList.Contains(e.StatisticGroupTypeCode.ToLower().Trim()));

                if (equationTypeIDList != null && equationTypeIDList.Count() > 0)
                    equery = equery.Where(e => equationTypeIDList.Contains(e.EquationTypeID.ToString().Trim())
                                            || equationTypeIDList.Contains(e.EquationTypeCode.ToLower().Trim()));


                return equery.ToList().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { k = key, equ = g })
                    .Select(p => new Scenario()
                    {
                        StatisticGroupType = new SimpleStatisticGroupType()
                        {
                            ID = p.k,
                            Name = p.equ.First().StatisticGroupTypeName,
                            Code = p.equ.First().StatisticGroupTypeCode
                        },
                        RegressionRegions = p.equ.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { k = key, equ = g }).ToList()
                        .Select(j => new SimpleRegionEquation()
                        {
                            ID = j.k,
                            Name = j.equ.First().RegressionRegionName,
                            Parameters = j.equ.Select(e => new Parameter()
                            {
                                Unit = e.UnitAbbr,
                                Limits = new Limit() { Min = e.VariableMinValue, Max = e.VariableMaxValue },
                                Code = e.VariableCode,
                                Description = e.VariableDescription,
                                Name = e.VariableName,
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

        #endregion
        #region "Helper Methods"
        IQueryable<T> getTable<T>() where T : class,new()
        {
            IQueryable <T> equery = null;
            try
            {
                string sql = getSQLStatement(typeof(T).Name);

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
                                    `v`.`UnitTypeID` AS `UnitTypeID`,
                                    `u`.`Abbr` AS `UnitAbbr`,
                                    `v`.`MaxValue` AS `VariableMaxValue`,
                                    `v`.`MinValue` AS `VariableMinValue`,
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