/*-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+*/
/*Change Statistic group type to: Urban flows(31) and Rural flows (32)*/
UPDATE "nss"."Equations" e SET "StatisticGroupTypeID" = 31 FROM "nss"."RegressionRegions" rr  WHERE e."RegressionRegionID" = rr."ID" AND rr."Code" IN('GC1540', 'GC1539', 'GC1541', 'GC1542', 'GC1543', 'GC1481', 'GC1577', 'GC1576', 'GC1578', 'GC1579', 'GC1614', 'GC1615', 'GC1616', 'GC1584', 'GC1583', 'GC1585', 'GC1586', 'GC1251');
UPDATE "nss"."Equations" e SET "StatisticGroupTypeID" = 32 FROM "nss"."RegressionRegions" rr  WHERE e."RegressionRegionID" = rr."ID" AND rr."Code" IN('GC1540');


/*Flash Citations to remove #*/
UPDATE "nss"."Citations" Set "CitationURL" = REPLACE("CitationURL",'#','');

/*hookup "nss"."Coefficient"*/
/*-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+*/
INSERT INTO "nss"."Coefficients"("ID","RegressionRegionID", "Criteria", "Description", "Value") VALUES (1, (SELECT "ID" FROM "RegressionRegions" where "Code" = 'GC730'), '(2875<ELEV) AND (ELEV <3125) AND ((ORREG2=10001) OR (ORREG2=10003))', 'OR, Ensures a smooth transition between Flood Regions 2A (GC730) and 2B (GC731), peak discharges for watersheds with mean elevations near 3,000 feet are estimated by a weighted average of peak discharges estimated by prediction equations for both regions.', '(ELEV-2875)/250');
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (6,41,1);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (259,1,1);

INSERT INTO "nss"."Coefficients"("ID","RegressionRegionID", "Criteria", "Description", "Value") VALUES (2, (SELECT "ID" FROM "RegressionRegions" where "Code" = 'GC731'), '(2875<ELEV) AND (ELEV <3125) AND ((ORREG2=10001) OR (ORREG2=10003))', 'OR, Ensures a smooth transition between Flood Regions 2A (GC730) and 2B (GC731), peak discharges for watersheds with mean elevations near 3,000 feet are estimated by a weighted average of peak discharges estimated by prediction equations for both regions.', '(3125-ELEV)/250');
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (6,41,2);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (259,1,2);

INSERT INTO "nss"."Coefficients"("ID","RegressionRegionID", "Criteria", "Description", "Value") VALUES (3, (SELECT "ID" FROM "RegressionRegions" where "Code" = 'GC660'), 'PERENNIAL=0', 'NM, Low flow statewide (GC 660)', '0');
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (249,1,3);

INSERT INTO "nss"."Coefficients"("ID","RegressionRegionID", "Criteria", "Description", "Value") VALUES (4, (SELECT "ID" FROM "RegressionRegions" where "Code" = 'GC661'), 'PERENNIAL=0', 'NM, Low flow mountain (GC 661)', '0');
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "CoefficientID") VALUES (249,1,4);

