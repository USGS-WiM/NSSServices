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

            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //B) Statistic group type: Urban flows(31) = GC1540,GC1539,GC1541,GC1542,GC1543,GC1481,GC1577,GC1576,GC1578,GC1579,GC1614,GC1615,GC1616

            //C) hookup RegressionRegionCoefficient
            //-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC730") up set rc.RegressionRegionID = up.ID Where rc.ID = 1;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC731") up set rc.RegressionRegionID = up.ID Where rc.ID = 2;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC660") up set rc.RegressionRegionID = up.ID Where rc.ID = 3;
            //update RegressionRegionCoefficient rc, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC661") up set rc.RegressionRegionID = up.ID Where rc.ID = 4;

            //SELECT l.ID as RRCoeffID, l.Criteria as RRCoeffCriteria, rr.`Code` as RegressionRegionCode, rr.`Name` as RegressionRegionName FROM RegressionRegionCoefficient l
            //LEFT JOIN RegressionRegion rr on(l.RegressionRegionID = rr.ID);
            //"RRCoeffID","RRCoeffCriteria","RegressionRegionCode","RegressionRegionName"
            //"1","(2875<ELEV) AND (ELEV <3125) AND ((ORREG2=10001) OR (ORREG2=10003))","GC730","Reg_2A_Western_Interior_GE_3000_ft_Cooper"
            //"2","(2875<ELEV) AND (ELEV <3125) AND ((ORREG2=10001) OR (ORREG2=10003))","GC731","Reg_2B_Western_Interior_LT_3000_ft_Cooper"
            //"3","PERENNIAL=0","GC660","Low_Flow_Statewide"
            //"4","PERENNIAL=0","GC661","Low_Flow_Mountainous"

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
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1553") up set l.RegressionRegionID = up.ID Where l.ID = 99;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1554") up set l.RegressionRegionID = up.ID Where l.ID = 100;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1555") up set l.RegressionRegionID = up.ID Where l.ID = 101;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1544") up set l.RegressionRegionID = up.ID Where l.ID = 102;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1545") up set l.RegressionRegionID = up.ID Where l.ID = 103;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1546") up set l.RegressionRegionID = up.ID Where l.ID = 104;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1547") up set l.RegressionRegionID = up.ID Where l.ID = 105;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1548") up set l.RegressionRegionID = up.ID Where l.ID = 106;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1549") up set l.RegressionRegionID = up.ID Where l.ID = 107;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1550") up set l.RegressionRegionID = up.ID Where l.ID = 108;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1551") up set l.RegressionRegionID = up.ID Where l.ID = 109;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1552") up set l.RegressionRegionID = up.ID Where l.ID = 110;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1632") up set l.RegressionRegionID = up.ID Where l.ID = 111;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1435") up set l.RegressionRegionID = up.ID Where l.ID = 112;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1614") up set l.RegressionRegionID = up.ID Where l.ID = 113;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1615") up set l.RegressionRegionID = up.ID Where l.ID = 114;
            //update Limitation l, (Select rr.ID from RegressionRegion rr where rr.`Code` = "GC1616") up set l.RegressionRegionID = up.ID Where l.ID = 115;


            //SELECT l.ID as LimitationID, l.Criteria as LimitationCriteria, rr.`Code` as RegressionRegionCode, rr.`Name` as RegressionRegionName FROM Limitation l LEFT JOIN RegressionRegion rr on(l.RegressionRegionID = rr.ID);
            //"LimitationID","LimitationCriteria","RegressionRegionCode","RegressionRegionName"
            //"1","DRNAREA<=1000.0","GC1656","Peak_Areas_less_than_1000_sqmi_SIR_2016_5024"
            //"2","DRNAREA>1000.0","GC1657","Peak_Areas_greater_than_1000_sqmi_SIR_2016_5024"
            //"3","LOWREG=1438","GC1438","Low_Flow_Region_1_2008_5065"
            //"4","LOWREG=1439","GC1439","Low_Flow_Region_2_2008_5065"
            //"5","LOWREG=1440","GC1440","Low_Flow_Region_3_2008_5065"
            //"10","PZNSSREGNO=1445","GC1445","Pzero_Flow_Region_1_2008_5065"
            //"11","PZNSSREGNO=1446","GC1446","Pzero_Flow_Region_2_2008_5065"
            //"12","PZNSSREGNO=1447","GC1447","Pzero_Flow_Region_3_2008_5065"
            //"13","FD_Region=1623","GC1623","Central_highland_MeanMax_flows_2014_5109"
            //"14","ELEV < 7500.0","GC1619","Peak_Region_2_Colorado_Plateau_2014_5211"
            //"15","ELEV < 7500.0","GC1620","Peak_Region_3_W_Basin_Range_2014_5211"
            //"16","ELEV < 7500.0","GC1621","Peak_Region_4_Central_Highland_2014_5211"
            //"17","ELEV < 7500.0","GC1622","Peak_Region_5_SE_Basin_Range_2014_5211"
            //"18","ELEV>=7500.0","GC1618","Peak_Region_1_High_Elev_2014_5211"
            //"22","(DRNAREA>=1.0)","GC1250","Peak_Southeast_US_over_1_sqmi_2009_5043"
            //"24","(DRNAREA<1.0)","GC1575","Region_5_rural_under_1_sqmi_2014_5030"
            //"30","(DRNAREA<3.0)","GC1539","Region_1_Urban_under_3_sqmi_2014_5030"
            //"32","(DRNAREA>=3.0)","GC1540","Region_1_Urban_over_3_sqmi_2014_5030"
            //"37","(DRNAREA<1.0)","GC1572","Region_1_rural_under_1_sqmi_2014_5030"
            //"38","(DRNAREA<1.0)","GC1573","Region_3_rural_under_1_sqmi_2014_5030"
            //"39","(DRNAREA<1.0)","GC1574","Region_4_rural_under_1_sqmi_2014_5030"
            //"40","DRNAREA <= 2.22 * STRMTOT","GC1561","Peak_Region_1_2013_5086"
            //"41","DRNAREA > 2.22 * STRMTOT","GC1564","Peak_Region_1_DA_only_2015_5055"
            //"42","BFREGNO=1566","GC1566","Bankfull_Central_Till_Plain_Region_2013_5078"
            //"44","BFREGNO=1567","GC1567","Bankfull_South_Hills_and_Lowlands_Region_2013_5078"
            //"46","HIGHREG=1005","GC1005","Region_1_Peak_Flow"
            //"47","HIGHREG=1006","GC1006","Region_2_Peak_Flow"
            //"48","HIGHREG=1007","GC1007","Region_3_Peak_Flow"
            //"49","HIGHREG=1008","GC1008","Region_4_Peak_Flow"
            //"50","HIGHREG=1009","GC1009","Region_5_Peak_Flow"
            //"51","HIGHREG=1010","GC1010","Region_6_Peak_Flow"
            //"52","HIGHREG=1011","GC1011","Region_7_Peak_Flow"
            //"53","HIGHREG=1012","GC1012","Region_8_Peak_Flow"
            //"54","(DRNAREA<3.0)","GC1576","Region_1_Urban_under_3_sqmi_2014_5030"
            //"56","((DRNAREA>=1.0) OR PCTREG2 >0)","GC1254","Peak_Southeast_US_over_1_sqmi_2009_5158"
            //"61","(DRNAREA<1.0)","GC1580","Region_1_rural_under_1_sqmi_2014_5030"
            //"62","(DRNAREA<1.0)","GC1581","Region_3_rural_under_1_sqmi_2014_5030"
            //"63","(DRNAREA<1.0)","GC1582","Region_4_rural_under_1_sqmi_2014_5030"
            //"64","(DRNAREA>=3.0)","GC1577","Region_1_Urban_over_3_sqmi_2014_5030"
            //"70","HIGHREG=1092","GC1092","Peak_2008_5119_NE_Plains_Flood_Region_1"
            //"71","HIGHREG=1093","GC1093","Peak_2008_5119_NW_Plateau_Flood_Region_2"
            //"72","HIGHREG=1094","GC1094","Peak_2008_5119_SE_Mountain_Flood_Region_3"
            //"73","HIGHREG=1095","GC1095","Peak_2008_5119_SE_Plains_Flood_Region_4"
            //"74","HIGHREG=1096","GC1096","Peak_2008_5119_N_Mountain_Flood_Region_5"
            //"75","HIGHREG=1097","GC1097","Peak_2008_5119_Central_MtnValley_Flood_Region_6"
            //"76","HIGHREG=1098","GC1098","Peak_2008_5119_SW_Desert_Flood_Region_7"
            //"77","HIGHREG=1099","GC1099","Peak_2008_5119_SW_Mountain_Flood_Region_8"
            //"78","HIGHREG=1100","GC1100","Peak_2008_5119_NE_Arizona_Flood_Region_9"
            //"79","ELEV<7500","GC660","Low_Flow_Statewide"
            //"80","ELEV>=7500","GC661","Low_Flow_Mountainous"
            //"83","LAT_CENT<=41.2","GC1450","Low_Flow_LatLE_41.2_wri02_4068"
            //"84","LAT_CENT>41.2","GC1449","Low_Flow_LatGT_41.2_wri02_4068"
            //"86","(2875<ELEV) AND ((ORREG2=10001) OR (ORREG2=10003))","GC730","Reg_2A_Western_Interior_GE_3000_ft_Cooper"
            //"87","(ELEV<3125) AND ((ORREG2=10001) OR (ORREG2=10003))","GC731","Reg_2B_Western_Interior_LT_3000_ft_Cooper"
            //"89","(ORREG2=10004) OR (ORREG2=10003) ","GC729","Reg_1_Coastal_Cooper"
            //"97","CONTDA<=30.2","GC346","MultiVariable_Area_3_CDA_LT_30.2"
            //"98","CONTDA>30.2","GC348","MultiVariable_Area_3_CDA_GT_30.2"
            //"99","LARGESTREGION","GC1553","Blue_Ridge_2011_5144"
            //"100","LARGESTREGION","GC1554","Valley_and_Ridge_2011_5144"
            //"101","LARGESTREGION","GC1555","Appalachian_Plateau_2011_5144"
            //"102","LARGESTREGION","GC1544","Coastal_Plain_2011_5143"
            //"103","LARGESTREGION","GC1545","Piedmont_nonMesozoic_2011_5143"
            //"104","LARGESTREGION","GC1546","Blue_Ridge_2011_5143"
            //"105","LARGESTREGION","GC1547","Valley_and_Ridge_2011_5143"
            //"106","LARGESTREGION","GC1548","Appalachian_Plateau_2011_5143"
            //"107","LARGESTREGION","GC1549","Piedmont_Mesozoic_2011_5143"
            //"108","LARGESTREGION","GC1550","Coastal_Plain_2011_5144"
            //"109","LARGESTREGION","GC1551","Piedmont_nonMesozoic_2011_5144"
            //"110","LARGESTREGION","GC1552","Piedmont_Mesozoic_2011_5144"
            //"111","DRNAREA < 12","GC1632","Statewide_Peak_Flow_DA_LT_12sqmi_2015_5049"
            //"112","DRNAREA >=12","GC1435","Statewide_Peak_Flow_Full_GT_12sqmi_WRI_99_4008"
            //"113","LARGESTREGION","GC1614","Peak_Urban01_2014_5090"
            //"114","LARGESTREGION","GC1615","Peak_Urban06_2014_5090"
            //"115","LARGESTREGION","GC1616","Peak_Urban11_2014_5090"
        }
    }
}
