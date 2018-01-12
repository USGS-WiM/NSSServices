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
    public class CatagoriesTest
    {
        public CategoriesController controller { get; private set; }
        public CatagoriesTest() {
            //Arrange
            controller = new CategoriesController(new InMemoryCatagoriesAgent());
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
            var result = Assert.IsType<EnumerableQuery<UnitConversionFactor>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("MockTestCatagory2", result.LastOrDefault().Name);
            Assert.Equal("test mock catagory 2", result.LastOrDefault().Description);
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
            var result = Assert.IsType<UnitConversionFactor>(okResult.Value);
            
            Assert.Equal("MockTestCatagory1", result.Name);
            Assert.Equal("test mock catagory 1", result.Description);
        }

        [Fact]
        public async Task Post()
        {
            //Arrange
            var entity = new UnitConversionFactor() {Name = "newCatagory", Description = "New mock catagory 3" };
   

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
            var entity = Assert.IsType<UnitConversionFactor>(okgetResult.Value);

            entity.Name = "editedName";

            //Act
            var response = await controller.Post(entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<UnitConversionFactor>(okResult.Value);

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
            Assert.Equal("MockTestCatagory2", result.LastOrDefault().Name);
            Assert.Equal("test mock catagory 2", result.LastOrDefault().Description);
        }
    }

    public class InMemoryCatagoriesAgent : IWaterUseAgent
    {
        private List<UnitConversionFactor> Catagories { get; set; }

        public InMemoryCatagoriesAgent() {
           this.Catagories = new List<UnitConversionFactor>()
            { new UnitConversionFactor() { ID=1,Name= "MockTestCatagory1", Description="test mock catagory 1" },
                new UnitConversionFactor() { ID=2,Name= "MockTestCatagory2", Description="test mock catagory 2" }};
        
    }

        public Task<T> Add<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitConversionFactor))
            {
                Catagories.Add(item as UnitConversionFactor);
            }
            return Task.Run(()=> { return item; });
        }

        public Task<IEnumerable<T>> Add<T>(List<T> items) where T : class, new()
        {
            if (typeof(T) == typeof(UnitConversionFactor))
            {
                Catagories.AddRange(items.Cast<UnitConversionFactor>());
            }
            return Task.Run(() => { return Catagories.Cast<T>(); });
        }

        public Task Delete<T>(T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitConversionFactor))
            {                
                return Task.Run(()=> { this.Catagories.Remove(item as UnitConversionFactor); });
            }

            else
                throw new Exception("not of correct type");
        }

        public Task<T> Find<T>(int pk) where T : class, new()
        {
            if (typeof(T) == typeof(UnitConversionFactor))
                return Task.Run(()=> { return Catagories.Find(i => i.ID == pk) as T; });

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
            if (typeof(T) == typeof(UnitConversionFactor))
                return this.Catagories.AsQueryable() as IQueryable<T>;

            throw new Exception("not of correct type");
        }

        public Task<T> Update<T>(int pkId, T item) where T : class, new()
        {
            if (typeof(T) == typeof(UnitConversionFactor))
            {
                var index = this.Catagories.FindIndex(x=>x.ID == pkId);
                (item as UnitConversionFactor).ID = pkId; 
                this.Catagories[index] = item as UnitConversionFactor;
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
