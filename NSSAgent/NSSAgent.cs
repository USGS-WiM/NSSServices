//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   The service agent is responsible for initiating the service call, 
//              capturing the data that's returned and forwarding the data back to 
//              the requestor.
//
//discussion:   delegated hunting and gathering responsibilities.   
//
// 

using System;
using System.Collections.Generic;
using System.Linq;
using NSSDB;
using NSSDB.Resources;
using WIM.Utilities;
using Microsoft.EntityFrameworkCore;
using NSSAgent.Resources;
using System.Threading.Tasks;
using WIM.Security.Authentication.Basic;
using Newtonsoft.Json;
using NSSAgent.ServiceAgents;
using SharedDB.Resources;
using WIM.Resources;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.Common;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using GeoAPI.Geometries;

namespace NSSAgent
{
    public interface INSSAgent: IBasicUserAgent
    {
        //Citations
        Task<Citation> GetCitation(Int32 ID);
        IQueryable<Citation> GetCitations(List<string> regionList = null, IGeometry geom = null, List<string> regressionRegionList = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null);
        IQueryable<Citation> GetManagerCitations(int managerID);
        Task<Citation> Update(Int32 pkId, Citation item);        

        //Limitations
        IQueryable<Limitation> GetRegressionRegionLimitations(Int32 RegressionRegionID);
        Task<IEnumerable<Limitation>> AddRegressionRegionLimitations(Int32 RegressionRegionID, List<Limitation> items);
        IEnumerable<Limitation> RemoveRegressionRegionLimitations(Int32 RegressionRegionID, List<Limitation> items);
        Task<Limitation> Update(Int32 pkId, Limitation item);
        Task DeleteLimitation(Int32 pkID);

        //Managers
        IQueryable<Manager> GetManagers();
        Task<Manager> GetManager(Int32 ID);
        Task<Manager> Add(Manager item);
        Task<IEnumerable<Manager>> Add(List<Manager> items);
        Task<Manager> Update(Int32 pkId, Manager item);
        Task DeleteManager(Int32 pkID);

        //Regions
        IQueryable<Region> GetRegions();
        Task<Region> GetRegion(Int32 ID);
        Region GetRegionByIDOrCode(string identifier);
        IQueryable<Region> GetManagerRegions(int managerID);
        Task<Region> Add(Region item);
        Task<IEnumerable<Region>> Add(List<Region> items);
        Task<Region> Update(Int32 pkId, Region item);
        Task DeleteRegion(Int32 pkID);

        //RegressionRegions
        IQueryable<NSSDB.Resources.RegressionRegion> GetRegressionRegions(List<string> regionList = null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null);
        Task<NSSDB.Resources.RegressionRegion> GetRegressionRegion(Int32 ID);
        IQueryable<NSSDB.Resources.RegressionRegion> GetManagerRegressionRegions(int managerID);
        Task<RegionRegressionRegion> Add(RegionRegressionRegion item);
        Task<IEnumerable<RegionRegressionRegion>> Add(List<RegionRegressionRegion> items);
        Task<NSSDB.Resources.RegressionRegion> Update(Int32 pkId, NSSDB.Resources.RegressionRegion item);
        Task DeleteRegressionRegion(Int32 pkID);

        //Coefficents
        IQueryable<Coefficient> GetRegressionRegionCoefficients(Int32 RegressionRegionID);
        Task<IEnumerable<Coefficient>> AddRegressionRegionCoefficients(Int32 RegressionRegionID, List<Coefficient> items);
        IEnumerable<Coefficient> RemoveRegressionRegionCoefficients(int RegressionRegionID, List<Coefficient> items);
        Task<Coefficient> Update(Int32 pkId, Coefficient item);
        Task DeleteCoefficient(Int32 pkID);

        //Roles
        IQueryable<Role> GetRoles();
        Task<Role> GetRole(Int32 ID);
        Task<Role> Add(Role item);
        Task<IEnumerable<Role>> Add(List<Role> items);
        Task<Role> Update(Int32 pkId, Role item);
        Task DeleteRole(Int32 pkID);
        
        //Scenarios
        IQueryable<Scenario> GetScenarios(List<string> regionList, List<string> regressionRegionList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0);
        IQueryable<Scenario> GetScenarios(List<string> regionList=null, IGeometry geom=null, List<string> regressionRegionList = null, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0);
        IQueryable<Scenario> EstimateScenarios(List<string> regionList, List<Scenario> scenarioList, List<string> regionEquationList, List<string> statisticgroupList, List<string> regressiontypeList, List<string> extensionMethodList, Int32 systemtypeID = 0);
        
