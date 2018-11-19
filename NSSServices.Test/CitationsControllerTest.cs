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
using NSSAgent.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Http;

namespace NSSServices.Test
{
    public class CitationsControllerTest
    {
        public CitationsController controller { get; private set; }
        public CitationsControllerTest() {
            //Arrange
            controller = new CitationsController(new InMemoryNSSAgent());
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
            var result = Assert.IsType<EnumerableQuery<Citation>>(okResult.Value);

            Assert.Equal(2, result.Count());
            Assert.Equal("test2 Author", result.LastOrDefault().Author);
            Assert.Equal("url2", result.LastOrDefault().CitationURL);
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
            var result = Assert.IsType<Citation>(okResult.Value);
            
            Assert.Equal("test Author", result.Author);
            Assert.Equal("url", result.CitationURL);
        }

        [Fact]
        public async Task Put()
        {
            //Arrange            
            var get = await controller.Get(1);
            var okgetResult = Assert.IsType<OkObjectResult>(get);
            var entity = Assert.IsType<Citation>(okgetResult.Value);

            entity.Author = "editedAuthor";

            //Act
            var response = await controller.Put(1,entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Citation>(okResult.Value);

            Assert.Equal(entity.Author, result.Author);
            Assert.Equal(entity.CitationURL, result.CitationURL);
            Assert.Equal(entity.ID, result.ID);
        }
    }
}
