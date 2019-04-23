using System;
using Xunit;
using FU_NSSDB;

namespace FU_NSSDB.Test
{
    public class ForceUpdateTest
    {
        [Fact]
        public void Test1Async()
        {
            try
            {
                var x = new ForceUpdate("user", "pass", @"db.mdb");
                if (x.VerifyLists())
                {
                    x.Load();
                    x.LoadSqlFiles(@"D:\WiM\GitHub\NSSServices\FU_NSSDB\SQL_files");
                }

            }
            catch (Exception ex)
            {
                Assert.False(true, ex.Message);
            }
        }
    }
}
