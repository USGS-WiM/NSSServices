using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace NSSDB.Test
{
    [TestClass]
    public class NSSDBTest
    {
        private string connectionstring = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build().GetConnectionString("nssConnection");

        [TestMethod]
        public void ConnectionTest()
        {
            using (NSSDBContext context = new NSSDBContext(new DbContextOptionsBuilder<NSSDBContext>().UseNpgsql(this.connectionstring,x=>x.UseNetTopologySuite()).Options))
            {
                try
                {
                    if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()) throw new Exception("db does ont exist");
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
        [TestMethod]
        public void QueryTest()
        {
            using (NSSDBContext context = new NSSDBContext(new DbContextOptionsBuilder<NSSDBContext>().UseNpgsql(this.connectionstring, x => x.UseNetTopologySuite()).Options))
            {
                try
                {
                    var testQuery = context.Equations.Include("EquationErrors.ErrorType").Include("EquationUnitTypes.UnitType").ToList();
                    Assert.IsNotNull(testQuery, testQuery.Count.ToString());
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
                finally
                {
                }

            }
        }
    }
}
