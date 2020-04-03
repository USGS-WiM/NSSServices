using System;
using Xunit;
using FU_NSSDB;
using Microsoft.Extensions.Configuration;

namespace FU_NSSDB.Test
{
    public class ForceUpdateTest
    {
        IConfiguration Configuration { get; set; }
        public ForceUpdateTest()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<ForceUpdateTest>();
            Configuration = builder.Build();
        }

        [Fact]
        public void Test1Async()
        {
            try
            {
                var username = Configuration["dbuser"];
                var password = Configuration["dbpassword"];

                var x = new ForceUpdate(username, password, @"C:\Users\jknewson\Documents\WIM\Projects\StreamStatsDB_2020-03-27.mdb");
                if (x.VerifyLists())
                {
                    x.Load();
                    Assert.True(true, "Loading passed");
                    x.LoadSqlFiles(@"D:\WiM\GitHub\NSSServices\FU_NSSDB\SQL_files");
                    Assert.True(true, "Files passed");

                }

            }
            catch (Exception ex)
            {
                Assert.False(true, ex.Message);
            }
        }
    }
}
