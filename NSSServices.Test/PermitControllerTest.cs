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
    public class PermitTest
    {
        public PermitController controller { get; private set; }
        public PermitTest() {
            //Arrange
            controller = new PermitController(new InMemoryPermitAgent());
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
            var result = Assert.IsType<EnumerableQuery<UnitSystemType>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("MockTestRole2", result.LastOrDefault().PermitNO);
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
            var result = Assert.IsType<UnitSystemType>(okResult.Value);
            
            Assert.Equal("MockTestRole1", result.PermitNO);
        }

        [Fact]
        public async Task Post()
        {
            //Arrange
            var entity = new UnitSystemType() {PermitNO = "newPermitNo"};
   

            //Act
            var response = await controller.Post(entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<UnitSystemType>(okResult.Value);
            

            Assert.Equal("newPermitNo", result.PermitNO);
        }

        [Fact]
        public async Task Put()
        {
            //Arrange
            var get = await controller.Get(1);
            var okgetResult = Assert.IsType<OkObjectResult>(get);
            var entity = Assert.IsType<UnitSystemType>(okgetResult.Value);

            entity.PermitNO = "editedPermitNo";

            //Act
            var response = await controller.Put(1, entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<UnitSystemType>(okResult.Value);

            Assert.Equal(entity.PermitNO, result.PermitNO);
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
            var result = Assert.IsType<EnumerableQuery<UnitSystemType>>(okResult.Value);

            Assert.Equal(1, result.Count());
            Assert.Equal("MockTestPermit2", result.LastOrDefault().PermitNO);
        }
    }

    public class InMemoryPermitAgent : IWaterUseAgent
    {
        private List<UnitSystemType> Permits { get; set; }

        public InMemoryPermitAgent() {
           this.Permits = new List<UnitSystemType>()
            { new UnitSystemType() { ID=1,PermitNO="MockTestPermit1" },
                new UnitSystemType() { ID=2, PermitNO= "MockTestPermit2"} };
        
    }

        public Task<T> Add<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
            {
                Permits.Add(item as UnitSystemType);
            }
            return Task.Run(()=> { return item; });
        }

        public Task<IEnumerable<T>> Add<T>(List<T> items) where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
            {
                Permits.AddRange(items.Cast<UnitSystemType>());
            }
            return Task.Run(() => { return Permits.Cast<T>(); });
        }

        public Task Delete<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
            {                
                return Task.Run(()=> { this.Permits.Remove(item as UnitSystemType); });
            }

            else
                throw new Exception("not of correct type");
        }

        public Task<T> Find<T>(int pk) where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
                return Task.Run(()=> { return Permits.Find(i => i.ID == pk) as T; });

            throw new Exception("not of correct type");
        }

        public Manager GetManagerByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Select<T>() where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
                return this.Permits.AsQueryable() as IQueryable<T>;

            throw new Exception("not of correct type");
        }

        public Task<T> Update<T>(int pkId, T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitSystemType))
            {
                var index = this.Permits.FindIndex(x=>x.ID == pkId);
                (item as UnitSystemType).ID = pkId; 
                this.Permits[index] = item as UnitSystemType;
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
}
