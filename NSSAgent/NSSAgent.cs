﻿//------------------------------------------------------------------------------
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
using WIM.Security.Authentication;
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
using System.Reflection;
using WIM.Exceptions.Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIM.Security;


namespace NSSAgent
{
    public interface INSSAgent:IAuthenticationAgent 
    {
        String[] allowableGeometries { get; }

        //Citations
        Task<Citation> GetCitation(Int32 ID);
        IQueryable<Citation> GetCitations(List<string> regionList = null, IGeometry geom = null, List<string> regressionRegionList = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null);
        IQueryable<Citation> GetManagedCitations(Manager manager, List<string> regionList=null, IGeometry geometry = null, List<string> regressionRegionList=null, List<string> statisticgroupList=null, List<string> regressiontypeList=null);
        IQueryable<Citation> GetManagerCitations(int managerID);
        Task<Citation> Update(Int32 pkId, Citation item);
        Task DeleteCitation(Int32 id);

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
        IQueryable<Region> GetManagedRegions(Manager manager);
        Task<Region> GetRegion(Int32 ID);
        Region GetRegionByIDOrCode(string identifier);
        IQueryable<Region> GetManagerRegions(int managerID);
        Task<Region> Add(Region item);
        Task<IEnumerable<Region>> Add(List<Region> items);
        Task<Region> Update(Int32 pkId, Region item);
        Task DeleteRegion(Int32 pkID);

        //RegressionRegions
        IQueryable<RegressionRegion> GetRegressionRegions(List<string> regionList = null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null);
        IQueryable<RegressionRegion> GetManagedRegressionRegions(Manager manager, List<string> regionList = null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null);
        Task<RegressionRegion> GetRegressionRegion(Int32 ID);
        IQueryable<RegressionRegion> GetManagerRegressionRegions(int managerID);
        Task<RegressionRegion> Add(RegressionRegion item);
        Task<IEnumerable<RegressionRegion>> Add(List<NSSDB.Resources.RegressionRegion> items);
        Task<RegressionRegion> Update(Int32 pkId, NSSDB.Resources.RegressionRegion item);
        Task DeleteRegressionRegion(Int32 pkID);

        //Coefficents
        IQueryable<Coefficient> GetRegressionRegionCoefficients(Int32 RegressionRegionID);
        Task<IEnumerable<Coefficient>> AddRegressionRegionCoefficients(Int32 RegressionRegionID, List<Coefficient> items);
        IEnumerable<Coefficient> RemoveRegressionRegionCoefficients(int RegressionRegionID, List<Coefficient> items);
        Task<Coefficient> Update(Int32 pkId, Coefficient item);
        Task DeleteCoefficient(Int32 pkID);

        //Roles
        IQueryable<string> GetRoles();
        
        //Scenarios
        //IQueryable<Scenario> GetScenarios(List<string> regionList, List<string> regressionRegionList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0);
        IQueryable<Scenario> GetScenarios(List<string> regionList=null, IGeometry geom=null, List<string> regressionRegionList = null, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0, Manager manager = null);
        IQueryable<Scenario> EstimateScenarios(List<string> regionList, List<Scenario> scenarioList, List<string> regionEquationList, List<string> statisticgroupList, List<string> regressiontypeList, List<string> extensionMethodList, Int32 systemtypeID = 0);
        Task<Scenario> Update(Scenario item, string existingStatisticGroup = null);
        Task<IQueryable<Scenario>> Add(Scenario item);
        Task DeleteScenario(int regressionregionID, int statisticgroupID, int regressiontypeID);

        //Readonly (Shared Views) methods
        IQueryable<ErrorType> GetErrors();
        Task<ErrorType> GetError(Int32 ID);
        IQueryable<RegressionType> GetRegressions(List<String> regionList=null, IGeometry geom = null, List<String> regressionRegionList=null, List<String> statisticgroupList=null);
        IQueryable<RegressionType> GetManagedRegressions(Manager manager, List<String> regionList = null, IGeometry geom = null, List<String> regressionRegionList = null, List<String> statisticgroupList = null);
        Task<RegressionType> GetRegression(Int32 ID);
        IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList=null, IGeometry geom = null, List<String> regressionRegionList=null, List<String> regressionsList = null);
        IQueryable<StatisticGroupType> GetManagedStatisticGroups(Manager manager, List<String> regionList = null, IGeometry geom = null, List<String> regressionRegionList = null, List<String> regressionsList = null);
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
        string[] INSSAgent.allowableGeometries => new String[] { "Polygon", "MultiPolygon" };        
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
            if (statisticgroupList?.Any() != true && regressiontypeList?.Any() != true && !regressiontypeList.Any() != true && geom == null)
                // for region only list
                return Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include(rrr => rrr.RegressionRegion).ThenInclude(rr=>rr.Citation)
                       .Where(rer => (regionList.Contains(rer.Region.Code.ToLower().Trim())
                               || regionList.Contains(rer.RegionID.ToString())) && rer.RegressionRegion.CitationID !=null).Select(r => r.RegressionRegion.Citation).Distinct().AsQueryable();

            if (geom != null)
                regressionRegionList = getRegressionRegionsByGeometry(geom).Select(rr => rr.ID.ToString()).ToList();
   
