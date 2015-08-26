using System;
using System.Data.Common;
using NSSDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NSSDB.Tests
{
    [TestClass]
    public class NSSDBTest
    {
        private string connectionString = "metadata=res://*/NSSEntityModel.csdl|res://*/NSSEntityModel.ssdl|res://*/NSSEntityModel.msl;provider=MySql.Data.MySqlClient;provider connection string=';server=nss.ck2zppz9pgsw.us-east-1.rds.amazonaws.com;user id=**username**;PASSWORD={0};database=nss';";
        private string password = "***REMOVED***";

        [TestMethod]
        public void NSSDBConnectionTest()
        {
            using (nssEntities context = new nssEntities(string.Format(connectionString,password)))
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
            using (nssEntities context = new nssEntities(string.Format(connectionString, password)))
            {
                try
                {
                    var testQuery = context.Citations.ToList();
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
