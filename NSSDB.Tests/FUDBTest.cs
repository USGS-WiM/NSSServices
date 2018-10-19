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
        [TestMethod]
        public void VerifyLookups()
        {
            var x = new ForceUpdate();
            x.VerifyLists();
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void ForceUpdate()
        {
            try
            {   
                var x = new ForceUpdate();
                x.Load();
                Assert.IsTrue(true);
                
            }
            catch (Exception e)
            {
                Assert.Fail("error " + e.Message);
            }

            //cleanup
            //A) ACCESS DB has 2 Regions named TN
            //  Change STate code of ID 10047 to 'XX'
            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //B) Change Statistic group type to: Urban flows(31) and Rural flows (32)
            //UPDATE Equation e LEFT JOIN RegressionRegion rr ON(e.RegressionRegionID = rr.ID) SET e.StatisticGroupTypeID = 31 Where rr.`Code` IN('GC1540', 'GC1539', 'GC1541', 'GC1542', 'GC1543', 'GC1481', 'GC1577', 'GC1576', 'GC1578', 'GC1579', 'GC1614', 'GC1615', 'GC1616', 'GC1584', 'GC1583', 'GC1585', 'GC1586', 'GC1251');
            //UPDATE Equation e LEFT JOIN RegressionRegion rr ON(e.RegressionRegionID = rr.ID) SET e.StatisticGroupTypeID = 32 Where rr.`Code` IN('GC1540');

            //C) hookup RegressionRegionCoefficient
            //- +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC730") up set rc.RegressionRegionID = up.ID Where rc.ID = 1;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC731") up set rc.RegressionRegionID = up.ID Where rc.ID = 2;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC660") up set rc.RegressionRegionID = up.ID Where rc.ID = 3;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC661") up set rc.RegressionRegionID = up.ID Where rc.ID = 4;

            //D) hookup Limitations
            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1656") up set l.RegressionRegionID = up.ID Where l.ID = 1;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1657") up set l.RegressionRegionID = up.ID Where l.ID = 2;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1438") up set l.RegressionRegionID = up.ID Where l.ID = 3;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1439") up set l.RegressionRegionID = up.ID Where l.ID = 4;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1440") up set l.RegressionRegionID = up.ID Where l.ID = 5;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1445") up set l.RegressionRegionID = up.ID Where l.ID = 10;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1446") up set l.RegressionRegionID = up.ID Where l.ID = 11;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1447") up set l.RegressionRegionID = up.ID Where l.ID = 12;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1623") up set l.RegressionRegionID = up.ID Where l.ID = 13;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1619") up set l.RegressionRegionID = up.ID Where l.ID = 14;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1620") up set l.RegressionRegionID = up.ID Where l.ID = 15;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1621") up set l.RegressionRegionID = up.ID Where l.ID = 16;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1622") up set l.RegressionRegionID = up.ID Where l.ID = 17;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1618") up set l.RegressionRegionID = up.ID Where l.ID = 18;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1250") up set l.RegressionRegionID = up.ID Where l.ID = 22;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1575") up set l.RegressionRegionID = up.ID Where l.ID = 24;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1539") up set l.RegressionRegionID = up.ID Where l.ID = 30;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1540") up set l.RegressionRegionID = up.ID Where l.ID = 32;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1572") up set l.RegressionRegionID = up.ID Where l.ID = 37;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1573") up set l.RegressionRegionID = up.ID Where l.ID = 38;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1574") up set l.RegressionRegionID = up.ID Where l.ID = 39;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1561") up set l.RegressionRegionID = up.ID Where l.ID = 40;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1564") up set l.RegressionRegionID = up.ID Where l.ID = 41;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1566") up set l.RegressionRegionID = up.ID Where l.ID = 42;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1567") up set l.RegressionRegionID = up.ID Where l.ID = 44;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1005") up set l.RegressionRegionID = up.ID Where l.ID = 46;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1006") up set l.RegressionRegionID = up.ID Where l.ID = 47;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1007") up set l.RegressionRegionID = up.ID Where l.ID = 48;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1008") up set l.RegressionRegionID = up.ID Where l.ID = 49;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1009") up set l.RegressionRegionID = up.ID Where l.ID = 50;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1010") up set l.RegressionRegionID = up.ID Where l.ID = 51;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1011") up set l.RegressionRegionID = up.ID Where l.ID = 52;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1012") up set l.RegressionRegionID = up.ID Where l.ID = 53;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1576") up set l.RegressionRegionID = up.ID Where l.ID = 54;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1254") up set l.RegressionRegionID = up.ID Where l.ID = 56;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1580") up set l.RegressionRegionID = up.ID Where l.ID = 61;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1581") up set l.RegressionRegionID = up.ID Where l.ID = 62;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1582") up set l.RegressionRegionID = up.ID Where l.ID = 63;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1577") up set l.RegressionRegionID = up.ID Where l.ID = 64;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1092") up set l.RegressionRegionID = up.ID Where l.ID = 70;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1093") up set l.RegressionRegionID = up.ID Where l.ID = 71;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1094") up set l.RegressionRegionID = up.ID Where l.ID = 72;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1095") up set l.RegressionRegionID = up.ID Where l.ID = 73;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1096") up set l.RegressionRegionID = up.ID Where l.ID = 74;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1097") up set l.RegressionRegionID = up.ID Where l.ID = 75;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1098") up set l.RegressionRegionID = up.ID Where l.ID = 76;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1099") up set l.RegressionRegionID = up.ID Where l.ID = 77;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1100") up set l.RegressionRegionID = up.ID Where l.ID = 78;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC660") up set l.RegressionRegionID = up.ID Where l.ID = 79;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC661") up set l.RegressionRegionID = up.ID Where l.ID = 80;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1450") up set l.RegressionRegionID = up.ID Where l.ID = 83;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1449") up set l.RegressionRegionID = up.ID Where l.ID = 84;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC730") up set l.RegressionRegionID = up.ID Where l.ID = 86;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC731") up set l.RegressionRegionID = up.ID Where l.ID = 87;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC729") up set l.RegressionRegionID = up.ID Where l.ID = 89;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC346") up set l.RegressionRegionID = up.ID Where l.ID = 97;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC348") up set l.RegressionRegionID = up.ID Where l.ID = 98;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1435") up set l.RegressionRegionID = up.ID Where l.ID = 112;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1632") up set l.RegressionRegionID = up.ID Where l.ID = 111;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1728") up set l.RegressionRegionID = up.ID Where l.ID = 116;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1729") up set l.RegressionRegionID = up.ID Where l.ID = 117;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1730") up set l.RegressionRegionID = up.ID Where l.ID = 118;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1270") up set l.RegressionRegionID = up.ID Where l.ID = 121;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1587") up set l.RegressionRegionID = up.ID Where l.ID = 122;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1588") up set l.RegressionRegionID = up.ID Where l.ID = 123;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1586") up set l.RegressionRegionID = up.ID Where l.ID = 124;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1584") up set l.RegressionRegionID = up.ID Where l.ID = 125;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1583") up set l.RegressionRegionID = up.ID Where l.ID = 126;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1770") up set l.RegressionRegionID = up.ID Where l.ID = 128;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1769") up set l.RegressionRegionID = up.ID Where l.ID = 129;


            //E) ReInsert limitations/Coeff to variable reference
            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(256, 1, 76);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(256, 1, 77);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(256, 1, 78);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(249, 1, 79);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 79);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(249, 1, 80);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 80);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(249, 1, 81);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 81);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(249, 1, 82);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 82);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(159, 18, 83);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(159, 18, 84);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 85);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 85);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 86);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 86);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 87);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 87);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(6, 41, 88);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 88);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 89);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 90);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 91);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES(259, 1, 92);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,93);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (259,1,93);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,94);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (259,1,94);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,95);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (259,1,95);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,96);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (259,1,96);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (7,35,97);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (7,35,98);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,1);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,2);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,3);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,4);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,5);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (257,1,10);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (257,1,11);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (257,1,12);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (262,1,13);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,14);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,15);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,16);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,17);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,18);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,19);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,20);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (6,41,21);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,22);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,23);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,24);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,25);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,26);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,27);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,28);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,29);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,30);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,31);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,32);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,33);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,34);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,35);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,36);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,37);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,38);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,39);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (221,40,40);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,40);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (221,40,41);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,41);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (258,1,42);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (258,1,43);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (258,1,44);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (258,1,45);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,46);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,47);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,48);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,49);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,50);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,51);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,52);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,53);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,54);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,56);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (28,11,56);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,58);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,60);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,61);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,62);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,63);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,64);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,65);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (250,11,65);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,70);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,71);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,72);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,73);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,74);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (256,1,75);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (6,41,1);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (259,1,1);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (6,41,2);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (259,1,2);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (249,1,3);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `RegressionRegionCoefficientID`) VALUES (249,1,4);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,111);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,112);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,121);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,122);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,123);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,124);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,125);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (1,35,126);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,116);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,117);
            //INSERT INTO `Variable` (`VariableTypeID`, `UnitTypeID`, `LimitationID`) VALUES (255,1,118);
            //INSERT INTO `Variable`(`VariableTypeID`, `UnitTypeID`, `LimitationID`, `Comments`) VALUES(87, 11, 128, "PA Statewide_Bankfull_Carbonate_2018_5066 (GC1770) Limitation");
            //INSERT INTO `Variable`(`VariableTypeID`, `UnitTypeID`, `LimitationID`, `Comments`) VALUES(87, 11, 129, "PA Statewide_Bankfull_Carbonate_2018_5066 (GC1769) Limitation");

            //F) Flash Citations to remove #
            //UPDATE Citation Set CitationURL = REPLACE(CitationURL,'#','');

            //G) add missing/default variable units
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(23, 35, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(25, 35, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(43, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(110, 36, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(111, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(112, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(113, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(114, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(119, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(149, 36, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(163, 38, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(164, 45, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(175, 11, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(195, 30, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(196, 30, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(161, 58, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(260, 1, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(261, 1, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(274, 1, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(275, 36, "Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(276, 41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(277, 41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(278, 41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(279, 41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES(280, 11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (281,39,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (282,41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (283,41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (284,41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (285,41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (286,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (287,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (288,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (289,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (290,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (291,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (292,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (293,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (294,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (295,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (296,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (297,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (298,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (299,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (300,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (301,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (302,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (303,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (304,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (305,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (306,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (307,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (308,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (309,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (310,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (311,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (312,42,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (313,42,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (314,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (315,39,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (316,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (317,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (318,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (319,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (320,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (321,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (322,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (324,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (325,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (326,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (327,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (328,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (329,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (330,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (331,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (332,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (333,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (334,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (335,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (336,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (337,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (338,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (339,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (340,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (341,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (342,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (343,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (344,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (345,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (346,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (347,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (348,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (349,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (352,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (353,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (354,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (355,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (356,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (357,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (358,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (359,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (360,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (361,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (362,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (363,37,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (364,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (365,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (366,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (367,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (368,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (369,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (370,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (371,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (372,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (373,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (374,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (375,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (376,35,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (377,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (378,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (379,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (380,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (381,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (382,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (383,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (384,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (385,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (386,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (387,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (388,36,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (389,39,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (390,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (391,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (392,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (393,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (394,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (395,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (396,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (397,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (398,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (399,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (400,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (401,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (402,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (403,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (404,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (405,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (406,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (407,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (409,39,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (410,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (413,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (414,58,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (415,8,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (416,8,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (417,8,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (418,8,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (419,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (420,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (421,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (423,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (424,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (425,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (426,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (427,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (428,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (429,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (430,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (431,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (432,50,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (434,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (435,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (436,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (437,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (438,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (439,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (440,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (441,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (442,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (443,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (444,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (448,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (449,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (453,40,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (454,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (455,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (456,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (457,25,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (458,39,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (459,43,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (460,1,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (461,41,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (462,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (463,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (464,11,"Default unit");
            //INSERT INTO `nss`.`Variable`(`VariableTypeID`, `UnitTypeID`, `Comments`) VALUES (465,11,"Default unit");

        }
    }
}
