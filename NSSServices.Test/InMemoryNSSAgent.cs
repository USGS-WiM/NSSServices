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
using NSSAgent;
using NSSAgent.Resources;
using NSSDB.Resources;
using SharedDB.Resources;
using WIM.Resources;
using WIM.Security.Authentication.Basic;

namespace NSSServices.Test
{
    public class InMemoryNSSAgent : INSSAgent
    {
        private List<Citation> Citations { get; set; }
        private List<Coefficient> Coefficients { get; set; }
        private List<EquationError> EquationErrors { get; set; }
        private List<Equation> Equations { get; set; }
        private List<EquationUnitType> EquationUnitTypes { get; set; }
        private List<Limitation> Limitations { get; set; }
        private List<Manager> Managers { get; set; }
        private List<PredictionInterval> PredictionIntervals { get; set; }
        private List<RegionRegressionRegion> RegionRegressionRegions { get; set; }
        private List<Region> Regions { get; set; }
        private List<RegressionRegion> RegressionRegions { get; set; }
        private List<Role> Roles { get; set; }
        private List<Variable> Variables { get; set; }
        private List<VariableUnitType> VariableUnitTypes { get; set; }

        private ReadOnlyCollection<ErrorType> Errors { get; set; }
        private ReadOnlyCollection<RegressionType> RegressionTypes { get; set; }
        private ReadOnlyCollection<StatisticGroupType> StatisticGroupTypes { get; set; }
        private ReadOnlyCollection<UnitConversionFactor> UnitConversionFactors { get; set; }
        private ReadOnlyCollection<UnitSystemType> UnitSystemTypes { get; set; }
        private ReadOnlyCollection<UnitType> UnitTypes { get; set; }
        private ReadOnlyCollection<VariableType> VariableTypes { get; set; }

        public InMemoryNSSAgent()
        {
            var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data");
            this.Citations = JsonConvert.DeserializeObject<List<Citation>>(System.IO.File.ReadAllText(Path.Combine(path, "citations.json")));
            this.Coefficients = JsonConvert.DeserializeObject<List<Coefficient>>(System.IO.File.ReadAllText(Path.Combine(path, "coefficients.json")));
            this.Managers = JsonConvert.DeserializeObject<List<Manager>>(System.IO.File.ReadAllText(Path.Combine(path, "managers.json")));
            this.Roles = JsonConvert.DeserializeObject<List<Role>>(System.IO.File.ReadAllText(Path.Combine(path, "roles.json")));

        }

