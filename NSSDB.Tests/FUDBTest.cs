using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FU_NSSDB;
using System.Linq;
using NSSDB;

namespace NSSDB.Tests
{
    [TestClass]
    public class FUDBTest
    {
        //[TestMethod]
        public void ForceUpdate()
        {
            #if DEBUG
                var x = new ForceUpdate();
                x.Load();
            Assert.IsTrue(true);
            #endif

            Assert.Inconclusive("used to purge");
            
        }
    }



}
