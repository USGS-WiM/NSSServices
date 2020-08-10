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
using NSSAgent;
using NSSServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSSDB.Resources;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using SharedDB.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;

namespace NSSServices.Test
{
    public class ManagerControllerTest
    {
        public ManagersController controller { get; private set; }
        public ManagerControllerTest() {
            //Arrange
            controller = new ManagersController(new InMemoryNSSAgent());
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = InMemoryPrincipalClaim.adminClaimsPrinciple
                }
            };
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
            var result = Assert.IsType<EnumerableQuery<Manager>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("testAdmin", result.LastOrDefault().Username);
            Assert.Equal("Test", result.LastOrDefault().FirstName);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var id = 2;

            //Act
            var response = await controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Manager>(okResult.Value);

            Assert.Equal("testAdmin", result.Username);
            Assert.Equal("Test", result.FirstName);
        }

        [Fact]
        public async Task GetLoggedInUser()
        {
            //Arrange

            //Act
            var response = await controller.GetLoggedInUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Manager>(okResult.Value);

            Assert.Equal("AdministratorUser", result.Username);
            Assert.Equal("User", result.FirstName);
            Assert.Equal("Administrator", result.LastName);
        }

        [Fact]
        public async Task Post()
        {
            //Arrange
            var entity = new Manager() { FirstName = "NewTest", LastName = "New", ID = 2, Username = "testNew", Email = "New@usgs.gov", RoleID = 1 };


            //Act
            var response = await controller.Post(entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Manager>(okResult.Value);
            

            Assert.Equal("NewTest", result.FirstName);
            Assert.Equal("testNew", result.Username);
        }

        [Fact]
        public async Task Put()
        {
            //Arrange
            var get = await controller.Get(1);
            var okgetResult = Assert.IsType<OkObjectResult>(get);
            var entity = Assert.IsType<Manager>(okgetResult.Value);

            entity.FirstName = "editedName";

            //Act
            var response = await controller.Put(1,entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Manager>(okResult.Value);

            Assert.Equal(entity.FirstName, result.FirstName);
            Assert.Equal(entity.Username, result.Username);
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
            var result = Assert.IsType<EnumerableQuery<Manager>>(okResult.Value);

            Assert.Equal(1, result.Count());
            Assert.Equal("testAdmin", result.LastOrDefault().Username);
            Assert.Equal(2, result.LastOrDefault().ID);
        }
    }

}
