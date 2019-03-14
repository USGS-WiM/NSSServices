using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WIM.Security;
using System.Linq;
using NSSDB.Resources;
using System.Collections.Generic;

namespace NSSDB.Test
{
    [TestClass]
    public class NSSDBTest
    {
        private string connectionstring = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build().GetConnectionString("Connection");

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
        [TestMethod]
        public void AddManagerTest()
        {
            using (NSSDBContext context = new NSSDBContext(new DbContextOptionsBuilder<NSSDBContext>().UseNpgsql(this.connectionstring, x => x.UseNetTopologySuite()).Options))
            {
                try
                {
                    var salt = Cryptography.CreateSalt();
                    var password = "";

                    if (String.IsNullOrEmpty(password)) throw new Exception("password cannot be empty");

                    Manager manager = new Manager()
                    {
                        FirstName ="Test",
                        Email = "testManager@usgs.gov",
                        LastName = "Manager", RoleID =2,
                        Username ="testManager", 
                        Password = Cryptography.GenerateSHA256Hash(password, salt),
                        Salt = salt
                        //RegionManagers = new List<RegionManager>() { new RegionManager() { RegionID = 1 } }
                       
                    };
                    context.Managers.Add(manager);
                    context.SaveChanges();

                    Assert.IsTrue(Cryptography.VerifyPassword(password, manager.Salt, manager.Password));

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
