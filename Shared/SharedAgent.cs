using System;
using WIM.Resources;
using SharedDB.Resources;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WIM.Utilities;
using SharedDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NSSDB.Resources;
using NSSAgent;

namespace SharedAgent
{
    public interface ISharedAgent
    {
        //Errors
        Task<ErrorType> Add(ErrorType item);
        Task<IEnumerable<ErrorType>> Add(List<ErrorType> items);
        Task<ErrorType> Update(Int32 pkId, ErrorType item);
        Task DeleteError(Int32 pkID);

        //RegressionTypes
        Task<RegressionType> Add(RegressionType item);
        Task<IEnumerable<RegressionType>> Add(List<RegressionType> items);
        Task<RegressionType> Update(Int32 pkId, RegressionType item);
        Task DeleteRegressionType(Int32 pkID);

        //StatisticGroups
        Task<StatisticGroupType> Add(StatisticGroupType item);
        Task<IEnumerable<StatisticGroupType>> Add(List<StatisticGroupType> items);
        Task<StatisticGroupType> Update(Int32 pkId, StatisticGroupType item);
        Task DeleteStatisticGroup(Int32 pkID);

        //UnitSystems
        Task<UnitSystemType> Add(UnitSystemType item);
        Task<IEnumerable<UnitSystemType>> Add(List<UnitSystemType> items);
        Task<UnitSystemType> Update(Int32 pkId, UnitSystemType item);
        Task DeleteUnitSystem(Int32 pkID);

        //Unit
        Task<UnitType> Add(UnitType item);
        Task<IEnumerable<UnitType>> Add(List<UnitType> items);
        Task<UnitType> Update(Int32 pkId, UnitType item);
        Task DeleteUnit(Int32 pkID);

        //VariableTypes
        Task<VariableType> Add(VariableType item);
        Task<IEnumerable<VariableType>> Add(List<VariableType> items);
        Task<VariableType> Update(Int32 pkId, VariableType item);
        Task DeleteVariableType(Int32 pkID);

        //Regions
        Task<Region> Add(Region item);
        Task<IEnumerable<Region>> Add(List<Region> items);
        Task<Region> Update(Int32 pkId, Region item);
        Task DeleteRegion(Int32 pkID);

        //Managers
        Task<Manager> Add(Manager item);
        Task<IEnumerable<Manager>> Add(List<Manager> items);
        Task<Manager> Update(Int32 pkId, Manager item);
        Task DeleteManager(Int32 pkID);

        //RegionManagers
        Task<RegionManager> Add(RegionManager item);
        Task<IEnumerable<RegionManager>> Add(List<RegionManager> items);

