using System;
using System.Data.Common;
using NSSDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Configuration;

namespace NSSDB.Tests
{
    [TestClass]
    public class NSSDBTest
    {
        private string connectionString = String.Format(@"metadata=res://*/NSSEntityModel.csdl|res://*/NSSEntityModel.ssdl|res://*/NSSEntityModel.msl;provider=MySql.Data.MySqlClient;provider connection string=';server=nsstest.ck2zppz9pgsw.us-east-1.rds.amazonaws.com;user id={0};PASSWORD={1};database=nss';",ConfigurationManager.AppSettings["dbuser"], ConfigurationManager.AppSettings["dbpassword"]);
        
        [TestMethod]
        public void NSSDBConnectionTest()
        {
            using (nssEntities context = new nssEntities(connectionString))
            {
                DbConnection conn = context.Database.Connection;
                try
                {
                    if(! context.Database.Exists()) throw new Exception("db does ont exist");
                    conn.Open();
                    Assert.IsTrue(true);
                    
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();                
                }
                
            }            
        }//end NSSDBConnectionTest

        [TestMethod]
        public void NSSDBQueryTest()
        {
            using (nssEntities context = new nssEntities(connectionString))
            {
                try
                {
                    var testQuery = context.Limitations.ToList();
                    Assert.IsNotNull(testQuery,testQuery.Count.ToString());
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }

            }
        }//end NSSDBConnectionTest
    }
}
