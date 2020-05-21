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
using Shared.Resources;
using NSSDB.Resources;

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

        //Variables
        Task<VariableType> Add(VariableWithUnit item);
        Task<IEnumerable<VariableType>> Add(List<VariableWithUnit> items);
        Task<VariableType> Update(Int32 pkId, VariableType item);
        Task DeleteVariable(Int32 pkID);
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
        #region DamageTypes
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
        public Task<VariableType> Add(VariableWithUnit item)
        {
            VariableType tempVT = new VariableType();
            VariableUnitType tempVUT = new VariableUnitType();

            tempVUT.VariableID = item.ID;
            tempVUT.UnitTypeID = item.UnitTypeID;

            tempVT.ID = item.ID;
            tempVT.Name = item.Name;
            tempVT.Code = item.Code;
            tempVT.Description = item.Description;

            
            this.Add<VariableUnitType>(tempVUT);
            return this.Add<VariableType>(tempVT);
        }
        public Task<IEnumerable<VariableType>> Add(List<VariableWithUnit> items)
        {
            List<VariableType> tempVTList = new List<VariableType>();
            List<VariableUnitType> tempVUTList = new List<VariableUnitType>();

            foreach(var item in items)
            {
                VariableType tempVT = new VariableType();
                VariableUnitType tempVUT = new VariableUnitType();

                tempVUT.VariableID = item.ID;
                tempVUT.UnitTypeID = item.UnitTypeID;

                tempVT.ID = item.ID;
                tempVT.Name = item.Name;
                tempVT.Code = item.Code;
                tempVT.Description = item.Description;

                tempVTList.Add(tempVT);
                tempVUTList.Add(tempVUT);
            }

            this.Add<VariableUnitType>(tempVUTList);
            return this.Add<VariableType>(tempVTList);
        }
        public Task<VariableType> Update(Int32 pkId, VariableType item)
        {
            return this.Update<VariableType>(pkId, item);
        }
        public Task DeleteVariable(Int32 pkID)
        {
            return this.Delete<VariableType>(pkID);
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