            return this.GetEquations(regionList, regressionRegionList, statisticgroupList, regressiontypeList).Select(e => e.RegressionRegion.Citation).Distinct().OrderBy(e => e.ID);
        }
        public IQueryable<Citation> GetManagedCitations(Manager manager, List<string> regionList = null, IGeometry geom = null, List<string> regressionRegionList = null, List<string> statisticgroupList = null, List<string> regressiontypeList = null)
        {
            if (manager.Role.Equals(Role.Admin))
                return GetCitations(regionList, geom, regressionRegionList, statisticgroupList, regressiontypeList);

            //return only managed citations
            var query = this.Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include("RegressionRegion.Citation")
                .Where(rrr => rrr.Region.RegionManagers.Any(rm => rm.ManagerID == manager.ID));


            if (regionList != null && regionList.Any())
                query = query.Where(r => regionList.Contains(r.Region.Code.ToLower().Trim())
                               || regionList.Contains(r.RegionID.ToString()));

            if (regressionRegionList != null && regressionRegionList.Any())
                query = query.Where(rr => regressionRegionList.Contains(rr.RegressionRegion.Code.ToLower().Trim())
                               || regressionRegionList.Contains(rr.RegressionRegionID.ToString()));


            if (statisticgroupList != null && statisticgroupList.Any())
                query = query.Include("RegressionRegion.Equations.StatisticGroupType").Where(c => c.RegressionRegion.Equations.Any(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                          || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim())));

            if (regressiontypeList != null && regressiontypeList.Any())
                query = query.Include("RegressionRegion.Equations.RegressionType").Where(c => c.RegressionRegion.Equations.Any(e => regressiontypeList.Contains(e.RegressionTypeID.ToString().Trim())
                                          || regressiontypeList.Contains(e.RegressionType.Code.ToLower().Trim())));

            if (geom != null)
                query = query.Where(rr=> getRegressionRegionsByGeometry(geom).Select(grr => grr.ID).Contains(rr.RegressionRegionID));

            return query.Select(rrr => rrr.RegressionRegion.Citation).Distinct();
            
        }
        public IQueryable<Citation> GetManagerCitations(int managerID)
        {
            return this.getTable<Citation>(sqltypeenum.managerCitations, new Object[managerID]);
        }
        public Task<Citation> Update(int pkId, Citation item)
        {
            return this.Update<Citation>(pkId, item);
        }
        public Task DeleteCitation(Int32 pkID)
        {
            return this.Delete<Citation>(pkID);
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
        public IUser GetUserByUsername(string username)
        {
            try
            {

                return Select<Manager>().FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));                    
            }
            catch (Exception ex)
            {
                sm("Failed to get user by username " + ex.Message, MessageType.error);
                return null;
            }
        }
        public IUser GetUserByID(int id) {
            return this.getUserByIDAsync(id).Result;
        }
        private  async Task<IUser> getUserByIDAsync(int id)
        {
            return await Find<Manager>(id);
        }
        public IUser AuthenticateUser(string username, string password)
        {
            try
            {
                var user = (Manager)GetUserByUsername(username);
                if (user == null || !WIM.Security.Cryptography.VerifyPassword(password, user.Salt, user.Password))
                {
                    return null;
                }
                return user;

            }
            catch (Exception ex)
            {
                sm("Error authenticaticating user "+ ex.Message, MessageType.error);
                return null;
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
        public IQueryable<Region> GetManagedRegions(Manager manager)
        {
            if (manager.Role.Equals(Role.Admin))//administrator
                return GetRegions();

            return this.Select<RegionRegressionRegion>().Include(rrr => rrr.Region)
                .Where(rrr => rrr.Region.RegionManagers.Any(rm => rm.ManagerID == manager.ID)).Select(rrr => rrr.Region).Distinct();

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
        public RegressionRegion GetRegressionRegionByIDOrCode(string identifier)
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
        public IQueryable<RegressionRegion> GetRegressionRegions(List<string> regionList=null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null)
        {
            Dictionary<Int32, RegressionRegion> regressionRegionList = null;
            if (!regionList.Any() && geom== null&& !statisticgroupList.Any() && !regressiontypeList.Any())
                return this.Select<RegressionRegion>();           

            if (statisticgroupList?.Any() != true && regressiontypeList?.Any() != true && geom == null)
                // for region only list
                return Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include(rrr => rrr.RegressionRegion)
                       .Where(rer => regionList.Contains(rer.Region.Code.ToLower().Trim())
                               || regionList.Contains(rer.RegionID.ToString())).Select(r=>r.RegressionRegion).AsQueryable();


            if (geom != null)
                regressionRegionList = getRegressionRegionsByGeometry(geom).ToDictionary(k => k.ID);            

            return this.GetEquations(regionList, regressionRegionList?.Keys.Select(k=>k.ToString()).ToList(), statisticgroupList, regressiontypeList).Select(e => e.RegressionRegion).Distinct()
                .Select(rr=> new RegressionRegion() {
                    ID = rr.ID,
                    Name = rr.Name,
                    Code = rr.Code,
                    CitationID = rr.CitationID,
                    Description = regressionRegionList != null ? regressionRegionList[rr.ID].Description : rr.Description,
                    Area = regressionRegionList != null ? regressionRegionList[rr.ID].Area:null,
                    PercentWeight = regressionRegionList != null ? regressionRegionList[rr.ID].PercentWeight : null,
                }).OrderBy(e => e.ID);
        }
        public IQueryable<RegressionRegion> GetManagedRegressionRegions(Manager manager, List<string> regionList = null, IGeometry geom = null, List<String> statisticgroupList = null, List<String> regressiontypeList = null)
        {
            if (manager.Role.Equals(Role.Admin))//administrator
                return GetRegressionRegions(regionList, geom, statisticgroupList, regressiontypeList);

            //return only managed citations
            var query = this.Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include("RegressionRegion")
                .Where(rrr => rrr.Region.RegionManagers.Any(rm => rm.ManagerID == manager.ID));


            if (regionList != null && regionList.Any())
                query = query.Where(r => regionList.Contains(r.Region.Code.ToLower().Trim())
                               || regionList.Contains(r.RegionID.ToString()));

           
            if (statisticgroupList != null && statisticgroupList.Any())
                query = query.Include("RegressionRegion.Equations.StatisticGroupType").Where(c => c.RegressionRegion.Equations.Any(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                          || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim())));

            if (regressiontypeList != null && regressiontypeList.Any())
                query = query.Include("RegressionRegion.Equations.RegressionType").Where(c => c.RegressionRegion.Equations.Any(e => regressiontypeList.Contains(e.RegressionTypeID.ToString().Trim())
                                          || regressiontypeList.Contains(e.RegressionType.Code.ToLower().Trim())));

            if (geom != null)
                query = query.Where(rr => getRegressionRegionsByGeometry(geom).Select(grr => grr.ID).Contains(rr.RegressionRegionID));

            return query.Select(rrr => rrr.RegressionRegion).Distinct();

        }
        public Task<RegressionRegion> GetRegressionRegion(int ID)
        {
            return this.Find<NSSDB.Resources.RegressionRegion>(ID);
        }
        public IQueryable<RegressionRegion> GetManagerRegressionRegions(int managerID)
        {
            return Select<RegionManager>().Where(rm => rm.ManagerID == managerID)
                                .Include("Region.RegionRegressionRegions.RegressionRegion")
                                .SelectMany(e => e.Region.RegionRegressionRegions.Select(s => s.RegressionRegion));
        }
        public Task<RegressionRegion> Add(NSSDB.Resources.RegressionRegion item)
        {
            return this.Add<NSSDB.Resources.RegressionRegion>(item);
        }
        public Task<IEnumerable<NSSDB.Resources.RegressionRegion>> Add(List<NSSDB.Resources.RegressionRegion> items)
        {
            return this.Add<NSSDB.Resources.RegressionRegion>(items);
        }
        public Task<RegressionRegion> Update(int pkId, NSSDB.Resources.RegressionRegion item)
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
        public IQueryable<String> GetRoles()
        {
            return Role.ToList().AsQueryable();
        }

        #endregion        
        #region Scenarios
        //public IQueryable<Scenario> GetScenarios(List<string> regionList, List<string> regressionRegionList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0)
        //{
        //    IQueryable<ScenarioParameterView> equery = null;
        //    List<Coefficient> flowCoefficents = new List<Coefficient>();
        //    try
        //    {                             
                
        //        flowCoefficents = Select<Coefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();
                

        //        List<RegionRegressionRegion> SelectedRegionRegressions = Select<RegionRegressionRegion>().Include("RegressionRegion")
        //                   .Where(rer => regionList.Contains(rer.Region.Code.ToLower().Trim())
        //                   || regionList.Contains(rer.RegionID.ToString())).ToList();             


        //        if (regressionRegionList != null && regressionRegionList.Count() > 0)
        //            SelectedRegionRegressions = SelectedRegionRegressions.Where(e => regressionRegionList.Contains(e.RegressionRegionID.ToString()) || regressionRegionList.Contains(e.RegressionRegion.Code.ToLower().Trim())).ToList();

        //        equery = GetScenarioParameterView().Where(s => SelectedRegionRegressions.Select(rr => rr.RegressionRegionID).Contains(s.RegressionRegionID));
                

        //        if (statisticgroupList != null && statisticgroupList.Count() > 0)
        //            equery = equery.Where(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
        //                                    || statisticgroupList.Contains(e.StatisticGroupTypeCode.ToLower().Trim()));

        //        if (regressionTypeIDList != null && regressionTypeIDList.Count() > 0)
        //            equery = equery.Where(e => regressionTypeIDList.Contains(e.RegressionTypeID.ToString().Trim())
        //                                    || regressionTypeIDList.Contains(e.RegressionTypeCode.ToLower().Trim()));


        //        return equery.ToList().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { groupkey = key, groupedparameters = g })
        //            .Select(s => new Scenario()
        //            {
        //                StatisticGroupID = s.groupkey,
        //                StatisticGroupName = s.groupedparameters.First().StatisticGroupTypeName,
        //                RegressionRegions = s.groupedparameters.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { groupkey = key, groupedparameters = g }).ToList()
        //                .Select(r => new SimpleRegressionRegion()
        //                {
        //                    Extensions = extensionMethodList.Where(ex => canIncludeExension(ex, s.groupkey)).Select(ex => getScenarioExtensionDef(ex, s.groupkey)).ToList(),
        //                    ID = r.groupkey,
        //                    Name = r.groupedparameters.First().RegressionRegionName,
        //                    Code = r.groupedparameters.First().RegressionRegionCode,
        //                    Parameters = r.groupedparameters.Select(p => new Parameter()
        //                    {
        //                        ID = p.VariableID,
        //                        UnitType = getUnit(new UnitType() { ID = p.UnitTypeID, Name = p.UnitName, Abbreviation = p.UnitAbbr, UnitSystemTypeID = p.UnitSystemTypeID }, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID),
        //                        Limits = new Limit() { Min = p.VariableMinValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID), Max = p.VariableMaxValue * this.getUnitConversionFactor(p.UnitTypeID, systemtypeID > 0 ? systemtypeID : p.UnitSystemTypeID) },
        //                        Code = p.VariableCode,
        //                        Description = p.VariableDescription,
        //                        Name = p.VariableName,
        //                        Value = -999.99
        //                    }).Union(limitations.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
        //                    {
        //                        ID = v.VariableType.ID,
        //                        UnitType = getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
        //                        Code = v.VariableType.Code,
        //                        Description = v.VariableType.Description,
        //                        Name = v.VariableType.Name,
        //                        Value = -999.99
        //                    })).Union(flowCoefficents.Where(l => l.RegressionRegionID == r.groupkey).SelectMany(l => l.Variables).Select(v => new Parameter()
        //                    {
        //                        ID = v.VariableType.ID,
        //                        UnitType = this.getUnit(v.UnitType, systemtypeID > 0 ? systemtypeID : v.UnitType.UnitSystemTypeID),
        //                        Code = v.VariableType.Code,
        //                        Description = v.VariableType.Description,
        //                        Name = v.VariableType.Name,
        //                        Value = -999.99
        //                    })).Distinct().ToList()
        //                }).ToList()
        //            }).AsQueryable();
        //    }
        //    catch (Exception ex)
        //    {
        //        sm("Error getting Scenario: " + ex.Message,WIM.Resources.MessageType.error);
        //        throw;
        //    }
        //}
        public IQueryable<Scenario> GetScenarios(List<string> regionList=null, IGeometry geom=null, List<string> regressionRegionList = null, List<string> statisticgroupList = null, List<string> regressiontypeList = null, List<string> extensionMethodList = null, Int32 systemtypeID = 0, Manager manager = null)
        {
            Dictionary<Int32, RegressionRegion> regressionRegions = null;
            List<Coefficient> flowCoefficents = new List<Coefficient>();

            try
            {
                flowCoefficents = Select<Coefficient>().Include("Variables.VariableType").Include("Variables.UnitType").ToList();

                if (geom != null)
                {
                    regressionRegions = getRegressionRegionsByGeometry(geom).ToDictionary(k => k.ID);
                    regressionRegionList = regressionRegions.Keys.ToList().ConvertAll(s=>s.ToString());
                }
                if (manager?.Username != null && !manager.Role.Equals(Role.Admin))
                {
                    var managedRegionList = Select<Manager>().Include("RegionManagers.Region").Where(r => r.ID == manager.ID).SelectMany(m => m.RegionManagers.Select(rm=>rm.Region));
                    if (regionList?.Any() != true)
                        regionList = managedRegionList.Select(r => r.ID.ToString()).ToList();
                    else
                        regionList = managedRegionList.Where(r => regionList.Contains(r.ID.ToString()) || regionList.Contains(r.Code.ToLower())).Select(r => r.ID.ToString()).ToList();

                    
                    if (regionList?.Any() != true)
                        throw new BadRequestException("When authenticated, user must specify a managed region");
                }

                var equ = this.GetEquations(regionList, regressionRegionList, statisticgroupList, regressiontypeList)
                    .Include("Variables.VariableType").Include("Variables.UnitType").Include(e => e.StatisticGroupType).Include(e => e.RegressionRegion)
                    .Include("PredictionInterval").Include("EquationErrors.ErrorType").Include(e => e.UnitType);

               
                return equ.AsEnumerable().GroupBy(e => e.StatisticGroupTypeID, e => e, (key, g) => new { groupkey = key, groupedparameters = g })
                    .Select(s => new Scenario()
                    {
                        StatisticGroupID = s.groupkey,
                        StatisticGroupName = s.groupedparameters.First().StatisticGroupType.Name,
                        RegressionRegions = s.groupedparameters.ToList().GroupBy(e => e.RegressionRegionID, e => e, (key, g) => new { groupkey = key, groupedparameters = g }).ToList()
                        .Select(r => new SimpleRegressionRegion()
                        {
                            Extensions = extensionMethodList?.Where(ex => canIncludeExension(ex, s.groupkey)).Select(ex => getScenarioExtensionDef(ex, s.groupkey)).ToList(),
                            ID = r.groupkey,
                            Name = r.groupedparameters.First().RegressionRegion.Name,
                            Code = r.groupedparameters.First().RegressionRegion.Code,
                            PercentWeight = (regressionRegions!=null && regressionRegions.ContainsKey(r.groupkey))?regressionRegions[r.groupkey].PercentWeight:null,
                            AreaSqMile = (regressionRegions != null && regressionRegions.ContainsKey(r.groupkey)) ? regressionRegions[r.groupkey].Area : null,
                            Regressions = (manager?.Username != null) ? r.groupedparameters.Select(rg=>new Regression()
                                                                                                    {
                                                                                                        ID = rg.RegressionTypeID,
                                                                                                        code = rg.RegressionType.Code,
                                                                                                        Name= rg.RegressionType.Name,
                                                                                                        Description = rg.RegressionType.Description,
                                                                                                        Errors = rg.EquationErrors?.Select(rger => new Error()
                                                                                                        {
                                                                                                            ID = rger.ErrorTypeID,
                                                                                                            Value = rger.Value,
                                                                                                            Code = rger.ErrorType?.Code,
                                                                                                            Name = rger.ErrorType?.Name
                                                                                                        }).ToList(),
                                                                                                        Unit = new SimpleUnitType() { ID = rg.UnitTypeID, Abbr = rg.UnitType?.Abbreviation, Unit = rg.UnitType?.Name},
                                                                                                        Equation = rg.Expression,
                                                                                                        EquivalentYears = rg.EquivalentYears,
                                                                                                        PredictionInterval = rg.PredictionIntervalID.HasValue? new PredictionInterval() {
                                                                                                                                                                                            ID = rg.PredictionIntervalID.Value,
                                                                                                                                                                                            BiasCorrectionFactor = rg.PredictionInterval.BiasCorrectionFactor,
                                                                                                                                                                                            Student_T_Statistic = rg.PredictionInterval.Student_T_Statistic,
                                                                                                                                                                                            Variance = rg.PredictionInterval.Variance,
                                                                                                                                                                                            XIRowVector = rg.PredictionInterval.XIRowVector,
                                                                                                                                                                                            CovarianceMatrix = rg.PredictionInterval.CovarianceMatrix
                                                                                                                                                                                        } :null
                                                                                                    }).ToList() : null,
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
                        regressionregion.Results = new List<RegressionBase>();
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
                        regressionregion.Extensions?.ForEach(ext => evaluateExtension(ext, regressionregion));
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
        public async Task<IQueryable<Scenario>> Add(Scenario item)
        {
            
            List<Equation> newEquations = new List<Equation>();
            try
            {

                foreach (var rregion in item.RegressionRegions)
                {
                    // todo: get parameters if not null.
                    foreach (var regression in rregion.Regressions)
                    {
                        var rr = Find<RegressionRegion>(rregion.ID);
                        var sg = Find<StatisticGroupType>(item.StatisticGroupID);
                        var unit = Find<UnitType>(regression.Unit.ID);
                        var reg = Find<RegressionType>(regression.ID);

                        await Task.WhenAll(rr, sg, reg, unit);

                        var eqErrors = this.Select<ErrorType>().Where(e => regression.Errors.Select(er => er.ID).Contains(e.ID)).ToList();                    
                        var variables = this.Select<VariableType>().Where(v => (rregion.Parameters.Any() ? rregion.Parameters : regression.Parameters).Select(p => p.Code.ToLower()).Contains(v.Code.ToLower())).ToList();
                        var Units = this.Select<UnitType>().Where(v => (rregion.Parameters.Any() ? rregion.Parameters : regression.Parameters).Select(p => p.UnitType.ID).Contains(v.ID)).ToList();

                        var neq = new Equation()
                        {
                            RegressionRegionID = (await rr).ID,
                            UnitTypeID = (await unit).ID,
                            Expression = regression.Equation,
                            RegressionTypeID = (await reg).ID,
                            StatisticGroupTypeID = (await sg).ID,
                            EquivalentYears = regression.EquivalentYears,
                            Variables = (rregion.Parameters.Any() ? rregion.Parameters : regression.Parameters).Select(p=> new Variable()
                            {
                                VariableTypeID = variables.FirstOrDefault(e=>e.Code.ToLower() == p.Code.ToLower()).ID,
                                MinValue = p.Limits.Min,
                                MaxValue = p.Limits.Max,
                                UnitTypeID = Units.FirstOrDefault(u=>u.ID == p.UnitType.ID).ID       
                                  
                            }).ToList(),
                            PredictionInterval = regression.PredictionInterval,
                            EquationErrors = regression.Errors.Select(er => new EquationError()
                            {
                                ErrorTypeID = eqErrors.FirstOrDefault(e => e.ID == er.ID).ID,
                                Value = er.Value
                            }).ToList()
                        };

                        //check if valid before uploading
                        if (valid(neq,regression.Expected))
                            newEquations.Add(neq);

                    }//next regression
                }//next regregion                                

                //submit
                var newItems = await Task.WhenAll(newEquations.Select(e => this.Add<Equation>(e)).ToList());
                if (!newItems.Any()) throw new Exception("Scenario failed to submit to repository. See messages for more information");

                return GetScenarios(null, null, newItems.Select(i => i.RegressionRegionID.ToString()).ToList(), newItems.Select(i => i.StatisticGroupTypeID.ToString()).ToList(),
                                                    newItems.Select(i => i.RegressionTypeID.ToString()).ToList(),null,0,new Manager() { Username = "temporary", Role= Role.Admin});
                
            }
            catch (Exception ex)
            {
                sm($"Error adding scenario {ex.Message}", MessageType.error);
                throw;
            }
        }
        public async Task<Scenario> Update(Scenario item, string existingStatisticGroup = null)
        {
            List<string> regressiontypeList = null;
            List<RegressionRegion> regressionregionList = null;

            try
            {
                StatisticGroupType sg = (!string.IsNullOrEmpty(existingStatisticGroup))? 
                    this.Select<StatisticGroupType>().FirstOrDefault(s => s.Code == existingStatisticGroup || 
                                                                            s.ID.ToString()== existingStatisticGroup):
                                                                                this.Select<StatisticGroupType>().FirstOrDefault(s => s.ID == item.StatisticGroupID);

                regressionregionList = item.RegressionRegions.Select(rr => new RegressionRegion() { ID = rr.ID, Code = rr.Code.ToLower() }).Distinct().ToList();
                regressiontypeList = item.RegressionRegions.SelectMany(s => s.Regressions?.Select(x => x.code.ToLower())).ToList();

                var AvailableScenarios = GetScenarios(null, null, regressionregionList.Select(s => s.Code).ToList(),new List<string>() { sg.ID.ToString() }, regressiontypeList, null, 0);
                if (AvailableScenarios.Count() != 1) throw new BadRequestException("More or less than 1 scenario returned, cannot complete update.");


                var equationsToUpdate = this.GetEquations(null, regressionregionList.Select(s => s.Code).ToList(), new List<string>() { sg.ID.ToString() }, regressiontypeList)
                                        .Include("Variables.VariableType").Include("Variables.UnitType").Include(e => e.StatisticGroupType).Include(p=>p.PredictionInterval).Include("EquationErrors.ErrorType");

            

                foreach (var equation in equationsToUpdate)
                {
                    var regressionregion = item.RegressionRegions.FirstOrDefault(rr=> rr.ID == equation.RegressionRegionID);     
                    
                    var regression = regressionregion.Regressions.FirstOrDefault(r => r.ID == equation.RegressionTypeID);
                    var associatedVariables = regressionregion.Parameters != null ? regressionregion.Parameters : regression.Parameters;
                    var errors = regression.Errors;
                    var unit = regression.Unit.ID > 0 ? await Find<UnitType>(regression.Unit.ID) : Select<UnitType>().FirstOrDefault(u => string.Equals(u.Abbreviation, regression.Unit.Abbr,StringComparison.CurrentCultureIgnoreCase));

                    var variables = this.Select<VariableType>().Where(v => (regressionregion.Parameters.Any() ? regressionregion.Parameters : regression.Parameters).Select(p => p.Code.ToLower()).Contains(v.Code.ToLower())).ToList();
                    var eqErrors = this.Select<ErrorType>().Where(er => regression.Errors.Select(e => e.ID).Contains(er.ID)).ToList();
                    

                    var Units = this.Select<UnitType>().Where(ut => (regressionregion.Parameters.Any()? regressionregion.Parameters : regression.Parameters).Any(u => (u.UnitType.ID > 0 && ut.ID == u.UnitType.ID) || string.Equals(ut.Abbreviation, u.UnitType.Abbr, StringComparison.CurrentCultureIgnoreCase))).ToList();

                    equation.RegressionRegionID = regressionregion.ID;
                    equation.UnitTypeID = unit.ID;
                    equation.Expression = regression.Equation;
                    //equation.RegressionTypeID = (await reg).ID;
                    equation.StatisticGroupTypeID = item.StatisticGroupID;
                    equation.EquivalentYears = regression.EquivalentYears;

                    //predictionInterval
                    if (regression.PredictionInterval != null)
                    {
                        if (equation.PredictionInterval != null)
                        {
                            //reset values
                            equation.PredictionInterval.BiasCorrectionFactor = regression.PredictionInterval.BiasCorrectionFactor;
                            equation.PredictionInterval.CovarianceMatrix = regression.PredictionInterval.CovarianceMatrix;
                            equation.PredictionInterval.Student_T_Statistic = regression.PredictionInterval.Student_T_Statistic;
                            equation.PredictionInterval.Variance = regression.PredictionInterval.Variance;
                            equation.PredictionInterval.XIRowVector = regression.PredictionInterval.XIRowVector;
                        }
                        else
                        {
                            equation.PredictionInterval = new PredictionInterval()
                            {
                                BiasCorrectionFactor = regression.PredictionInterval.BiasCorrectionFactor,
                                CovarianceMatrix = regression.PredictionInterval.CovarianceMatrix,
                                Student_T_Statistic = regression.PredictionInterval.Student_T_Statistic,
                                Variance = regression.PredictionInterval.Variance,
                                XIRowVector = regression.PredictionInterval.XIRowVector
                            };
                        }//endif
                    }//end if
                    else
                    {
                        equation.PredictionInterval = null;
                    }
                        
                    //variables
                    var varList = associatedVariables?.Where(v => regression.Equation == null || regression.Equation.Contains(v.Code)).ToList();
                    var variablesToRemove = equation.Variables?.Where(x => !varList.Any(y => y.Code == x.VariableType.Code)).ToList();
                    var variablesToAdd = varList?.Where(y => equation.Variables == null || !equation.Variables.Any(x => y.Code == x.VariableType.Code)).Select(v=> new Variable()
                    {
                        VariableTypeID = variables.FirstOrDefault(e => e.Code.ToLower() == v.Code.ToLower()).ID,
                        MinValue = v.Limits.Min,
                        MaxValue = v.Limits.Max,
                        UnitTypeID = Units.FirstOrDefault(u => u.ID == v.UnitType.ID || string.Equals(u.Abbreviation,v.UnitType.Abbr,StringComparison.CurrentCultureIgnoreCase)).ID
                    }).ToList();
                    var variablesToKeep = equation.Variables.Where(x => varList.Any(y => y.Code == x.VariableType.Code)).ToList();


                    variablesToRemove?.ForEach(v=> Delete<Variable>(v));
                    variablesToAdd?.ForEach(v => { if (equation.Variables == null) equation.Variables = new List<Variable>(); equation.Variables.Add(v); });
                    foreach (var variable in variablesToKeep)
                    {
                        var editvariable = varList.FirstOrDefault(v => v.Code == variable.VariableType.Code);
                        variable.MinValue = editvariable.Limits.Min;
                        variable.MaxValue = editvariable.Limits.Max;
                        variable.UnitTypeID = Units.FirstOrDefault(u => u.ID == editvariable.UnitType.ID || string.Equals(u.Abbreviation, editvariable.UnitType.Abbr, StringComparison.CurrentCultureIgnoreCase)).ID;                        
                    }//next variable

                    //EquationErrors
                    var EquationErrorToRemove = equation.EquationErrors?.Where(x => errors == null || !errors.Any(y => y.ID == x.ErrorTypeID)).ToList();
                    var EquationErrorToAdd = errors?.Where(y => equation.EquationErrors == null || !equation.EquationErrors.Any(x => y.ID == x.ErrorTypeID)).Select(v => new EquationError()
                    {
                        ErrorTypeID = eqErrors.FirstOrDefault(e => e.ID == v.ID).ID,
                        Value = v.Value
                    }).ToList();
                    var EquationErrorToKeep = equation.EquationErrors?.Where(x => errors == null || errors.Any(y => y.ID == x.ErrorTypeID)).ToList();

                    EquationErrorToRemove?.ForEach(v => Delete<EquationError>(v));
                    EquationErrorToAdd?.ForEach(v => { if (equation.EquationErrors == null) equation.EquationErrors = new List<EquationError>(); equation.EquationErrors.Add(v); });
                    if (EquationErrorToKeep != null)
                    {
                        foreach (var error in EquationErrorToKeep)
                        {
                            var editError = errors.FirstOrDefault(v => v.ID == error.ErrorTypeID);
                            error.Value = editError.Value;                            
                        }//next error
                    }

                    if (valid(equation, regression.Expected))
                        await this.Update<Equation>(equation.ID, equation);
                    

                }//next equation
                


                return GetScenarios(null, null, regressionregionList.Select(s => s.Code).ToList(),
                                    new List<string>() { item.StatisticGroupID.ToString() }, regressiontypeList, null, 0, new Manager() { Username="temporary", Role= Role.Admin }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                sm($"Error adding scenario {ex.Message}", MessageType.error);
                throw;
            }
        }
        public Task DeleteScenario(int regressionregionID, int statisticgroupID, int regressiontypeID)
        {
            try
            {
                
                var equationToDelete = this.GetEquations(null, new List<string>() { regressionregionID.ToString() }, 
                                                                new List<string>() { statisticgroupID.ToString() }, 
                                                                new List<string>() { regressiontypeID.ToString() });
                if(equationToDelete.Count() != 1) throw new BadRequestException("More than 1 scenario returned with the given query parameters, cannot delete. Please contact the system administrator");

                return Delete<Equation>(equationToDelete.FirstOrDefault());
            }
            catch (Exception ex)
            {

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
        public ErrorType GetErrorByCode(string code)
        {
            return this.Select<ErrorType>().FirstOrDefault(e => e.Code == code);
        }
        public RegressionType GetRegressionByCode(string code)
        {
            return this.Select<RegressionType>().FirstOrDefault(r => r.Code == code);
        }
        public IQueryable<RegressionType> GetRegressions(List<String> regionList=null, IGeometry geom = null, List<String> regressionRegionList=null, List<String> statisticgroupList=null)
        {          
            if (regionList?.Any() != true && geom == null && regressionRegionList?.Any()!= true && statisticgroupList?.Any()!=true)
                    return this.Select<RegressionType>();

            var equations = this.GetEquations(regionList, regressionRegionList, statisticgroupList);
            if (geom != null)
                equations = equations.Where(e => getRegressionRegionsByGeometry(geom).Select(rr => rr.ID).Contains(e.RegressionRegion.ID));

            return equations.Select(e => e.RegressionType).Distinct().OrderBy(e => e.ID);
        }
        public IQueryable<RegressionType> GetManagedRegressions(Manager manager, List<String> regionList = null, IGeometry geom = null, List<String> regressionRegionList = null, List<String> statisticgroupList = null)
        {
            if (manager.Role.Equals(Role.Admin))//administrator
                return GetRegressions(regionList, geom, regressionRegionList, statisticgroupList);

            //return only managed citations
            var query = this.Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include("RegressionRegion.Equations.RegressionType")
                .Where(rrr => rrr.Region.RegionManagers.Any(rm => rm.ManagerID == manager.ID));


            if (regionList != null && regionList.Any())
                query = query.Where(r => regionList.Contains(r.Region.Code.ToLower().Trim())
                               || regionList.Contains(r.RegionID.ToString()));

            if (regressionRegionList != null && regressionRegionList.Any())
                query = query.Where(rr => regressionRegionList.Contains(rr.RegressionRegion.Code.ToLower().Trim())
                               || regressionRegionList.Contains(rr.RegressionRegionID.ToString()));


            if (statisticgroupList != null && statisticgroupList.Any())
                query = query.Include("RegressionRegion.Equations.StatisticGroupType").Where(c => c.RegressionRegion.Equations.Any(e => statisticgroupList.Contains(e.StatisticGroupTypeID.ToString().Trim())
                                          || statisticgroupList.Contains(e.StatisticGroupType.Code.ToLower().Trim())));


            if (geom != null)
                query = query.Where(rr => getRegressionRegionsByGeometry(geom).Select(grr => grr.ID).Contains(rr.RegressionRegionID));

            return query.SelectMany(rrr => rrr.RegressionRegion.Equations.Select(e=>e.RegressionType)).Distinct();
        }
        public Task<RegressionType> GetRegression(Int32 ID)
        {
            return this.Find<RegressionType>(ID);
        }
        public StatisticGroupType GetStatisticGroupByCode(string code)
        {
            return this.Select<StatisticGroupType>().FirstOrDefault(s => s.Code == code);
        }
        public IQueryable<StatisticGroupType> GetStatisticGroups(List<String> regionList=null, IGeometry geom = null, List<String> regressionRegionList= null, List<String> regressionsList=null)
        {
            if (regionList?.Any() != true && geom == null && regressionRegionList?.Any() != true && regressionsList?.Any() != true)
                return this.Select<StatisticGroupType>();

            var equations = this.GetEquations(regionList, regressionRegionList, null, regressionsList);

            if (geom != null)
                equations = equations.Where(e => getRegressionRegionsByGeometry(geom).Select(rr => rr.ID).Contains(e.RegressionRegion.ID));

            return equations.Select(e => e.StatisticGroupType).Distinct().OrderBy(e => e.ID);

        }
        public IQueryable<StatisticGroupType> GetManagedStatisticGroups(Manager manager, List<String> regionList = null, IGeometry geom = null, List<String> regressionRegionList = null, List<String> regressionsList = null)
        {
            if (manager.Role.Equals(Role.Admin))
                return GetStatisticGroups(regionList, geom, regressionRegionList, regressionsList);

            //return only managed citations
            var query = this.Select<RegionRegressionRegion>().Include(rrr => rrr.Region).Include("RegressionRegion.Equations.StatisticGroupType")
                .Where(rrr => rrr.Region.RegionManagers.Any(rm => rm.ManagerID == manager.ID));


            if (regionList != null && regionList.Any())
                query = query.Where(r => regionList.Contains(r.Region.Code.ToLower().Trim())
                               || regionList.Contains(r.RegionID.ToString()));

            if (regressionRegionList != null && regressionRegionList.Any())
                query = query.Where(rr => regressionRegionList.Contains(rr.RegressionRegion.Code.ToLower().Trim())
                               || regressionRegionList.Contains(rr.RegressionRegionID.ToString()));


            if (regressionsList != null && regressionsList.Any())
                query = query.Include("RegressionRegion.Equations.RegressionType").Where(c => c.RegressionRegion.Equations.Any(e => regressionsList.Contains(e.RegressionTypeID.ToString().Trim())
                                          || regressionsList.Contains(e.RegressionType.Code.ToLower().Trim())));


            if (geom != null)
                query = query.Where(rr => getRegressionRegionsByGeometry(geom).Select(grr => grr.ID).Contains(rr.RegressionRegionID));

            return query.SelectMany(rrr => rrr.RegressionRegion.Equations.Select(e => e.StatisticGroupType)).Distinct();

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
        public UnitType GetUnitByAbbreviation(string abbr)
        {
            return this.Select<UnitType>().FirstOrDefault(s => s.Abbreviation == abbr);
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
        public VariableType GetVariableByCode(string code)
        {
            return this.Select<VariableType>().FirstOrDefault(v => v.Code == code);
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
                case sqltypeenum.regionbygeom:
                    return @"WITH Feature(geom) as (
                                SELECT st_makevalid(ST_Transform(
	                                ST_SetSRID(
			                                ST_GeomFromText('{0}'),
                                    4236),
                                102008)))
                                SELECT ""ID"",""Name"", ""Code"", ""Description"", ""CitationID"", ""StatusID"",""LocationID"", ""Area"" * 0.000000386102159 as ""Area"", ""MaskArea""* 0.000000386102159 as ""MaskArea"", ""Area""/""MaskArea""*100 as ""PercentWeight"" FROM
                                    (SELECT r.*, st_area(ST_Intersection(l.""Geometry"", f.geom)) as ""Area"", st_area(f.geom) as ""MaskArea""
                                    FROM Feature AS f, nss.""RegressionRegions"" AS r
                                    LEFT JOIN nss.""Locations"" AS l ON r.""LocationID"" = l.""ID""
                                    WHERE r.""LocationID"" IS NOT NULL AND(ST_Intersects(l.""Geometry"", f.geom) = TRUE)) t";

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
                        Description = "Estimates the flow at an ungaged site given the reference streamgage",
                        Name = "Flow Duration Curve Transfer Method",
                        Parameters = new List<ExtensionParameter>{new ExtensionParameter() { Code = "sid", Name="NWIS Station ID", Description="USGS NWIS Station Identifier", Value="01234567" },
                                     new ExtensionParameter() { Code = "sdate", Name="Start Date", Description="start date of returned flow estimate", Value=  DateTime.MinValue },
                                     new ExtensionParameter() { Code = "edate", Name ="End Date", Description="end date of returned flow estimate", Value= DateTime.Today }
                       }

                    };

            }//end switch
            return null;
        }
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

                return new SimpleUnitType() { ID = inUnitType.ID, Abbr = inUnitType.Abbreviation, Unit = inUnitType.Name, factor = 1 };
            }
            catch (Exception)
            {
                return new SimpleUnitType() { Abbr = inUnitType.Abbreviation, Unit = inUnitType.Name, factor = 1 };
            }
        }  
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
        private bool valid (Equation equation,ExpectedValue expected)
        {
            ExpressionOps eOps = null;
            try
            {
                var results = new List<ValidationResult>();
                var isValidEquation = Validator.TryValidateObject(equation, new ValidationContext(equation, serviceProvider: null, items: null),results);
                equation.EquationErrors.All(ee => Validator.TryValidateObject(ee, new ValidationContext(ee, serviceProvider: null, items: null), results));
                if (equation.PredictionInterval != null)
                    Validator.TryValidateObject(equation.PredictionInterval, new ValidationContext(equation.PredictionInterval, serviceProvider: null, items: null), results);

                equation.Variables.All(v => Validator.TryValidateObject(v, new ValidationContext(v, serviceProvider: null, items: null), results));
                if (results.Any()) {
                    results.ForEach(vr => sm(vr.ErrorMessage, MessageType.error));                    
                    return false;
                }


                var variables = expected.Parameters.ToDictionary(k => k.Key, v => (double?)v.Value);
                eOps = new ExpressionOps(equation.Expression, variables);

                if (!eOps.IsValid) { sm($"Scenario expression failed to execute. {equation.Expression} is invalid"); return false; }
                if (eOps.Value != expected.Value) { sm($"Expected value {expected.Value} does not match computed {eOps.Value}"); return false; }

                if (equation.PredictionInterval != null)
                {
                    IntervalBounds computedBound = evaluateUncertainty(equation.PredictionInterval, variables, eOps.Value);
                    if (computedBound?.Upper != expected.IntervalBounds?.Upper || computedBound?.Lower != expected.IntervalBounds?.Lower)
                    {
                        sm($"Expected interval bound values; Upper: {expected.IntervalBounds.Upper}, Lower: {expected.IntervalBounds.Lower} do not match computed; Upper: {computedBound?.Upper}, Lower: {computedBound?.Lower}");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                sm($"equation failed to validate, {ex.Message}");
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
            catch (Exception ex)
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
                                .Select(r =>
                                {
                                    r.Value = 1;
                                    r.Value = r.Value * (x.PercentWeight.HasValue ? x.PercentWeight.Value / 100 : 1);
                                    if (r.Errors != null)
                                    {
                                        r.Errors.ForEach(e => { e.Value = e.Value * (x.PercentWeight.HasValue? x.PercentWeight.Value / 100:1); });
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
        private IEnumerable<RegressionBase> AccumulateRegressionResults(IEnumerable<RegressionResult> regressionresults)
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
        private IQueryable<RegressionRegion> getRegressionRegionsByGeometry(IGeometry geom)
        {

            //var query = getTable<RegressionRegion>(sqltypeenum.regionbygeom, new Object[] { geom.AsText() });
            //The below method fails to return all overlay regions, Left here because it still does a good job and may need to extend 
            //if (geom.SRID != 102008)
            //    geom = geom.ProjectTo(102008);
            //if (!geom.IsValid)
            //    geom = geom.Normalized().Buffer(0);
            //var query = Select<RegressionRegion>().Include(r => r.Location).Where(x => x.Location != null && x.Location.Geometry.Intersects(geom));

            //https://github.com/aspnet/EntityFrameworkCore/issues/7810#issuecomment-384909854
            try
            {
                var query = new List<RegressionRegion>();
                using (var command = this.context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandTimeout = 3 * 60; //3 min
                    command.CommandText = String.Format(getSQLStatement(sqltypeenum.regionbygeom), geom.AsText());
                    context.Database.OpenConnection();
                    using (System.Data.Common.DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.HasColumn("ID") && reader.HasColumn("Name") && reader.HasColumn("Code") && reader.HasColumn("Area")&& reader.HasColumn("PercentWeight"))
                                query.Add(new RegressionRegion() {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Name = reader["Name"].ToString(),
                                    Code = reader["Code"].ToString() ,
                                    Area = Convert.ToDouble(reader["Area"]),
                                    PercentWeight = Math.Round(Convert.ToDouble(reader["PercentWeight"]), 2, MidpointRounding.AwayFromZero),
                                    Description = reader.HasColumn("MaskArea")? $"Regression region Percent Area computed with a Mask Area of {Convert.ToDouble(reader["MaskArea"])} sqr. miles and overlay Area of {Convert.ToDouble(reader["Area"])} sqr miles": string.Empty
                                });
                        }
                    }//end using  
                }//end using 
                return query.Where(r=> r.PercentWeight.Value>1).AsQueryable();
            }
            catch (Exception ex)
            {
                sm($"Error occured computing percent overlay {ex.Message}", MessageType.error);
                throw;
            }
            finally
            {
                if (context.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
                    context.Database.CloseConnection();
            }
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
            managerCitations,
            regionbygeom
        }

    }
}