        IQueryable<ApplicationInfo> GetApplicationInfo(string application);
        IQueryable<GeneralInfomation> GetGeneralInfomation(string state);

    }
    public class SharedAgent: DBAgentBase, ISharedAgent
    {
        #region "Properties"
        private readonly IDictionary<Object, Object> _messages;
        #endregion
        #region Constructor
        public SharedAgent(SharedDBContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _messages = httpContextAccessor.HttpContext.Items;
            //optimize query for disconnected databases.
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        #endregion
        #region METHODS
        #region ErrorTypes
        public Task<ErrorType> Add(ErrorType item)
        {
            return this.Add<ErrorType>(item);
        }
        public Task<IEnumerable<ErrorType>> Add(List<ErrorType> items)
        {
            return this.Add<ErrorType>(items);
        }
        public Task<ErrorType> Update(Int32 pkId, ErrorType item)
        {
            return this.Update<ErrorType>(pkId, item);
        }
        public Task DeleteError(Int32 pkID)
        {
            return this.Delete<ErrorType>(pkID);
        }
        #endregion
        #region RegressionTypes
        public Task<RegressionType> Add(RegressionType item)
        {
            //be sure ID is set to 0
            item.ID = 0;
            return this.Add<RegressionType>(item);
        }
        public Task<IEnumerable<RegressionType>> Add(List<RegressionType> items)
        {
            return this.Add<RegressionType>(items);
        }
        public Task<RegressionType> Update(Int32 pkId, RegressionType item)
        {
            return this.Update<RegressionType>(pkId, item);
        }
        public Task DeleteRegressionType(Int32 pkID)
        {
            return this.Delete<RegressionType>(pkID);
        }
        #endregion
        #region StatisticGroupTypes
        public Task<StatisticGroupType> Add(StatisticGroupType item)
        {
            return this.Add<StatisticGroupType>(item);
        }
        public Task<IEnumerable<StatisticGroupType>> Add(List<StatisticGroupType> items)
        {
            return this.Add<StatisticGroupType>(items);
        }
        public Task<StatisticGroupType> Update(Int32 pkId, StatisticGroupType item)
        {
            return this.Update<StatisticGroupType>(pkId, item);
        }
        public Task DeleteStatisticGroup(Int32 pkID)
        {
            return this.Delete<StatisticGroupType>(pkID);
        }
        #endregion
        #region UnitSystemTypes
        public Task<UnitSystemType> Add(UnitSystemType item)
        {
            return this.Add<UnitSystemType>(item);
        }
        public Task<IEnumerable<UnitSystemType>> Add(List<UnitSystemType> items)
        {
            return this.Add<UnitSystemType>(items);
        }
        public Task<UnitSystemType> Update(Int32 pkId, UnitSystemType item)
        {
            return this.Update<UnitSystemType>(pkId, item);
        }
        public Task DeleteUnitSystem(Int32 pkID)
        {
            return this.Delete<UnitSystemType>(pkID);
        }
        #endregion
        #region UnitTypes
        public Task<UnitType> Add(UnitType item)
        {
            return this.Add<UnitType>(item);
        }
        public Task<IEnumerable<UnitType>> Add(List<UnitType> items)
        {
            return this.Add<UnitType>(items);
        }
        public Task<UnitType> Update(Int32 pkId, UnitType item)
        {
            return this.Update<UnitType>(pkId, item);
        }
        public Task DeleteUnit(Int32 pkID)
        {
            return this.Delete<UnitType>(pkID);
        }
        #endregion
        #region VariableTypes
        public Task<VariableType> Add(VariableType item)
        {
            return this.Add<VariableType>(item);
        }
        public Task<IEnumerable<VariableType>> Add(List<VariableType> items)
        {
            return this.Add<VariableType>(items);
        }
        public Task<VariableType> Update(Int32 pkId, VariableType item)
        {
            return this.Update<VariableType>(pkId, item);
        }
        public Task DeleteVariableType(Int32 pkID)
        {
            return this.Delete<VariableType>(pkID);
        }
        #endregion
        #region Regions
        public Task<Region> Add(Region item)
        {
            return this.Add<Region>(item);
        }
        public Task<IEnumerable<Region>> Add(List<Region> items)
        {
            return this.Add<Region>(items);
        }
        public Task<Region> Update(Int32 pkId, Region item)
        {
            return this.Update<Region>(pkId, item);
        }
        public Task DeleteRegion(Int32 pkID)
        {
            return this.Delete<Region>(pkID);
        }
        #endregion
        #region Managers
        public Task<Manager> Add(Manager item)
        {
            return this.Add<Manager>(item);
        }
        public Task<IEnumerable<Manager>> Add(List<Manager> items)
        {
            return this.Add<Manager>(items);
        }
        public Task<Manager> Update(Int32 pkId, Manager item)
        {
            return this.Update<Manager>(pkId, item);
        }
        public Task DeleteManager(Int32 pkID)
        {
            return this.Delete<Manager>(pkID);
        }
        #endregion
        #region RegionManagers
        public Task<RegionManager> Add(RegionManager item)
        {
            return this.Add<RegionManager>(item);
        }
        public Task<IEnumerable<RegionManager>> Add(List<RegionManager> items)
        {
            return this.Add<RegionManager>(items);
        }
        #endregion

        public IQueryable<ApplicationInfo> GetApplicationInfo(string application)
        {
            var query = this.Select<ApplicationInfo>();
            var results = query.Where(x => x.Application.Equals(application));

            return results;
        }

        public IQueryable<GeneralInfomation> GetGeneralInfomation(string state)
        {
            var query = this.Select<GeneralInfomation>();
            var results = query.Where(x => x.State.Equals(state));

            return results;
        }


        #endregion
        #region HELPER METHODS
        private Task Delete<T>(Int32 id) where T : class, new()
        {
            var entity = base.Find<T>(id).Result;
            if (entity == null) return new Task(null);
            return base.Delete<T>(entity);
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
    }
}
