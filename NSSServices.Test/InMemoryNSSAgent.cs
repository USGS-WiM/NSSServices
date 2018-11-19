using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NSSAgent;
using NSSDB.Resources;
using SharedDB.Resources;
using WiM.Resources;

namespace NSSServices.Test
{
    public class InMemoryNSSAgent : INSSAgent
    {
        private List<Citation> Citations { get; set; }
        private List<Role> Roles { get; set; }
        private List<Manager> Managers { get; set; }
        private List<ErrorType> Errors { get; set; }

        public InMemoryNSSAgent()
        {
            this.Citations = new List<Citation>()
                        { new Citation(){ Author = "test Author", CitationURL="url", ID = 1, Title="test title" },
                          new Citation(){ Author = "test2 Author", CitationURL="url2", ID = 2, Title="test2 title" }
                        };
            this.Errors = new List<ErrorType>()
                    { new ErrorType() { ID=1,Name= "Error Test", Code ="error1" },
                        new ErrorType() { ID=2,Name= "Error Test 2", Code="error2" }};
            this.Roles = new List<Role>()
                    { new Role() { ID=1,Name= "MockTestRole1", Description="test mock role 1" },
                        new Role() { ID=2,Name= "MockTestRole2", Description="test mock role 2" }};
            this.Managers = new List<Manager>()
                        { new Manager(){  FirstName = "Test", LastName="Manager", ID = 1, Username="testManager", Email="test@usgs.gov",RoleID=2},
                          new Manager(){ FirstName = "Test", LastName="Admin", ID = 2, Username="testAdmin", Email="testAdmin@usgs.gov",RoleID=1 }
                        };
        }

        public List<Message> Messages => new List<Message>();
        
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