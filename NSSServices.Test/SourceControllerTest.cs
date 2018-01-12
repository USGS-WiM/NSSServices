//Unit testing involves testing a part of an app in isolation from its infrastructure and dependencies. 
//When unit testing controller logic, only the contents of a single action is tested, not the behavior of 
//its dependencies or of the framework itself. As you unit test your controller actions, make sure you focus 
//only on its behavior. A controller unit test avoids things like filters, routing, or model binding. By focusing 
//on testing just one thing, unit tests are generally simple to write and quick to run. A well-written set of unit 
//tests can be run frequently without much overhead. However, unit tests do not detect issues in the interaction 
//between components, which is the purpose of integration testing.

using System;
using Xunit;
using System.Threading.Tasks;
using WaterUseAgent;
using NSSServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using WaterUseDB.Resources;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using WaterUseAgent.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NSSServices.XUnitTest
{
    public class SourceTest
    {
        public SourcesController controller { get; private set; }
        public SourceTest() {
            //Arrange
            controller = new RolesController(new InMemorySourceAgent());
            //must set explicitly for tests to work
            controller.ObjectValidator = new InMemoryModelValidator();


        }
        [Fact]
        public async Task GetAll()
        {           

            //Act
            var response = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<EnumerableQuery<UserType>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("MockTestRole2", result.LastOrDefault().Name);
            Assert.Equal("test mock role 2", result.LastOrDefault().FacilityCode);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var id = 1;

            //Act
            var response = await controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Role>(okResult.Value);
            
            Assert.Equal("MockTestRole1", result.Name);
            Assert.Equal("test mock role 1", result.Description);
        }

        [Fact]
        public async Task Post()
        {
            //Arrange
            var entity = new Role() {Name = "newRole", Description = "New mock role 3" };
   

            //Act
            var response = await controller.Post(entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Role>(okResult.Value);
            

            Assert.Equal("newRole", result.Name);
            Assert.Equal("New mock role 3", result.Description);
        }

        [Fact]
        public async Task Put()
        {
            //Arrange
            var get = await controller.Get(1);
            var okgetResult = Assert.IsType<OkObjectResult>(get);
            var entity = Assert.IsType<Role>(okgetResult.Value);

            entity.Name = "editedName";

            //Act
            var response = await controller.Post(entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Role>(okResult.Value);

            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);
            Assert.Equal(entity.ID, result.ID);
        }

        [Fact]
        public async Task Delete()
        {
            //Act
            await controller.Delete(1);

            var response = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<EnumerableQuery<Role>>(okResult.Value);

            Assert.Equal(1, result.Count());
            Assert.Equal("MockTestRole2", result.LastOrDefault().Name);
            Assert.Equal("test mock role 2", result.LastOrDefault().Description);
        }
    }

    public class InMemorySourceAgent : IWaterUseAgent
    {
        private List<Role> Roles { get; set; }
        public bool IncludePermittedWithdrawals { set => throw new NotImplementedException(); }

        public InMemorySourceAgent() {
           this.Roles = new List<Role>()
            { new Role() { ID=1,Name= "MockTestRole1", Description="test mock role 1" },
                new Role() { ID=2,Name= "MockTestRole2", Description="test mock role 2" }};
        
    }

        public Task<T> Add<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(Role))
            {
                Roles.Add(item as Role);
            }
            return Task.Run(()=> { return item; });
        }

        public Task<IEnumerable<T>> Add<T>(List<T> items) where T : class, new()
        {
            if (typeof(T) == typeof(Role))
            {
                Roles.AddRange(items.Cast<Role>());
            }
            return Task.Run(() => { return Roles.Cast<T>(); });
        }

        public Task Delete<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(Role))
            {                
                return Task.Run(()=> { this.Roles.Remove(item as Role); });
            }

            else
                throw new Exception("not of correct type");
        }

        public Task<T> Find<T>(int pk) where T : class, new()
        {
            if (typeof(T) == typeof(Role))
                return Task.Run(()=> { return Roles.Find(i => i.ID == pk) as T; });

            throw new Exception("not of correct type");
        }

        public Manager GetManagerByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetRoles()
        {
            return this.Roles.AsQueryable();
        }

        public IQueryable<T> Select<T>() where T : class, new()
        {
            if (typeof(T) == typeof(Role))
                return this.Roles.AsQueryable() as IQueryable<T>;

            throw new Exception("not of correct type");
        }

        public Task<T> Update<T>(int pkId, T item) where T : class, new()
        {
            if (typeof(T) == typeof(Role))
            {
                var index = this.Roles.FindIndex(x=>x.ID == pkId);
                (item as Role).ID = pkId; 
                this.Roles[index] = item as Role;
            }
            throw new Exception("not of correct type");
        }

        public Wateruse GetWateruse(List<string> sources, int startyear, int? endyear)
        {
            throw new NotImplementedException();
        }

        public Wateruse GetWateruse(object basin, int startyear, int? endyear)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, Wateruse> GetWaterusebySource(List<string> sources, int startyear, int? endyear)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, Wateruse> GetWaterusebySource(object basin, int startyear, int? endyear)
        {
            throw new NotImplementedException();
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
}