/*hookup "nss"."Limitation"s*/
/*-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+*/
INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (1, 'DRNAREA<=1000.0', 'AK, Peak flows (GC1656) area less than 1000', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1656'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,1);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (2, 'DRNAREA>1000.0', 'AK, Peak flows (GC1657) area greater than 1000', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1657'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,2);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (3, 'LOWREG=1438', 'AR, Low flows region 1 (GC1438)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1438'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,3);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (4, 'LOWREG=1439', 'AR, Low flows region 2 (GC1439)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1439'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,4);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (5, 'LOWREG=1440', 'AR, Low flows region 3 (GC1440)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1440'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,5);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (10, 'PZNSSREGNO=1445', 'AR, Probability of Zero flows region 1 (GC1445)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1445'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (257,1,10);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (11, 'PZNSSREGNO=1446', 'AR, Probability of Zero flows region 2 (GC1446)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1446'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (257,1,11);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (12, 'PZNSSREGNO=1447', 'AR, Probability of Zero flows region 3 (GC1447)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1447'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (257,1,12);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (13, 'FD_Region=1623', 'AZ,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1623'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (262,1,13);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (14, 'ELEV < 7500.0', 'AZ, Peak flows region 2 (GC1619) elevation less than 7500', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1619'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (6,41,14);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (15, 'ELEV < 7500.0', 'AZ, Peak flows region 3 (GC1620) elevation less than 7500', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1620'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (6,41,15);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (16, 'ELEV < 7500.0', 'AZ, Peak flows region 4 (GC1621) elevation less than 7500', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1621'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (6,41,16);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (17, 'ELEV < 7500.0', 'AZ, Peak flows region 5 (GC1622) elevation less than 7500', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1622'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (6,41,17);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (18, 'ELEV>=7500.0', 'AZ, Peak flows (GC1618) elevation greater than 7500', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1618'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (6,41,18);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (22, '(DRNAREA>=1.0)', 'GA, Southeast US Rural (GC1250) area over 1 sqr mile', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1250'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,22);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (24, '(DRNAREA<1.0)', 'GA, Region 5 Rural flows (GC1575) area under 1 sqr mile', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1575'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,24);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (30, '(DRNAREA<3.0)', 'GA, Region 1 Urban flows (GC1539) area under 3 sqr mil', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1539'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,30);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (32, '(DRNAREA>=3.0)', 'GA, Region 1 Urban (GC 1540) over 3 sqr mil.', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1540'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,32);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (37, '(DRNAREA<1.0)', 'GA, Region 1 Rural flows (GC 1572) area under 1 sqr mile', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1572'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,37);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (38, '(DRNAREA<1.0)', 'GA, Region 3 Rural (GC 1573) area under 1 sqr mil', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1573'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,38);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (39, '(DRNAREA<1.0)', 'GA, Region 4 Rural (GC 1574) area under 1 sqr mil', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1574'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,39);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (40, 'DRNAREA <= 2.22 * STRMTOT', 'IA, Region 1 Area (GC1561) LE 2.22 * streamtotal', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1561'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (221,40,40);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,40);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (41, 'DRNAREA > 2.22 * STRMTOT', 'IA, Region 1 Area (GC 1564) GT 2.22 * streamtotal', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1564'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (221,40,41);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,41);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (42, 'BFREGNO=1566', 'IN, Bankfull_Central_Till_Plain_region (GC 1566)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1566'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (258,1,42);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (44, 'BFREGNO=1567', 'IN, Bankfull_South_Hills_and_lowlands_region (GC1567)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1567'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (258,1,44);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (46, 'HIGHREG=1005', 'IN, Region_1_Peak_Flow (GC 1005)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1005'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,46);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (47, 'HIGHREG=1006', 'IN, Region_2_Peak_Flow (GC 1006)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1006'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,47);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (48, 'HIGHREG=1007', 'IN, Region_3_Peak_Flow (GC 1007)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1007'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,48);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (49, 'HIGHREG=1008', 'IN, Region_4_Peak_Flow (GC 1008)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1008'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,49);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (50, 'HIGHREG=1009', 'IN, Region_5_Peak_Flow (GC 1009)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1009'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,50);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (51, 'HIGHREG=1010', 'IN, Region_6_Peak_Flow (GC 1010)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1010'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,51);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (52, 'HIGHREG=1011', 'IN, Region_7_Peak_Flow (GC 1011)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1011'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,52);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (53, 'HIGHREG=1012', 'IN, Region_8_Peak_Flow (GC 1012)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1012'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,53);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (54, '(DRNAREA<=3.0)', 'NC, Region 1 (GC1576) Urban undr 3 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1576'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,54);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (56, '((DRNAREA>=1.0) OR PCTREG2 >0)', 'NC, SouthEast (GC 1254) Rural all reg. over 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1254'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,56);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (28,11,56);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (61, '(DRNAREA<1.0)', 'NC, Region 1 (GC 1580) Rural under 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1580'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,61);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (62, '(DRNAREA<1.0)', 'NC, Region 3 (GC 1581) Rural under 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1581'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,62);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (63, '(DRNAREA<1.0)', 'NC, Region 4 (GC 1582) Rural under 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1582'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,63);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (64, '(DRNAREA>3.0)', 'NC, Region 1 (GC 1577) Urban over 3 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1577'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,64);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (70, 'HIGHREG=1092', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1092'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,70);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (71, 'HIGHREG=1093', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1093'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,71);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (72, 'HIGHREG=1094', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1094'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,72);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (73, 'HIGHREG=1095', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1095'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,73);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (74, 'HIGHREG=1096', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1096'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,74);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (75, 'HIGHREG=1097', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1097'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (256,1,75);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (76, 'HIGHREG=1098', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1098'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(256, 1, 76);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (77, 'HIGHREG=1099', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1099'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(256, 1, 77);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (78, 'HIGHREG=1100', 'NM,', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1100'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(256, 1, 78);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (79, 'ELEV<7500', 'NM, Low flow statewide (GC 660) ', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC660'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(249, 1, 79);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(6, 41, 79);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (80, 'ELEV>=7500', 'NM, Low flow mountain (GC 661) ', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC661'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(249, 1, 80);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(6, 41, 80);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (83, 'LAT_CENT<=41.2', 'OH, Low flow lat LE 41.2 (GC 1450)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1450'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(159, 18, 83);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (84, 'LAT_CENT>41.2', 'OH, Low flow lat GT 41.2 (GC 1449)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1449'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(159, 18, 84);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (86, '(2875<ELEV) AND ((ORREG2=10001) OR (ORREG2=10003))', 'OR, Elevation GT 2875 to include transition zone (2875<=ELEV<3125) for region Reg_2A_Western_Interior_GE_3000_ft_Cooper', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC730'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(6, 41, 86);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(259, 1, 86);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (87, '(ELEV<3125) AND ((ORREG2=10001) OR (ORREG2=10003))', 'OR, Elevation LT 3125 to include transition zone (2875<=ELEV<3125) for region Reg_2B_Western_Interior_LT_3000_ft_Cooper', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC731'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(6, 41, 87);
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(259, 1, 87);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (89, '(ORREG2=729) OR (ORREG2=10003) ', 'OR, region Reg_1_Coastal_Cooper', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC729'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES(259, 1, 89);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (97, 'CONTDA<=30.2', 'TN, Multivariable are region 3 (GC 346) CDA LE 30.2', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC346'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (7,35,97);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (98, 'CONTDA>30.2', 'TN, Multivariable are region 3 (GC 348) CDA GT 30.2', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC348'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (7,35,98);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (111, 'DRNAREA < 12', 'ME, Peak flows statewide (GC1632)DRNAREA less than 12 sqr mi ', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1632'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,111);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (112, 'DRNAREA >=12', 'ME, Peak flows statewide (GC1435)DRNAREA greater than or equal 12 sqr mi ', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1435'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,112);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (116, 'LOWREG=1728', 'IN, Harmonic_Mean_Northern_Region_2016_5102 (GC 1728)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1728'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,116);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (117, 'LOWREG=1729', 'IN, Harmonic_Mean_Central_Region_2016_5102 (GC 1729)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1729'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,117);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (118, 'LOWREG=1730', 'IN, Harmonic_Mean_Southern_Region_2016_5102 (GC 1730)', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1730'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (255,1,118);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (121, '(DRNAREA>=1.0)', 'SC, SouthEast US (GC1270) over 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1270'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,121);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (122, '(DRNAREA<1.0)', 'SC, Region 1 (GC1587) Rural under 1.0 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1587'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,122);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (123, '(DRNAREA<1.0)', 'SC, Region 3 (GC1588) Rural under 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1588'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,123);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (124, '(DRNAREA<1.0)', 'SC,Region 4 (GC1586) Rural under 1 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1586'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,124);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (125, '(DRNAREA>3.0)', 'SC, Region 1 (GC1584) Urban over 3.0 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1584'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,125);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (126, '(DRNAREA<=3.0)', 'SC, Region 1 (GC1583) Urban under 3.0 sqr mi', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1583'));
INSERT INTO "nss"."Variables" ("VariableTypeID", "UnitTypeID", "LimitationID") VALUES (1,35,126);

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (128, '(CARBON>30)', 'PA, Statewide_Bankfull_Carbonate_2018_5066 (GC1770) Over 30% Carbonate ', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1770'));
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "LimitationID", "Comments") VALUES(87, 11, 128, 'PA Statewide_Bankfull_Carbonate_2018_5066 (GC1770) Limitation');

INSERT INTO "nss"."Limitations"("ID", "Criteria", "Description", "RegressionRegionID") VALUES (129, '(CARBON <=30)', 'PA, Statewide_Bankfull_Noncarbonate_2018_5066 (GC1769) less 30% Carbonate', (Select rr."ID" from "RegressionRegions" rr where rr."Code" = 'GC1769'));
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "LimitationID", "Comments") VALUES(87, 11, 129, 'PA Statewide_Bankfull_Carbonate_2018_5066 (GC1769) Limitation');

/*add missing/default variable units*/
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(23, 35, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(25, 35, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(43, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(110, 36, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(111, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(112, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(113, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(114, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(119, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(149, 36, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(163, 40, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(164, 45, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(175, 11, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(195, 30, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(196, 30, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(161, 58, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(260, 1, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(261, 1, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(274, 1, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(275, 36, 'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(276, 41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(277, 41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(278, 41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(279, 41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES(280, 11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (281,39,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (282,41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (283,41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (284,41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (285,41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (286,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (287,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (288,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (289,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (290,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (291,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (292,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (293,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (294,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (295,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (296,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (297,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (298,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (299,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (300,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (301,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (302,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (303,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (304,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (305,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (306,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (307,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (308,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (309,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (310,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (311,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (312,42,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (313,42,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (314,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (315,39,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (316,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (317,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (318,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (319,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (320,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (321,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (322,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (324,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (325,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (326,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (327,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (328,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (329,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (330,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (331,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (332,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (333,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (334,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (335,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (336,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (337,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (338,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (339,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (340,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (341,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (342,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (343,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (344,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (345,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (346,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (347,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (348,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (349,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (352,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (353,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (354,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (355,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (356,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (357,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (358,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (359,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (360,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (361,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (362,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (363,37,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (364,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (365,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (366,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (367,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (368,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (369,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (370,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (371,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (372,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (373,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (374,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (375,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (376,35,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (377,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (378,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (379,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (380,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (381,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (382,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (383,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (384,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (385,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (386,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (387,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (388,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (389,39,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (390,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (391,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (392,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (393,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (394,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (395,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (396,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (397,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (398,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (399,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (400,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (401,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (402,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (403,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (404,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (405,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (406,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (407,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (409,39,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (410,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (413,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (414,58,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (415,8,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (416,8,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (417,8,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (418,8,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (419,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (420,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (421,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (423,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (424,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (425,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (426,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (427,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (428,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (429,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (430,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (431,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (432,50,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (434,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (435,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (436,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (437,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (438,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (439,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (440,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (441,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (442,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (443,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (444,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (448,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (449,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (453,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (454,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (455,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (456,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (457,25,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (458,39,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (459,43,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (460,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (461,41,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (462,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (463,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (464,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (465,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (629,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (630,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (631,50,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (633,36,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (634,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (635,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (636,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (637,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (638,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (639,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (640,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (641,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (642,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (643,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (644,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (645,11,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (650,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (653,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (654,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (655,1,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (652,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (657,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (658,40,'Default unit');
INSERT INTO "nss"."Variables"("VariableTypeID", "UnitTypeID", "Comments") VALUES (659,40,'Default unit');

/*update regions*/
UPDATE "nss"."Regions" SET "Code" = 'MO_SL' WHERE "Code" = 'SL';
UPDATE "nss"."Regions" SET "Code" = 'RRB' WHERE "Code" = 'RR';
