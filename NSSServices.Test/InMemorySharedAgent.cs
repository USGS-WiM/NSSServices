using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using SharedAgent;
using NSSDB.Resources;
using SharedDB.Resources;
using WIM.Resources;
using WIM.Security.Authentication.Basic;

namespace NSSServices.Test
{
    public class InMemorySharedAgent : ISharedAgent
    {

        private ReadOnlyCollection<ErrorType> ErrorTypes { get; set; }
        private ReadOnlyCollection<RegressionType> RegressionTypes { get; set; }
        private ReadOnlyCollection<StatisticGroupType> StatisticGroupTypes { get; set; }
        private ReadOnlyCollection<UnitConversionFactor> UnitConversionFactors { get; set; }
        private ReadOnlyCollection<UnitSystemType> UnitSystemTypes { get; set; }
        private ReadOnlyCollection<UnitType> UnitTypes { get; set; }
        private ReadOnlyCollection<VariableType> VariableTypes { get; set; }

        public InMemorySharedAgent()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Data");
            
        }

        public Task<ErrorType> Add(ErrorType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ErrorType>> Add(List<ErrorType> items)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorType> Update(int pkId, ErrorType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteError(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<RegressionType> Add(RegressionType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegressionType>> Add(List<RegressionType> items)
        {
            throw new NotImplementedException();
        }

        public Task<RegressionType> Update(int pkId, RegressionType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRegressionType(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<StatisticGroupType> Add(StatisticGroupType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StatisticGroupType>> Add(List<StatisticGroupType> items)
        {
            throw new NotImplementedException();
        }

        public Task<StatisticGroupType> Update(int pkId, StatisticGroupType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStatisticGroup(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<UnitSystemType> Add(UnitSystemType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UnitSystemType>> Add(List<UnitSystemType> items)
        {
            throw new NotImplementedException();
        }

        public Task<UnitSystemType> Update(int pkId, UnitSystemType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUnitSystem(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<UnitType> Add(UnitType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UnitType>> Add(List<UnitType> items)
        {
            throw new NotImplementedException();
        }

        public Task<UnitType> Update(int pkId, UnitType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUnit(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<VariableType> Add(VariableType item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VariableType>> Add(List<VariableType> items)
        {
            throw new NotImplementedException();
        }

        public Task<VariableType> Update(int pkId, VariableType item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVariable(int pkID)
        {
            throw new NotImplementedException();
        }
    }
}