        public Task<Manager> Add(Manager item)
        {
            try
            {
                //auto increment
                Int32 lastID = this.Managers.OrderBy(c => c.ID).Select(x => x.ID).Last();
                item.ID = lastID += 1;
                this.Managers.Add(item);
                return Task.Run(() => { return item; });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<IEnumerable<Manager>> Add(List<Manager> items)
        {
            throw new NotImplementedException();
        }

        public Task<Region> Add(Region item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Region>> Add(List<Region> items)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Add(Role item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> Add(List<Role> items)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Coefficient>> AddRegressionRegionCoefficients(int RegressionRegionID, List<Coefficient> items)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Limitation>> AddRegressionRegionLimitations(int RegressionRegionID, List<Limitation> items)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCoefficient(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLimitation(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManager(int pkID)
        {
            var managerTORemove = this.Managers.FirstOrDefault(m => m.ID == pkID);
            if (managerTORemove != null)
                this.Managers.Remove(managerTORemove);

            return Task.Run(() => { return; });
        }

        public Task DeleteRegion(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRole(int pkID)
        {
            throw new NotImplementedException();
        }

        public Task<Citation> GetCitation(int ID)
        {
            Citation citation = this.Citations.FirstOrDefault(c => c.ID == ID);
            return Task.Run(() => { return citation; });
        }

        public IQueryable<Citation> GetCitations()
        {
            try
            {
                return this.Citations.AsQueryable();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IQueryable<Citation> GetCitations(List<String> region, List<string> regressionRegionList, List<string> statisticgroupList, List<string> regressiontypeList)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorType> GetError(int ID)
        {
            ErrorType error = this.Errors.FirstOrDefault(c => c.ID == ID);
            return Task.Run(() => { return error; });
        }

        public IQueryable<ErrorType> GetErrors()
        {
            return this.Errors.AsQueryable();
        }

        public Task<Manager> GetManager(int ID)
        {
            Manager manager = this.Managers.FirstOrDefault(m => m.ID == ID);
            return Task.Run(() => { return manager; });
        }

        public IQueryable<Manager> GetManagers()
        {
            return this.Managers.AsQueryable();
        }

        public Task<Region> GetRegion(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Region> GetRegions()
        {
            throw new NotImplementedException();
        }

        public Task<RegressionType> GetRegression(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Coefficient> GetRegressionRegionCoefficients(int RegressionRegionID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Limitation> GetRegressionRegionLimitations(int RegressionRegionID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RegressionType> GetRegressions()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRole(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public IQueryable<StatisticGroupType> GetStatisticGroups()
        {
            throw new NotImplementedException();
        }

        public Task<StatisticGroupType> GetStatisticGroup(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<UnitType> GetUnit(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UnitType> GetUnits()
        {
            throw new NotImplementedException();
        }

        public Task<VariableType> GetVariable(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<VariableType> GetVariables()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coefficient> RemoveRegressionRegionCoefficients(int RegressionRegionID, List<Coefficient> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Limitation> RemoveRegressionRegionLimitations(int RegressionRegionID, List<Limitation> items)
        {
            throw new NotImplementedException();
        }

        public Task<Citation> Update(int pkId, Citation item)
        {
            Citation toUpdate = this.Citations.FirstOrDefault(c => c.ID == pkId);
            toUpdate.Author = item.Author;
            toUpdate.CitationURL = item.CitationURL;
            toUpdate.Title = item.Title;

            return Task.Run(() => { return toUpdate; });
        }

        public Task<Limitation> Update(int pkId, Limitation item)
        {
            throw new NotImplementedException();
        }

        public Task<Manager> Update(int pkId, Manager item)
        {
            Manager toUpdate = this.Managers.FirstOrDefault(c => c.ID == pkId);
            toUpdate.Email = item.Email;
            toUpdate.FirstName = item.FirstName;
            toUpdate.LastName = item.LastName;
            toUpdate.OtherInfo = item.OtherInfo;
            toUpdate.PrimaryPhone = item.PrimaryPhone;
            toUpdate.SecondaryPhone = item.SecondaryPhone;
   
            return Task.Run(() => { return toUpdate; });
        }

        public Task<Region> Update(int pkId, Region item)
        {
            throw new NotImplementedException();
        }

        public Task<Coefficient> Update(int pkId, Coefficient item)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Update(int pkId, Role item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Citation> GetManagerCitations(int managerID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RegressionRegion> GetRegressionRegions()
        {
            throw new NotImplementedException();
        }

        public Task<RegressionRegion> GetRegressionRegion(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<RegressionRegion> Add(RegressionRegion item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegressionRegion>> Add(List<RegressionRegion> items)
        {
            throw new NotImplementedException();
        }

        public Task<RegressionRegion> Update(int pkId, RegressionRegion item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRegressionRegion(int pkID)
        {
            throw new NotImplementedException();
        }

        public Region GetRegionByIDOrCode(string identifier)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UnitSystemType> GetUnitSystems()
        {
            throw new NotImplementedException();
        }

        public Task<UnitSystemType> GetUnitSystem(int ID)
        {
            throw new NotImplementedException();
        }

        public IBasicUser GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Scenario> GetScenarios(string region, List<string> regionEquationList, List<string> statisticgroupList = null, List<string> regressionTypeIDList = null, List<string> extensionMethodList = null, int systemtypeID = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Scenario> EstimateScenarios(List<string> regionList, List<Scenario> scenarioList, List<string> regionEquationList, List<string> statisticgroupList, List<string> regressiontypeList, List<string> extensionMethodList, int systemtypeID = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Region> GetManagerRegions(int managerID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RegressionRegion> GetRegressionRegions(List<string> regionList, List<string> statisticgroupList, List<string> regressiontypeList)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RegressionRegion> GetManagerRegressionRegions(int managerID)
        {
            throw new NotImplementedException();
        }

        public Task<RegionRegressionRegion> Add(RegionRegressionRegion item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegionRegressionRegion>> Add(List<RegionRegressionRegion> items)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RegressionType> GetRegressions(List<string> regionList, List<string> regressionRegionList, List<string> statisticgroupList)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StatisticGroupType> GetStatisticGroups(List<string> regionList, List<string> regressionRegionList, List<string> regressionsList)
        {
            throw new NotImplementedException();
        }
    }
}

public class InMemoryModelValidator : IObjectModelValidator
{
    public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
    {
        //assume all is valid
        return;
    }
}

public static class InMemoryPrincipalClaim
{
    public static ClaimsPrincipal claimsPrincipal = getInMemoryClaimPrinciple("Manager");
    public static ClaimsPrincipal adminClaimsPrinciple = getInMemoryClaimPrinciple("Administrator");

    private static ClaimsPrincipal getInMemoryClaimPrinciple(string usertype)
    {
        var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "User", ClaimValueTypes.String),
                    new Claim(ClaimTypes.Surname, usertype, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, usertype+"Email@email.com", ClaimValueTypes.String),
                    new Claim(ClaimTypes.Role, usertype, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Anonymous, usertype=="Administrator"?"1":"2", ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.PrimarySid, "1", ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.NameIdentifier, usertype+"User",ClaimValueTypes.String)
                        };
        var userIdentity = new ClaimsIdentity(claims, "test");
        return new ClaimsPrincipal(userIdentity);
    }


}