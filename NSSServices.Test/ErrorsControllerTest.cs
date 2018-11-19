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
    public class ErrorsControllerTest
    {
        public ErrorsController controller { get; private set; }
        public ErrorsControllerTest() {
            //Arrange
            controller = new ErrorsController(new InMemoryNSSAgent());
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
            var result = Assert.IsType<EnumerableQuery<ErrorType>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("Error Test 2", result.LastOrDefault().Name);
            Assert.Equal("error2", result.LastOrDefault().Code);
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
            var result = Assert.IsType<ErrorType>(okResult.Value);
            
            Assert.Equal("Error Test", result.Name);
            Assert.Equal("error1", result.Code);
        }
    }
}
