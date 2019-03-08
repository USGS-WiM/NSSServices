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
using System.IO;
using Newtonsoft.Json;

namespace NSSServices.Test
{
    public class CitationsControllerTest
    {
        private IEnumerable<Citation> AssertItems;
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

            var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data");
            this.AssertItems = JsonConvert.DeserializeObject<IEnumerable<Citation>>(System.IO.File.ReadAllText(Path.Combine(path, "citations.json")));

        }
        [Fact]
        public async Task GetAll()
        {   
            //Act
            var response = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<EnumerableQuery<Citation>>(okResult.Value);

            Assert.Equal(AssertItems.Count(), result.Count());
            Assert.Equal(AssertItems.LastOrDefault().Author, result.LastOrDefault().Author);
            Assert.Equal(AssertItems.LastOrDefault().CitationURL, result.LastOrDefault().CitationURL);
        }
        [Fact]
        public async Task GetAllByQuery()
        {
            //Act
            var regressionregions="21,22,23,24,25,26,27,28";
            var response = await controller.Get(regressionRegions:regressionregions );

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<EnumerableQuery<Citation>>(okResult.Value);

            Assert.Equal(AssertItems.Count(), result.Count());
            Assert.Equal(AssertItems.LastOrDefault().Author, result.LastOrDefault().Author);
            Assert.Equal(AssertItems.LastOrDefault().CitationURL, result.LastOrDefault().CitationURL);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var assertItem = AssertItems.FirstOrDefault();
            var id = assertItem.ID;

            //Act
            var response = await controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Citation>(okResult.Value);
            Assert.Equal(assertItem.Author, result.Author);
        }

        [Fact]
        public async Task Put()
        {
            //Arrange 
            var assertItem = AssertItems.FirstOrDefault();

            var get = await controller.Get(assertItem.ID);
            var okgetResult = Assert.IsType<OkObjectResult>(get);
            var entity = Assert.IsType<Citation>(okgetResult.Value);

            entity.Author = "editedAuthor";

            //Act
            var response = await controller.Put(assertItem.ID, entity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var result = Assert.IsType<Citation>(okResult.Value);

            Assert.Equal(entity.Author, result.Author);
            Assert.Equal(entity.CitationURL, result.CitationURL);
            Assert.Equal(entity.ID, result.ID);
        }
    }
}
