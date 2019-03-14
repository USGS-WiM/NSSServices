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
                var x = new ForceUpdate("testadmin", "euOjmZXwywrsAt5wbXMa", @"D:\WiM\Projects\NSS\DB\StreamStatsDB_2019-02-11.mdb");
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
