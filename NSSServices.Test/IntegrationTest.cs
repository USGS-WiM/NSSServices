using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using NSSServices;

namespace NSSServices.XUnitTest
{
    public class IntegrationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public IntegrationTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost");
        }

        //[Fact]
        public async Task Roles()
        {
            //Act
            var response = await _client.GetAsync("/roles");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.True(responseString.Contains("name"));
        }
    }
}
