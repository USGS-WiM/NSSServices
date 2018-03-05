using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FU_NSSDB;
using System.Linq;
using NSSDB;

namespace NSSDB.Tests
{
    //[TestClass]
    public class FUDBTest
    {
        //[TestMethod]
        public void VerifyLookups()
        {
            var x = new ForceUpdate();
            x.VerifyLists();;
            Assert.IsTrue(true);
        }


        //[TestMethod]
        public void ForceUpdate()
        {
            #if DEBUG
                var x = new ForceUpdate();
                x.Load();
            Assert.IsTrue(true);
            #endif
            Assert.Inconclusive("used to purge");

            //cleanup
            //A) ACCESS DB has 2 Regions named TN
            //  Change STate code of ID 10047 to 'XX'
            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //B) Change Statistic group type to: Urban flows(31) and Rural flows (32)
            //UPDATE Equation e LEFT JOIN RegressionRegion rr ON (e.RegressionRegionID = rr.ID) SET e.StatisticGroupTypeID = 31 Where rr.`Code` IN ('GC1540','GC1539','GC1541','GC1542','GC1543','GC1481','GC1577','GC1576','GC1578','GC1579','GC1614','GC1615','GC1616','GC1584','GC1583','GC1585','GC1586','GC1251');
            //UPDATE Equation e LEFT JOIN RegressionRegion rr ON (e.RegressionRegionID = rr.ID) SET e.StatisticGroupTypeID = 32 Where rr.`Code` IN ('GC1540');

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

            //F) Flash Citations to remove #
            //UPDATE Citation Set CitationURL = REPLACE(CitationURL,'#','');

            //G) add missing/default variable units
            //todo

        }
    }
}