        //Readonly (Shared Views) methods
        IQueryable<ErrorType> GetErrors();
        Task<ErrorType> GetError(Int32 ID);
        IQueryable<RegressionType> GetRegressions();
        IQueryable<RegressionType> GetRegressions(List<String> regionList, List<String> regressionRegionList, List<String> statisticgroupList);
        Task<RegressionType> GetRegression(Int32 ID);
        IQueryable<StatisticGroupType> GetStatisticGroups();
        IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList, List<String> regressionRegionList, List<String> regressionsList);
        IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList, IGeometry geom, List<String> regressionsList);
        Task<StatisticGroupType> GetStatisticGroup(Int32 ID);
        IQueryable<UnitType> GetUnits();
        Task<UnitType> GetUnit(Int32 ID);
        IQueryable<UnitSystemType> GetUnitSystems();
        Task<UnitSystemType> GetUnitSystem(Int32 ID);
        IQueryable<VariableType> GetVariables();
        Task<VariableType> GetVariable(Int32 ID);
    }
    public class NSSServiceAgent : DBAgentBase, INSSAgent
    {
        #region "Properties"
        private readonly IDictionary<Object,Object> _messages;
        #endregion
        #region "Collections & Dictionaries"
        private List<UnitConversionFactor> unitConversionFactors { get; set; }
        private List<Limitation> limitations { get; set; }
        #endregion
        #region Constructors
        public NSSServiceAgent(NSSDBContext context, IHttpContextAccessor httpContextAccessor) : base(context) {
            _messages = httpContextAccessor.HttpContext.Items;
            //optimize query for disconnected databases.
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            this.unitConversionFactors = Select<UnitConversionFactor>().Include("UnitTypeIn.UnitConversionFactorsIn.UnitTypeOut").ToList();
            this.limitations = Select<Limitation>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
        }
        #endregion
        #region Methods
        #region Citations
        public Task<Citation> GetCitation(int ID)
        {
            return this.Find<Citation>(ID);
        }
        public IQueryable<Citation> GetCitations(List<String> regionList=null, IGeometry geom = null, List<String> regressionRegionList = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null)
        {
            if (!regionList.Any() && geom == null && !regressionRegionList.Any()&& !statisticgroupList.Any() && !regressiontypeList.Any())
                return this.Select<Citation>();

            if (geom != null) //TODO: will need more work, depending on usecase. ie, if want to limit geometry by regreagionlist over (currently) overwrite.
                regressionRegionList = getRegressionRegionsByGeometry(geom).Select(rr => rr.ID.ToString()).ToList();
   
            return this.GetEquations(regionList, regressionRegionList, statisticgroupList, regressiontypeList).Select(e => e.RegressionRegion.Citation).Distinct().OrderBy(e => e.ID);
        }
        public IQueryable<Citation> GetManagerCitations(int managerID)
        {
            return this.getTable<Citation>(sqltypeenum.managerCitations, new Object[managerID]);
        }
        public Task<Citation> Update(int pkId, Citation item)
        {
            return this.Update<Citation>(pkId, item);
        }
        #endregion
        #region Limitations
        public IQueryable<Limitation> GetRegressionRegionLimitations(Int32 RegressionRegionID)
        {
            return this.Select<Limitation>().Where(l => l.RegressionRegionID == RegressionRegionID);
        }
        public Task<IEnumerable<Limitation>> AddRegressionRegionLimitations(Int32 RegressionRegionID,List<Limitation> items)
        {
            //ensure limitations are assigned to region
            items.ForEach(i => i.RegressionRegionID = RegressionRegionID);
            return this.Add<Limitation>(items);
        }
        public IEnumerable<Limitation> RemoveRegressionRegionLimitations(int RegressionRegionID, List<Limitation> items)
        {
            //find limitations
            this.Select<Limitation>()
                .Where(l => l.RegressionRegionID == RegressionRegionID)
                .Where(l => items.Contains(l)).ToList().ForEach(item=> this.Delete(item));

            return this.Select<Limitation>().Where(l => l.RegressionRegionID == RegressionRegionID).AsEnumerable();
        }
        public Task<Limitation> Update(Int32 pkId, Limitation item) {
            return this.Update<Limitation>(pkId, item);
        }
        public Task DeleteLimitation(Int32 pkID) {
            return this.Delete<Limitation>(pkID);
        }
        #endregion
        #region Manager
        public IBasicUser GetUserByUsername(string username)
        {
            try
            {

                return Select<Manager>().Include(p => p.Role).Where(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase))
                    .Select(u => new User()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Role = u.Role.Name,
                        RoleID = u.RoleID,
                        ID = u.ID,
                        Username = u.Username,
                        Salt = u.Salt,
                        password = u.Password
                    }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }


        }
        public IQueryable<Manager> GetManagers()
        {
            return this.Select<Manager>();
        }
        public Task<Manager> GetManager(int ID)
        {
            return this.Find<Manager>(ID);
        }       
        public Task<Manager> Add(Manager item)
        {
            return this.Add<Manager>(item);
        }
        public Task<IEnumerable<Manager>> Add(List<Manager> items)
        {
            return this.Add<Manager>(items);
        }
        public Task<Manager> Update(int pkId, Manager item)
        {
            return this.Update<Manager>(pkId, item);
        }
        public Task DeleteManager(int pkID)
        {
            return this.Delete<Manager>(pkID);
        }
        #endregion        
        #region Region
        public Region GetRegionByIDOrCode(string identifier)
        {
            try
            {

                return  Select<Region>().FirstOrDefault(e => String.Equals(e.ID.ToString().Trim().ToLower(),
                                                         identifier.Trim().ToLower()) || String.Equals(e.Code.Trim().ToLower(),
                                                         identifier.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                sm("Error finding region " + ex.Message, WIM.Resources.MessageType.error);
                return null;
            }


        }
        public IQueryable<Region> GetRegions()
        {
            return this.Select<Region>();
        }
        public Task<Region> GetRegion(int ID)
        {
            return this.Find<Region>(ID);
        }
        public IQueryable<Region> GetManagerRegions(int managerID)
        {
            return Select<RegionManager>().Where(rm => rm.ManagerID == managerID)
                                .Include("Region").Select(rm => rm.Region);
        }
        public Task<Region> Add(Region item)
        {
            return this.Add<Region>(item);
        }
        public Task<IEnumerable<Region>> Add(List<Region> items)
        {
            return this.Add<Region>(items);
        }
        public Task<Region> Update(int pkId, Region item)
        {
            return this.Update<Region>(pkId, item);
        }
        public Task DeleteRegion(int pkID)
        {
            return this.Delete<Region>(pkID);
        }
        #endregion
        #region RegressionRegion
        public NSSDB.Resources.RegressionRegion GetRegressionRegionByIDOrCode(string identifier)
        {
            try
            {
                return Select<NSSDB.Resources.RegressionRegion>().FirstOrDefault(e => String.Equals(e.ID.ToString().Trim().ToLower(),
                                                        identifier.Trim().ToLower()) || String.Equals(e.Code.Trim().ToLower(),
                                                        identifier.Trim().ToLower()));
            }
            catch (Exception ex)
            {
                sm("Error finding region " + ex.Message, WIM.Resources.MessageType.error);
                return null;
            }
        }
        public IQueryable<NSSDB.Resources.RegressionRegion> GetRegressionRegions(List<string> regionList=null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null)
        {
            Dictionary<Int32, Resources.RegressionRegion> regressionRegionList = null;
            if (!regionList.Any() && geom== null&& !statisticgroupList.Any() && !regressiontypeList.Any())
                return this.Select<NSSDB.Resources.RegressionRegion>();

            if (geom != null) {
                var ProjGeom = geom.ProjectTo(102008);
                regressionRegionList = getRegressionRegionsByGeometry(ProjGeom)
                    .Select(r => new Resources.RegressionRegion()
                    { ID=r.ID,Code = r.Code, AreaSqMeter = r.Location.Geometry.Intersection(ProjGeom).Area, MaskAreaSqMeter = ProjGeom.Area }).ToList()
                    .Where(p => Math.Round(p.AreaSqMeter.Value / p.MaskAreaSqMeter.Value * 100, 2, MidpointRounding.AwayFromZero) > 1).ToDictionary(k=>k.ID);
            }
            
            return this.GetEquations(regionList, regressionRegionList?.Keys.Select(k=>k.ToString()).ToList(), statisticgroupList, regressiontypeList).Select(e => e.RegressionRegion).Distinct()
                .Select(rr=> new Resources.RegressionRegion() {
                    ID = rr.ID,
                    Name = rr.Name,
                    Code = rr.Code,
                    CitationID = rr.CitationID,
                    Description = rr.Description,
                    AreaSqMeter = regressionRegionList != null ? regressionRegionList[rr.ID].AreaSqMeter:null,
                    PercentWeight =regressionRegionList!= null? (double?)Math.Round(regressionRegionList[rr.ID].AreaSqMeter.Value / regressionRegionList[rr.ID].MaskAreaSqMeter.Value * 100, 2, MidpointRounding.AwayFromZero):null
                }).OrderBy(e => e.ID);
        }
        public Task<NSSDB.Resources.RegressionRegion> GetRegressionRegion(int ID)
        {
            return this.Find<NSSDB.Resources.RegressionRegion>(ID);
        }
        public IQueryable<NSSDB.Resources.RegressionRegion> GetManagerRegressionRegions(int managerID)
        {
            return Select<RegionManager>().Where(rm => rm.ManagerID == managerID)
                                .Include("Region.RegionRegressionRegions.RegressionRegion")
                                .SelectMany(e => e.Region.RegionRegressionRegions.Select(s => s.RegressionRegion));
        }
        public Task<RegionRegressionRegion> Add(RegionRegressionRegion item)
        {
            return this.Add<RegionRegressionRegion>(item);
        }
        public Task<IEnumerable<RegionRegressionRegion>> Add(List<RegionRegressionRegion> items)
        {
            return this.Add<RegionRegressionRegion>(items);
        }
        public Task<NSSDB.Resources.RegressionRegion> Update(int pkId, NSSDB.Resources.RegressionRegion item)
        {
            return this.Update<NSSDB.Resources.RegressionRegion>(pkId, item);
        }
        public Task DeleteRegressionRegion(int pkID)
        {
            return this.Delete<NSSDB.Resources.RegressionRegion>(pkID);
        }
        #endregion
        #region Coefficient
        public IQueryable<Coefficient> GetRegressionRegionCoefficients(Int32 RegressionRegionID)
        {
            return this.Select<Coefficient>().Where(l => l.RegressionRegionID == RegressionRegionID);
        }
        public Task<IEnumerable<Coefficient>> AddRegressionRegionCoefficients(Int32 RegressionRegionID, List<Coefficient> items)
        {
            //ensure Coefficients are assigned to region
            items.ForEach(i => i.RegressionRegionID = RegressionRegionID);
            return this.Add<Coefficient>(items);
        }
        public IEnumerable<Coefficient> RemoveRegressionRegionCoefficients(int RegressionRegionID, List<Coefficient> items)
        {
            //find Coefficients
            this.Select<Coefficient>()
                .Where(l => l.RegressionRegionID == RegressionRegionID)
                .Where(l => items.Contains(l)).ToList().ForEach(item => this.Delete(item));

            return this.Select<Coefficient>().Where(l => l.RegressionRegionID == RegressionRegionID).AsEnumerable();
        }
        public Task<Coefficient> Update(Int32 pkId, Coefficient item)
        {
            return this.Update<Coefficient>(pkId, item);
        }
        public Task DeleteCoefficient(Int32 pkID)
        {
            return this.Delete<Coefficient>(pkID);
        }
        #endregion
        #region Roles
        public IQueryable<Role> GetRoles()
        {
            return this.Select<Role>();
        }
        public Task<Role> GetRole(int ID)
        {
            return this.Find<Role>(ID);
        }
        public Task<Role> Add(Role item)
        {
            return this.Add<Role>(item);
        }
        public Task<IEnumerable<Role>> Add(List<Role> items)
        {
            return this.Add<Role>(items);
        }
        public Task<Role> Update(int pkId, Role item)
        {
            return this.Update<Role>(pkId, item);
        }
        public Task DeleteRole(int pkID)
        {
            return this.Delete<Role>(pkID);
        }
        #endregion        
        #region Scenarios
        public IQueryable<Scenario> GetScenarios(List<string> regionList, List<string> regressionRegionList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0)
        {
            IQueryable<ScenarioParameterView> equery = null;
            List<Coefficient> flowCoefficents = new List<Coefficient>();
            try
            {                             
                
                flowCoefficents = Select<Coefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                

                List<RegionRegressionRegion> SelectedRegionRegressions = Select<RegionRegressionRegion>().Include("RegressionRegion")
                           .Where(rer => regionList.Contains(rer.Region.Code.ToLower().Trim())
                           || regionList.Contains(rer.RegionID.ToString())).ToList();             


                if (regressionRegionList != null && regressionRegionList.Count() > 0)
                    SelectedRegionRegressions = SelectedRegionRegressions.Where(e => regressionRegionList.Contains(e.RegressionRegionID.ToString()) || regressionRegionList.Contains(e.RegressionRegion.Code.ToLower().Trim())).ToList();

                equery = GetScenarioParameterView().Where(s => SelectedRegionRegressions.Select(rr => rr.RegressionRegionID).Contains(s.RegressionRegionID));
                

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
                        .Select(r => new SimpleRegressionRegion()
                        {
                            Extensions = extensionMethodList.Where(ex => canIncludeExension(ex, s.groupkey)).Select(ex => getScenarioExtensionDef(ex, s.groupkey)).ToList(),
                            ID = r.groupkey,
                            Name = r.groupedparameters.First().RegressionRegionName,
                            Code = r.groupedparameters.First().RegressionRegionCode,
                            Parameters = r.groupedparameters.Select(p => new Parameter()
                            {
                                ID = p.VariableID,
                                UnitType = getUnit(new UnitType() { ID = p.UnitTypeID, Name = p.UnitName, Abbreviation = p.UnitAbbr, UnitSystemTypeID = p.UnitSystemTypeID }, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID),
                                Limits = new Limit() { Min = p.VariableMinValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID), Max = p.VariableMaxValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID) },
                                Code = p.VariableCode,
                                Description = p.VariableDescription,
                                Name = p.VariableName,
                                Value = -999.99
                            }).Union(limitations.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
                            {
                                ID = v.VariableType.ID,
                                UnitType = getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
                                Code = v.VariableType.Code,
                                Description = v.VariableType.Description,
                                Name = v.VariableType.Name,
                                Value = -999.99
                            })).Union(flowCoefficents.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
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
                sm("Error getting Scenario: " + ex.Message,WIM.Resources.MessageType.error);
                throw;
            }
        }
        public IQueryable<Scenario> GetScenarios(List<string> regionList=null, IGeometry geom=null, List<string> regressionRegionList = null, List<string> statisticgroupList = null, List<string> regressiontypeList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0)
        {
            Dictionary<Int32, Resources.RegressionRegion> regressionRegions = null;
            List<Coefficient> flowCoefficents = new List<Coefficient>();
            try
            {
                flowCoefficents = Select<Coefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();

                if (geom != null)
                {
                    var ProjGeom = geom.ProjectTo(102008);
                    regressionRegions = getRegressionRegionsByGeometry(ProjGeom)
                        .Select(r => new Resources.RegressionRegion()
                        { ID = r.ID, Code = r.Code, AreaSqMeter = r.Location.Geometry.Intersection(ProjGeom).Area, MaskAreaSqMeter = ProjGeom.Area }).ToList()
                        .Where(p => Math.Round(p.AreaSqMeter.Value / p.MaskAreaSqMeter.Value * 100, 2, MidpointRounding.AwayFromZero) > 1).ToDictionary(k => k.ID);
                }

                var equ =  this.GetEquations(regionList, regressionRegionList, statisticgroupList,regressiontypeList)
                    .Include("Variables.VariableType").Include("Variables.UnitType").Include(e=>e.StatisticGroupType).Include(e=>e.RegressionRegion);

                return equ.ToList().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { groupkey = key, groupedparameters = g })
                    .Select(s => new Scenario()
                    {
                        StatisticGroupID = s.groupkey,
                        StatisticGroupName = s.groupedparameters.First().StatisticGroupType.Name,
                        RegressionRegions = s.groupedparameters.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { groupkey = key, groupedparameters = g }).ToList()
                        .Select(r => new SimpleRegressionRegion()
                        {
                            Extensions = extensionMethodList.Where(ex => canIncludeExension(ex, s.groupkey)).Select(ex => getScenarioExtensionDef(ex, s.groupkey)).ToList(),
                            ID = r.groupkey,
                            Name = r.groupedparameters.First().RegressionRegion.Name,
                            Code = r.groupedparameters.First().RegressionRegion.Code,
                            PercentWeight = (regressionRegions!=null && regressionRegions.ContainsKey(r.groupkey))?regressionRegions[r.groupkey].PercentWeight:null,
                            Parameters = r.groupedparameters.SelectMany(gp=>gp.Variables).Select(p => new Parameter()
                            {
                                ID = p.ID,
                                UnitType = getUnit(new UnitType() { ID = p.UnitTypeID, Name = p.UnitType.Name, Abbreviation = p.UnitType.Abbreviation, UnitSystemTypeID = p.UnitType.UnitSystemTypeID }, systemtypeID > 0 ? systemtypeID : p.UnitType.UnitSystemTypeID),
                                Limits = new Limit() { Min = p.MinValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitType.UnitSystemTypeID), Max = p.MaxValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitType.UnitSystemTypeID) },
                                Code = p.VariableType.Code,
                                Description = p.VariableType.Description,
                                Name = p.VariableType.Name,
                                Value = -999.99
                            }).Union(limitations.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
                            {
                                ID = v.VariableType.ID,
                                UnitType = getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
                                Code = v.VariableType.Code,
                                Description = v.VariableType.Description,
                                Name = v.VariableType.Name,
                                Value = -999.99
                            })).Union(flowCoefficents.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
                            {
                                ID = v.VariableType.ID,
                                UnitType = this.getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
                                Code = v.VariableType.Code,
                                Description = v.VariableType.Description,
                                Name = v.VariableType.Name,
                                Value = -999.99
                            })).Distinct().ToList()
                        }).ToList()
                    }).ToList().AsQueryable();
            }
            catch (Exception ex)
            {
                sm($"Error getting scenarios, more info: {ex.Message};", MessageType.error);
                throw;
            }
        }
        public IQueryable<Scenario> EstimateScenarios(List<string> regionList, List<Scenario> scenarioList, List<string> regressionRegionList, List<string> statisticgroupList, List<string> regressiontypeList, List<string> extensionMethodList, Int32 systemtypeID = 0)
        {
            IQueryable<Equation> equery = null;
            List<Equation> EquationList = null;
            ExpressionOps eOps = null;
            List<Coefficient> regressionregionCoeff = new List<Coefficient>();
            try
            {
                regressionregionCoeff = Select<Coefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                

                equery = GetEquations(regionList, regressionRegionList, statisticgroupList, regressiontypeList);
                equery = equery.Include("UnitType.UnitConversionFactorsIn.UnitTypeOut").Include("EquationErrors.ErrorType").Include("PredictionInterval").Include("Variables.VariableType").Include("Variables.UnitType");

                foreach (Scenario scenario in scenarioList)
                {
                    //remove if invalid
                    scenario.RegressionRegions.RemoveAll(rr => !valid(rr, scenario.RegressionRegions.Max(r => r.PercentWeight)));

                    foreach (SimpleRegressionRegion regressionregion in scenario.RegressionRegions)
                    {
                        regressionregion.Results = new List<RegressionResultBase>();
                        EquationList = equery.Where(e => scenario.StatisticGroupID == e.StatisticGroupTypeID && regressionregion.ID == e.RegressionRegionID).ToList();                            

                        Boolean paramsOutOfRange = regressionregion.Parameters.Any(x => x.OutOfRange);
                        if (paramsOutOfRange)
                        {
                            var outofRangemsg = "One or more of the parameters is outside the suggested range. Estimates were extrapolated with unknown errors";
                            regressionregion.Disclaimer = outofRangemsg;
                            sm(outofRangemsg, WIM.Resources.MessageType.warning);
                        }//end if

                        foreach (Equation equation in EquationList)
                        {
                            //equation variables, computed in native units
                            var variables = regressionregion.Parameters.Where(e => equation.Variables.Any(v => v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, equation.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code, true) == 0).UnitType.UnitSystemTypeID));
                            //var variables = regressionregion.Parameters.ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, equation.UnitType.UnitSystemTypeID));
                            eOps = new ExpressionOps(equation.Expression, variables);

                            if (!eOps.IsValid) break;// next equation

                            var unit = getUnit(equation.UnitType, systemtypeID > 0 ? systemtypeID : equation.UnitType.UnitSystemTypeID);

                            regressionregion.Results.Add(new RegressionResult()
                            {
                                Equation = eOps.InfixExpression,
                                Name = equation.RegressionType.Name,
                                code = equation.RegressionType.Code,
                                Description = equation.RegressionType.Description,
                                Unit = unit,
                                Errors = paramsOutOfRange ? new List<Error>() : equation.EquationErrors.Select(e => new Error() { Name = e.ErrorType?.Name, Value = e.Value, Code = e.ErrorType?.Code }).ToList(),
                                EquivalentYears = paramsOutOfRange ? null : equation.EquivalentYears,
                                IntervalBounds = paramsOutOfRange ? null : evaluateUncertainty(equation.PredictionInterval, variables, eOps.Value * unit.factor),
                                Value = eOps.Value * unit.factor
                            });
                        }//next equation
                        regressionregion.Extensions.ForEach(ext => evaluateExtension(ext, regressionregion));
                    }//next regressionregion
                    if (canAreaWeight(scenario.RegressionRegions))
                    {
                        var weightedRegion = evaluateWeightedAverage(scenario.RegressionRegions);
                        if (weightedRegion != null) scenario.RegressionRegions.Add(weightedRegion);
                    }//endif

                    var tz = evaluateTransitionBetweenFlowZones(scenario.RegressionRegions, regressionregionCoeff);
                    if (tz != null)
                    {
                        scenario.RegressionRegions.Add(tz);
                    }


                }//next scenario

                return scenarioList.AsQueryable();
            }
            catch (Exception ex)
            {
                sm("Error Estimating Scenarios: " + ex.Message, WIM.Resources.MessageType.error);
                throw;
            }
        }
        #endregion
        #region ReadOnly
        public IQueryable<ErrorType> GetErrors()
        {
            return this.Select<ErrorType>();
        }
        public Task<ErrorType> GetError(Int32 ID)
        {
            return this.Find<ErrorType>(ID);
        }

        public IQueryable<RegressionType> GetRegressions()
        {
            return this.Select<RegressionType>();
        }
        public IQueryable<RegressionType> GetRegressions(List<String> regionList, List<String> regressionRegionList, List<String> statisticgroupList)
        {
            return this.GetEquations(regionList, regressionRegionList, statisticgroupList).Select(e => e.RegressionType).Distinct().OrderBy(e => e.ID);
        }
        public IQueryable<RegressionType> GetRegressions(List<String> regionList, IGeometry geom, List<String> statisticgroupList)
        {
            return this.GetEquations(regionList, getRegressionRegionsByGeometry(geom).Select(rr => rr.ID.ToString()).ToList(), statisticgroupList).Select(e => e.RegressionType).Distinct().OrderBy(e => e.ID);
        }
        public Task<RegressionType> GetRegression(Int32 ID)
        {
            return this.Find<RegressionType>(ID);
        }

        public IQueryable<StatisticGroupType> GetStatisticGroups()
        {
            return this.Select<StatisticGroupType>();
        }
        public IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList, List<String> regressionRegionList, List<String> regressionsList)
        {
            return this.GetEquations(regionList, regressionRegionList,null, regressionsList).Select(e => e.StatisticGroupType).Distinct().OrderBy(e => e.ID);

        }
        public IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList, IGeometry geom, List<String> regressionsList)
        {
            return this.GetStatisticGroups(regionList, getRegressionRegionsByGeometry(geom).Select(rr=>rr.ID.ToString()).ToList(), regressionsList);
        }
        public Task<StatisticGroupType> GetStatisticGroup(Int32 ID)
        {
            return this.Find<StatisticGroupType>(ID);
        }

        public IQueryable<UnitType> GetUnits()
        {
            return this.Select<UnitType>();
        }
        public Task<UnitType> GetUnit(Int32 ID)
        {
            return this.Find<UnitType>(ID);
        }

        public IQueryable<UnitSystemType> GetUnitSystems()
        {
            return this.Select<UnitSystemType>();
        }
        public Task<UnitSystemType> GetUnitSystem(Int32 ID)
        {
            return this.Find<UnitSystemType>(ID);
        }

        public IQueryable<VariableType> GetVariables()
        {
            return this.Select<VariableType>();
        }
        public Task<VariableType> GetVariable(Int32 ID)
        {
            return this.Find<VariableType>(ID);
        }
        #endregion
        #endregion
        #region HELPER METHODS
        private Task Delete<T>(Int32 id) where T : class, new()
        {
            var entity = base.Find<T>(id).Result;            
            if (entity == null) return new Task(null);
            return base.Delete<T>(entity);
        }
        internal IQueryable<Equation> GetEquations(List<string> regionList, List<string> regressionRegions = null, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null)
        {

            IQueryable<Equation> equery = null;

            if (regionList != null && regionList.Count() > 0)
                equery = Select<RegionRegressionRegion>().Include(rrr=>rrr.Region).Include(rrr=>rrr.RegressionRegion).ThenInclude(rr=>rr.Equations)
                       .Where(rer => regionList.Contains(rer.Region.Code.ToLower().Trim())
                               || regionList.Contains(rer.RegionID.ToString())).SelectMany(rr => rr.RegressionRegion.Equations).Include(e => e.RegressionType).AsQueryable();
            else
                equery = Select<Equation>().Include(e => e.RegressionType).AsQueryable();

            if (regressionRegions != null && regressionRegions.Count() > 0)
                equery = equery.Include(e=>e.RegressionRegion).Where(e => regressionRegions.Contains(e.RegressionRegionID.ToString().Trim())
                                            || regressionRegions.Contains(e.RegressionRegion.Code.ToLower().Trim()));

            if (statisticgroupList != null && statisticgroupList.Count() > 0)
                equery = equery.Include(e=>e.StatisticGroupType).Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                        || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim()));

            if (regressionTypeIDList != null && regressionTypeIDList.Count() > 0)
                equery = equery.Where(e => regressionTypeIDList.Contains(e.RegressionTypeID.ToString().Trim())
                                        || regressionTypeIDList.Contains(e.RegressionType.Code.ToLower().Trim()));

            return equery.OrderBy(e => e.OrderIndex);
        }
        private IQueryable<T> getTable<T>(sqltypeenum type, object[] args) where T : class, new()
        {
            try
            {
                string sql = String.Format(getSQLStatement(type), args);
                return FromSQL<T>(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected IQueryable<ScenarioParameterView> GetScenarioParameterView()
        {
            try
            {
                string sql = getSQLStatement(sqltypeenum.ScenarioParameterView);
                Func<IDataReader, ScenarioParameterView> fromdr = (Func<IDataReader, ScenarioParameterView>)Delegate.CreateDelegate(typeof(Func<IDataReader, ScenarioParameterView>), null, typeof(ScenarioParameterView).GetMethod("FromDataReader"));

                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sql;
                    context.Database.OpenConnection();
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        return reader.Select<ScenarioParameterView>(fromdr).ToList().AsQueryable();
                    }//end using
                }//end using

            }
            catch (Exception)
            {

                throw;
            }
        }
        private string getSQLStatement(sqltypeenum type)
        {
            string sql = string.Empty;
            switch (type)
            {
                case sqltypeenum.ScenarioParameterView:
                    return @"SELECT e.""ID"" AS ""EquationID"",
		                            e.""RegressionTypeID"" AS ""RegressionTypeID"",
		                            e.""StatisticGroupTypeID"" AS ""StatisticGroupTypeID"",
		                            e.""RegressionRegionID"" AS ""RegressionRegionID"",
		                            rt.""Code"" AS ""RegressionTypeCode"",
		                            rr.""Name"" AS ""RegressionRegionName"",
		                            rr.""Code"" AS ""RegressionRegionCode"",
		                            st.""Code"" AS ""StatisticGroupTypeCode"",
		                            st.""Name"" AS ""StatisticGroupTypeName"",
		                            v.""ID"" AS ""VariableID"",
		                            v.""UnitTypeID"" AS ""UnitTypeID"",
		                            u.""Abbreviation"" AS ""UnitAbbr"",
		                            u.""Name"" AS ""UnitName"",
		                            u.""UnitSystemTypeID"" AS ""UnitSystemTypeID"",
		                            v.""MaxValue"" AS ""VariableMaxValue"",
		                            v.""MinValue"" AS ""VariableMinValue"", 
		                            vt.""Name"" AS ""VariableName"",
		                            vt.""Code"" AS ""VariableCode"",
		                            vt.""Description"" AS ""VariableDescription""
		                            FROM ""Equations"" e
		                            JOIN ""Variables"" v ON (v.""EquationID"" = e.""ID"")
		                            JOIN ""UnitType_view"" u ON (u.""ID"" = v.""UnitTypeID"")
		                            JOIN ""VariableType_view"" vt ON (vt.""ID"" = v.""VariableTypeID"")
		                            JOIN ""RegressionRegions"" rr ON ( rr.""ID"" = e.""RegressionRegionID"")
		                            JOIN ""RegressionType_view"" rt ON (rt.""ID"" = e.""RegressionTypeID"")
		                            JOIN ""StatisticGroupType_view"" st ON (st.""ID"" = e.""StatisticGroupTypeID"");";

                case sqltypeenum.managerCitations:
                    return @"SELECT DISTINCT c.* FROM ""RegionManager"" rm
                                JOIN ""RegionRegressionRegions"" rrr ON(rrr.""RegionID"" = rm.""RegionID"")
                                JOIN ""RegressionRegions"" rr ON(rrr.""RegressionRegionID"" = rr.""ID"") 
                                JOIN ""Citations"" c ON(c.""ID"" = rr.""CitationID"")
                                Where rm.""ManagerID"" = {0}; ";

                default:
                    throw new Exception("No sql for table " + type);
            }//end switch;

        }
        private bool canIncludeExension(string ex, int statisticCode)
        {
            switch (ex.ToUpper())
            {
                case "QPPQ":
                case "FDCTM":
                    if (statisticCode == 5) return true;
                    break;
            }//end switch
            return false;
        }
        private Extension getScenarioExtensionDef(string ex, int statisticCode)
        {
            switch (ex.ToUpper())
            {
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
                            Abbr = u.UnitTypeOut.Abbreviation,
                            Unit = u.UnitTypeOut.Name,
                            factor = u.Factor
                        }).First();

                }//end if

                return new SimpleUnitType() { Abbr = inUnitType.Abbreviation, Unit = inUnitType.Name, factor = 1 };
            }
            catch (Exception)
            {
                return new SimpleUnitType() { Abbr = inUnitType.Abbreviation, Unit = inUnitType.Name, factor = 1 };
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
        private bool valid(SimpleRegressionRegion regressionRegion, double? maxRegressionregion = null)
        {
            ExpressionOps eOps = null;
            try
            {
                if (regressionRegion.Parameters.Any(p => p.Value <= -999)) throw new Exception("One or more parameters are invalid");
                //check limitations
                foreach (var item in limitations.Where(l => l.RegressionRegionID == regressionRegion.ID))
                {
                    if (string.Equals(item.Criteria, "largestRegion", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!maxRegressionregion.HasValue || !regressionRegion.PercentWeight.HasValue) break;
                        if (regressionRegion.PercentWeight != maxRegressionregion)
                        {
                            sm($"Majority area Used for computation {regressionRegion.Name} removed PercentArea {Math.Round(regressionRegion.PercentWeight.Value, 2)}");
                            return false;
                        }
                        else
                            break;
                    }
                    var variables = regressionRegion.Parameters.Where(e => item.Variables.Any(v => v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, item.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code, true) == 0).UnitType.UnitSystemTypeID));
                    eOps = new ExpressionOps(item.Criteria, variables);
                    if (!eOps.IsValid) throw new Exception("Validation equation invalid.");
                    if (!Convert.ToBoolean(eOps.Value))
                    {
                        sm($"{regressionRegion.Name} removed; limitation criteria: {item.Criteria}");
                        return false;
                    }
                }//next item                
                return true;
            }
            catch (Exception ex)
            {
                sm($"{regressionRegion.Name} is not valid: {ex.Message}", WIM.Resources.MessageType.error);
                return false;
            }
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
            double Si = -999;
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
                if (predictionInterval == null || !predictionInterval.Variance.HasValue || !predictionInterval.Student_T_Statistic.HasValue ||
                    String.IsNullOrEmpty(predictionInterval.CovarianceMatrix) || string.IsNullOrEmpty(predictionInterval.XIRowVector) ||
                    !predictionInterval.BiasCorrectionFactor.HasValue) return null;

                rowVectorList = JsonConvert.DeserializeObject<List<string>>(predictionInterval.XIRowVector)
                    .Select(x => new ExpressionOps(x, variables).Value).ToList();
                //augement Xi with 1 as the first element
                rowVectorList.Insert(0, 1);

                xi = new double[1, rowVectorList.Count()];
                xiprime = new double[rowVectorList.Count(), 1];
                for (int i = 0; i < rowVectorList.Count(); i++)
                {
                    xi[0, i] = rowVectorList[i];
                    xiprime[i, 0] = rowVectorList[i];
                }//next i

                BCF = predictionInterval.BiasCorrectionFactor.Value;
                γ2 = predictionInterval.Variance.Value;
                studentT = predictionInterval.Student_T_Statistic.Value;
                U = JsonConvert.DeserializeObject<double[,]>(predictionInterval.CovarianceMatrix);

                xiu = MathOps.MatrixMultiply(xi, U);
                xiuxiprime = MathOps.MatrixMultiply(xiu, xiprime)[0, 0];

                Si = Math.Pow(γ2 + xiuxiprime, 0.5);

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
        private void evaluateExtension(Extension ext, SimpleRegressionRegion regressionregion)
        {
            ExtensionServiceAgentBase sa = null;
            try
            {
                switch (ext.Code.ToUpper())
                {
                    case "QPPQ":
                    case "FDCTM":
                        sa = new FDCTMServiceAgent(ext, new SortedDictionary<double, double>(regressionregion.Results.ToDictionary(k => Convert.ToDouble(k.Name.Replace("Percent Duration", "").Trim()) / 100, v => v.Value.Value)));
                        break;
                }//end switch

                if (sa.isInitialized && sa.Execute())
                    ext.Result = sa.Result;
            }
            catch (Exception ex)
            {
                this.sm($"Error evaluating extension: {ex.Message}", WIM.Resources.MessageType.error);                
            }
        }
        private bool canAreaWeight(List<SimpleRegressionRegion> regressionRegions)
        {
            double? areaSum;
            try
            {
                areaSum = regressionRegions.Sum(r => r.PercentWeight);
                if (!areaSum.HasValue || areaSum <= 0) return false;

                if (Math.Round(areaSum.Value) == 100 && regressionRegions.Count() > 1)
                {
                    //area weight internal components to see if match
                    return true;
                }
                else
                {
                    if (Math.Round(areaSum.Value) < 100)
                    {
                        string msg = "Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates. Percentage of area falls outside where region is undefined. Whole estimates have been provided using available regional equations.";
                        regressionRegions.ForEach(r => r.Disclaimer += msg);
                        sm(msg, WIM.Resources.MessageType.error);
                    }
                    if (Math.Round(areaSum.Value) > 100)
                        return regressionRegions.SelectMany(rr => rr.Results.Select(r => new { weight = rr.PercentWeight, code = r.code }))
                             .GroupBy(k => k.code).All(su => Math.Round(su.Sum(y => y.weight.Value)) == 100);

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        private SimpleRegressionRegion evaluateWeightedAverage(List<SimpleRegressionRegion> regressionRegions)
        {
            SimpleRegressionRegion weightedRR = null;
            try
            {

                weightedRR = new SimpleRegressionRegion();
                weightedRR.Name = "Area-Averaged";
                weightedRR.Code = "areaave";
                var Results = regressionRegions.SelectMany(x => x.Results.Select(r => r.Clone())
                                .Select(r => {
                                    r.Value = r.Value * x.PercentWeight / 100;
                                    if (r.Errors != null)
                                    {
                                        r.Errors.ForEach(e => { e.Value = e.Value * x.PercentWeight / 100; });
                                    }
                                    if (r.IntervalBounds != null)
                                    {
                                        r.IntervalBounds.Lower = r.IntervalBounds.Lower * x.PercentWeight.Value / 100;
                                        r.IntervalBounds.Upper = r.IntervalBounds.Upper * x.PercentWeight.Value / 100;
                                    }
                                    return r;
                                }))
                                .OfType<RegressionResult>().ToList();

                weightedRR.Results = this.AccumulateRegressionResults(Results).ToList();

                return weightedRR;
            }
            catch (Exception ex)
            {
                sm(ex.Message, WIM.Resources.MessageType.error);
                sm("Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.", WIM.Resources.MessageType.warning);
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
                sm(ex.Message, WIM.Resources.MessageType.error);
                sm("Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.", WIM.Resources.MessageType.warning);
                return null;
            }
        }
        private SimpleRegressionRegion evaluateTransitionBetweenFlowZones(List<SimpleRegressionRegion> regressionRegions, List<Coefficient> regressionregionCoeff)
        {
            SimpleRegressionRegion RRTransZone = null;
            ExpressionOps eOps = null;
            Dictionary<int, double> TransitionZoneCoeff = new Dictionary<int, double>();
            try
            {
                var coef = regressionregionCoeff.Where(rr => regressionRegions.Select(r => r.ID).Contains(rr.RegressionRegionID));
                if (coef.Count() <= 0 || coef.Count() != regressionRegions.Count()) return null;

                foreach (var rRegion in coef)
                {
                    var vars = regressionRegions.FirstOrDefault(e => e.ID == rRegion.RegressionRegionID).Parameters.Where(e => rRegion.Variables.Any(v => v.VariableType.Code == e.Code)).ToDictionary(k => k.Code, v => v.Value * getUnitConversionFactor(v.UnitType.ID, rRegion.Variables.FirstOrDefault(e => String.Compare(e.VariableType.Code, v.Code, true) == 0).UnitType.UnitSystemTypeID));
                    eOps = new ExpressionOps(rRegion.Criteria, vars);
                    if (!eOps.IsValid) throw new Exception();
                    if (!Convert.ToBoolean(eOps.Value)) return null;
                    //use coeff value to compute
                    eOps = new ExpressionOps(rRegion.Value, vars);
                    if (!eOps.IsValid) throw new Exception();
                    TransitionZoneCoeff[rRegion.RegressionRegionID] = eOps.Value;
                }//next regRegion            



                RRTransZone = new SimpleRegressionRegion();
                RRTransZone.Disclaimer = "Weighted-Averaged flows were computed using transition between flow zones. See referenced report for more details";

                RRTransZone.Name = "Weighted-Average";
                RRTransZone.Code = "transave";
                var Results = regressionRegions.SelectMany(x => x.Results.Select(r => r.Clone())
                    .Select(r => { r.Value = r.Value * TransitionZoneCoeff[x.ID]; return r; }))
                    .OfType<RegressionResult>();

                RRTransZone.Results = this.AccumulateRegressionResults(Results).ToList();

                return RRTransZone;
            }
            catch (Exception ex)
            {
                sm(ex.Message, WIM.Resources.MessageType.error);
                sm("Weighted flows were not calculated. Users should be careful to evaluate the applicability of the provided estimates.", WIM.Resources.MessageType.warning);
                return null;
            }
        }
        private IQueryable<NSSDB.Resources.RegressionRegion> getRegressionRegionsByGeometry(IGeometry geom)
        {
            if (geom.SRID != 102008) geom = geom.ProjectTo(102008);
            return Select<NSSDB.Resources.RegressionRegion>().Include(r=>r.Location).Where(x => x.Location != null && x.Location.Geometry.Intersects(geom));
        }

        protected override void sm(string msg, MessageType type = MessageType.info)
        {
            sm(new Message() { msg = msg, type = type });
        }
        private void sm(Message msg)
        {
            //wim_msgs comes from WIM.Standard/blob/staging/Services/Middleware/X-Messages.cs
            //manually added to avoid including libr in project.
            if (!this._messages.ContainsKey("wim_msgs"))
                this._messages["wim_msgs"] = new List<Message>();

            ((List<Message>)this._messages["wim_msgs"]).Add(msg);
        }
        #endregion

        private enum sqltypeenum
        {
            ScenarioParameterView,
            managerCitations
        }

    }
}
