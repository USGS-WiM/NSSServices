CREATE SCHEMA IF NOT EXISTS shared;
CREATE TABLE IF NOT EXISTS shared."_EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK__EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS shared;

CREATE TABLE shared."ErrorType" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    CONSTRAINT "PK_ErrorType" PRIMARY KEY ("ID")
);

CREATE TABLE shared."RegressionType" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "Description" text NULL,
    CONSTRAINT "PK_RegressionType" PRIMARY KEY ("ID")
);

CREATE TABLE shared."StatisticGroupType" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    CONSTRAINT "PK_StatisticGroupType" PRIMARY KEY ("ID")
);

CREATE TABLE shared."UnitSystemType" (
    "ID" serial NOT NULL,
    "UnitSystem" text NOT NULL,
    CONSTRAINT "PK_UnitSystemType" PRIMARY KEY ("ID")
);

CREATE TABLE shared."VariableType" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "Description" text NULL,
    CONSTRAINT "PK_VariableType" PRIMARY KEY ("ID")
);

CREATE TABLE shared."UnitType" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Abbreviation" text NOT NULL,
    "UnitSystemTypeID" integer NOT NULL,
    CONSTRAINT "PK_UnitType" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_UnitType_UnitSystemType_UnitSystemTypeID" FOREIGN KEY ("UnitSystemTypeID") REFERENCES shared."UnitSystemType" ("ID") ON DELETE RESTRICT
);

CREATE TABLE shared."UnitConversionFactor" (
    "ID" serial NOT NULL,
    "UnitTypeInID" integer NOT NULL,
    "UnitTypeOutID" integer NOT NULL,
    "Factor" double precision NOT NULL,
    CONSTRAINT "PK_UnitConversionFactor" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_UnitConversionFactor_UnitType_UnitTypeInID" FOREIGN KEY ("UnitTypeInID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT,
    CONSTRAINT "FK_UnitConversionFactor_UnitType_UnitTypeOutID" FOREIGN KEY ("UnitTypeOutID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT
);

INSERT INTO shared."ErrorType" ("ID", "Code", "Name")
VALUES (1, 'SE', 'Average standard error (of either estimate or prediction)');
INSERT INTO shared."ErrorType" ("ID", "Code", "Name")
VALUES (3, 'SEp', 'Average standard error of prediction');
INSERT INTO shared."ErrorType" ("ID", "Code", "Name")
VALUES (4, 'PC', 'Percent Correct');

INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (975, 'JANMINMON', 'Minimum of  January monthly mean flows', 'Min Jan Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (974, 'DECMAXMON', 'Maximum of  December monthly mean flows', 'Max Dec Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (973, 'NOVMAXMON', 'Maximum of  November monthly mean flows', 'Max Nov Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (972, 'OCTMAXMON', 'Maximum of  October monthly mean flows', 'Max Oct Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (971, 'SEPMAXMON', 'Maximum of  September monthly mean flows', 'Max Sep Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (970, 'AUGMAXMON', 'Maximum of  August monthly mean flows', 'Max Aug Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (969, 'JULMAXMON', 'Maximum of  July monthly mean flows', 'Max Jul Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (968, 'JUNMAXMON', 'Maximum of  June monthly mean flows', 'Max Jun Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (967, 'MAYMAXMON', 'Maximum of  May monthly mean flows', 'Max May Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (966, 'APRMAXMON', 'Maximum of  April monthly mean flows', 'Max Apr Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (965, 'MARMAXMON', 'Maximum of  March monthly mean flows', 'Max Mar Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (964, 'FEBMAXMON', 'Maximum of  February monthly mean flows', 'Max Feb Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (963, 'JANMAXMON', 'Maximum of January monthly mean flows', 'Max Jan Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (962, 'DECMEDMON', 'Median of December monthly mean flows', 'Med Dec Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (961, 'NOVMEDMON', 'Median of November monthly mean flows', 'Med Nov Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (960, 'OCTMEDMON', 'Median of October monthly mean flows', 'Med Oct Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (959, 'SEPMEDMON', 'Median of September monthly mean flows', 'Med Sep Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (958, 'AUGMEDMON', 'Median of August monthly mean flows', 'Med Aug Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (976, 'FEBMINMON', 'Minimum of  February monthly mean flows', 'Min Feb Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (978, 'APRMINMON', 'Minimum of  April monthly mean flows', 'Min Apr Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (980, 'JUNMINMON', 'Minimum of  June monthly mean flows', 'Min Jun Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (957, 'JULMEDMON', 'Median of July monthly mean flows', 'Med Jul Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (999, 'M10D2Y1103', 'November to March 10-day low flow that occurs on average once in 2 years', 'Nov to Mar 10 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (998, 'M7D20Y1103', 'November to March 7-day low flow that occurs on average once in 20 years', 'Nov to Mar 7 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (997, 'M7D10Y1103', 'November to March 7-day low flow that occurs on average once in 10 years', 'Nov to Mar 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (996, 'M7D5Y1103', 'November to March 7-day low flow that occurs on average once in 5 years', 'Nov to Mar 7 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (995, 'M7D2Y1103', 'November to March 7-day low flow that occurs on average once in 2 years', 'Nov to Mar 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (994, 'M3D20Y1103', 'November to March 3-day low flow that occurs on average once in 20 years', 'Nov to Mar 3 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (993, 'M3D10Y1103', 'November to March 3-day low flow that occurs on average once in 10 years', 'Nov to Mar 3 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (992, 'M3D5Y1103', 'November to March 3-day low flow that occurs on average once in 5 years', 'Nov to Mar 3 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (991, 'M3D2Y1103', 'November to March 3-day low flow that occurs on average once in 2 years', 'Nov to Mar 3 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (990, 'M1D20Y1103', 'November to March 1-day low flow that occurs on average once in 20 years', 'Nov to Mar 1 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (989, 'M1D10Y1103', 'November to March 1-day low flow that occurs on average once in 10 years', 'Nov to Mar 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (988, 'M1D5Y1103', 'November to March 1-day low flow that occurs on average once in 5 years', 'Nov to Mar 1 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (987, 'M1D2Y1103', 'November to March 1-day low flow that occurs on average once in 2 years', 'Nov to Mar 1 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (986, 'DECMINMON', 'Minimum of  December monthly mean flows', 'Min Dec Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (985, 'NOVMINMON', 'Minimum of  November monthly mean flows', 'Min Nov Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (984, 'OCTMINMON', 'Minimum of  October monthly mean flows', 'Min Oct Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (983, 'SEPMINMON', 'Minimum of  September monthly mean flows', 'Min Sep Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (982, 'AUGMINMON', 'Minimum of  August monthly mean flows', 'Min Aug Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (981, 'JULMINMON', 'Minimum of  July monthly mean flows', 'Min Jul Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (979, 'MAYMINMON', 'Minimum of  May monthly mean flows', 'Min May Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (956, 'JUNMEDMON', 'Median of June monthly mean flows', 'Med Jun Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (954, 'APRMEDMON', 'Median of April monthly mean flows', 'Med Apr Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1000, 'M10D5Y1103', 'November to March 10-day low flow that occurs on average once in 5 years', 'Nov to Mar 10 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (930, 'D25_07_10', 'July to October flow exceeded 25 percent of the time.', '25 Percent Duration July to October');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (929, 'D99_03_04', 'Streamflow exceeded 99 percent of the time during March to April', '99 Percent Duration March to April');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (928, 'D95_03_04', 'Streamflow exceeded 95 percent of the time during March to April', '95 Percent Duration March to April');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (927, 'D75_03_04', 'Streamflow exceeded 75 percent of the time during March to April', '75 Percent Duration March to April');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (926, 'D50_03_04', 'Streamflow exceeded 50 percent of the time during March to April', '50 Percent Duration March to April');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (925, 'D25_03_04', 'Streamflow exceeded 25 percent of the time during March to April', '25 Percent Duration March to April');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (924, 'D99_12_02', 'Streamflow exceeded 99 percent of the time during December to February', '99 Percent Duration December to February');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (923, 'D25_12_02', 'Streamflow exceeded 25 percent of the time during December to February', '25 Percent Duration December to February');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (922, 'BFAREA', 'Bankfull area', 'Bankfull Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (921, 'BFDPTH', 'Bankfull depth', 'Bankfull Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (920, 'BFWDTH', 'Bankfull width', 'Bankfull Width');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (919, 'BFFLOW', 'Bankfull streamflow', 'Bankfull Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (918, 'D95WSP', 'November 1 to May 31 flow exceeded 95 percent of the time', 'Nov to May 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (917, 'D90WSP', 'November 1 to May 31 flow exceeded 90 percent of the time', 'Nov to May 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (916, 'D80WSP', 'November 1 to May 31 flow exceeded 80 percent of the time', 'Nov to May 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (915, 'D50WSP', 'November 1 to May 31 flow exceeded 50 percent of the time', 'Nov to May 50 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (914, 'D20WSP', 'November 1 to May 31 flow exceeded 20 percent of the time', 'Nov to May 20 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (913, 'D50SUM', 'June 1 to October 31 flow exceeded 50 percent of the time', 'Jun to Oct 50 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (912, 'D20SUM', 'June 1 to October 31 flow exceeded 20 percent of the time', 'Jun to Oct 20 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (931, 'D50_07_10', 'July to October flow exceeded 50 percent of the time.', '50 Percent Duration July to October');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (955, 'MAYMEDMON', 'Median of May monthly mean flows', 'Med May Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (932, 'D75_07_10', 'July to October flow exceeded 75 percent of the time.', '75 Percent Duration July to October');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (934, 'D99_07_10', 'July to October flow exceeded 99 percent of the time.', '99 Percent Duration July to October');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (953, 'MARMEDMON', 'Median of March monthly mean flows', 'Med Mar Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (952, 'FEBMEDMON', 'Median of February monthly mean flows', 'Med Feb Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (951, 'JANMEDMON', 'Median of January monthly mean flows', 'Med Jan Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (950, 'DECPAR', 'Average percent of annual runoff occuring in December', 'Dec Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (949, 'NOVPAR', 'Average percent of annual runoff occuring in November', 'Nov Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (948, 'OCTPAR', 'Average percent of annual runoff occuring in October', 'Oct Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (947, 'SEPPAR', 'Average percent of annual runoff occuring in September', 'Sep Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (946, 'AUGPAR', 'Average percent of annual runoff occuring in August', 'Aug Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (945, 'JULPAR', 'Average percent of annual runoff occuring in July', 'Jul Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (944, 'JUNPAR', 'Average percent of annual runoff occuring in June', 'Jun Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (943, 'MAYPAR', 'Average percent of annual runoff occuring in May', 'May Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (942, 'APRPAR', 'Average percent of annual runoff occuring in April', 'Apr Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (941, 'MARPAR', 'Average percent of annual runoff occuring in March', 'Mar Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (940, 'FEBPAR', 'Average percent of annual runoff occuring in February', 'Feb Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (939, 'JANPAR', 'Average percent of annual runoff occuring in January', 'Jan Percent Annual Runoff');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (938, 'M10D20Y', '10-Day mean low-flow that occurs on average once in 20 years', '10 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (937, 'M10D10Y', '10-Day mean low-flow that occurs on average once in 10 years', '10 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (936, 'M10D5Y', '10-Day mean low-flow that occurs on average once in 5 years', '10 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (935, 'M10D2Y', '10-Day mean low-flow that occurs on average once in 2 years', '10 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (933, 'D80_07_10', 'July to October flow exceeded 80 percent of the time.', '80 Percent Duration July to October');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1001, 'M10D10Y113', 'November to March 10-day low flow that occurs on average once in 10 years', 'Nov to Mar 10 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1002, 'M10D20Y113', 'November to March 10-day low flow that occurs on average once in 20 years', 'Nov to Mar 10 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1003, 'M30D2Y1103', 'November to March 30-day low flow that occurs on average once in 2 years', 'Nov to Mar 30 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1067, 'M1D10YPA', '1-Day mean low-flow per unit of drainage area that occurs on average once in 10 years', '1 Day 10 Year Low Flow Per Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1066, 'M7D10YPA', '7-Day mean low-flow per unit of drainage area that occurs on average once in 10 years', '7 Day 10 Year Low Flow Per Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1065, 'PK1_25W', 'Weighted maximum instantaneous flow that occurs on average once in 1.25 years', 'Weighted 1 25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1064, 'M7D10Y0409', 'April to September 7-day mean low-flow that occurs on average once in 10 years', 'Apr to Sep 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1063, 'M7D10Y1003', 'October to March 7-day mean low-flow that occurs on average once in 10 years', 'Oct to Mar 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1062, 'PROB_DUR', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow durations');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1061, 'PROB7D11', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 7 day Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1060, 'PROB7D1112', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 7 day Nov to Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1068, 'M30D10YPA', '30-Day mean low-flow per unit of drainage area that occurs on average once in 10 years', '30 Day 10 Year Low Flow Per Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1059, 'PROB7D1104', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 7 day Nov to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1057, 'M15D10Y', '15-Day mean low-flow that occurs on average once in 10 years', '15 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1056, 'M60D20Y610', 'June to October 60-day low flow that occurs on average once in 20 years', 'Jun to Oct 60 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1055, 'M60D10Y610', 'June to October 60-day low flow that occurs on average once in 10 years', 'Jun to Oct 60 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1054, 'M60D5Y0610', 'June to October 60-day low flow that occurs on average once in 5 years', 'Jun to Oct 60 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1053, 'M60D2Y0610', 'June to October 60-day low flow that occurs on average once in 2 years', 'Jun to Oct 60 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1052, 'M30D20Y610', 'June to October 30-day low flow that occurs on average once in 20 years', 'Jun to Oct 30 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1051, 'M30D10Y610', 'June to October 30-day low flow that occurs on average once in 10 years', 'Jun to Oct 30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1050, 'M30D5Y0610', 'June to October 30-day low flow that occurs on average once in 5 years', 'Jun to Oct 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1058, 'M15D2Y', '15-Day mean low-flow that occurs on average once in 2 years', '15 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1069, 'M90D10YPA', '90-Day mean low-flow per unit of drainage area that occurs on average once in 10 years', '90 Day 10 Year Low Flow Per Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1070, 'D80PA', 'Streamflow per unit of drainage area exceeded 80 percent of the time', '80 Percent Duration Per Area');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1071, 'RIBANK', 'Average number of years between occurrences of the bankfull discharge', 'Recurrence Interval of Bankfull Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1090, 'D1C', 'Estimate of regulated streamflow exceeded 1 percent of the time', 'Controlled 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1089, 'BF50YRC', 'Estimate of regulated base flow component of streamflow that occurs on average once in 50 years', 'Controlled Base Flow 50 Year Rec Interv');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1088, 'BF25YRC', 'Estimate of regulated base flow component of streamflow that occurs on average once in 25 years', 'Controlled Base Flow 25 Year Rec Interv');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1087, 'BF10YRC', 'Estimate of regulated base flow component of streamflow that occurs on average once in 10 years', 'Controlled Base Flow 10 Year Rec Interv');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1086, 'M90D10YC', 'Estimate of regulated 90-Day mean low-flow that occurs on average once in 10 years', 'Controlled 90 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1085, 'M30D10YC', 'Estimate of regulated 30-Day mean low-flow that occurs on average once in 10 years', 'Controlled 30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1084, 'M30D2YC', 'Estimate of regulated 30-Day mean low-flow that occurs on average once in 2 years', 'Controlled 30 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1083, 'M7D10YC', 'Estimate of regulated 7-Day mean low-flow that occurs on average once in 10 years', 'Controlled 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1082, 'M7D2YC', 'Estimate of regulated 7-Day mean low-flow that occurs on average once in 2 years', 'Controlled 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1081, 'M1D10YC', 'Estimate of regulated 1-Day mean low-flow that occurs on average once in 10 years', 'Controlled 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1080, 'ARTAU_SPR', 'Tau, March through April as defined in SIR 2008-5065', 'Tau Mar Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1079, 'ARTAU_WIN', 'Tau, November through December as defined in SIR 2008-5065', 'Tau Nov Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1078, 'ARTAU_ANN', 'Tau, annual as defined in SIR 2008-5065', 'Tau Annual');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1077, 'PORHISPK', 'Period of record of historic peaks in water years', 'Period of record of historic peaks');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1076, 'MSEREGSKEW', 'Mean squared error of regional skew coefficient', 'Regional skew mean squared error');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1075, 'REG_SKEW', 'Regional skew coefficient', 'Regional skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1074, 'MSE_SKEW', 'Mean squared error of WRC skew coefficient', 'Mean square error WRC skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1073, 'KY_CUTCO', 'Coefficient to adjust to optimal cutpoint probability in PZero equations for KY in SIR 2010-5217', 'KY Optimal Cutpoint Coefficient');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1072, 'ROSCLASS', 'Stream classification as defined by Rosgen, D.L., 1994', 'Rosgen Stream Class');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1049, 'M30D2Y0610', 'June to October 30-day low flow that occurs on average once in 2 years', 'Jun to Oct 30 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1048, 'M10D20Y610', 'June to October 10-day low flow that occurs on average once in 20 years', 'Jun to Oct 10 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1047, 'M10D10Y610', 'June to October 10-day low flow that occurs on average once in 10 years', 'Jun to Oct 10 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1046, 'M10D5Y0610', 'June to October 10-day low flow that occurs on average once in 5 years', 'Jun to Oct 10 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1022, 'M7D20Y0405', 'April to May 7-day low flow that occurs on average once in 20 years', 'Apr to May 7 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1021, 'M7D10Y0405', 'April to May 7-day low flow that occurs on average once in 10 years', 'Apr to May 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1020, 'M7D5Y0405', 'April to May 7-day low flow that occurs on average once in 5 years', 'Apr to May 7 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1019, 'M7D2Y0405', 'April to May 7-day low flow that occurs on average once in 2 years', 'Apr to May 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1018, 'M3D20Y0405', 'April to May 3-day low flow that occurs on average once in 20 years', 'Apr to May 3 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1017, 'M3D10Y0405', 'April to May 3-day low flow that occurs on average once in 10 years', 'Apr to May 3 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1016, 'M3D5Y0405', 'April to May 3-day low flow that occurs on average once in 5 years', 'Apr to May 3 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1015, 'M3D2Y0405', 'April to May 3-day low flow that occurs on average once in 2 years', 'Apr to May 3 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1014, 'M1D20Y0405', 'April to May 1-day low flow that occurs on average once in 20 years', 'Apr to May 1 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1013, 'M1D10Y0405', 'April to May 1-day low flow that occurs on average once in 10 years', 'Apr to May 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1012, 'M1D5Y0405', 'April to May 1-day low flow that occurs on average once in 5 years', 'Apr to May 1 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1011, 'M1D2Y0405', 'April to May 1-day low flow that occurs on average once in 2 years', 'Apr to May 1 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1010, 'M60D20Y113', 'November to March 60-day low flow that occurs on average once in 20 years', 'Nov to Mar 60 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1009, 'M60D10Y113', 'November to March 60-day low flow that occurs on average once in 10 years', 'Nov to Mar 60 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1008, 'M60D5Y1103', 'November to March 60-day low flow that occurs on average once in 5 years', 'Nov to Mar 60 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1007, 'M60D2Y1103', 'November to March 60-day low flow that occurs on average once in 2 years', 'Nov to Mar 60 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1006, 'M30D20Y113', 'November to March 30-day low flow that occurs on average once in 20 years', 'Nov to Mar 30 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1005, 'M30D10Y113', 'November to March 30-day low flow that occurs on average once in 10 years', 'Nov to Mar 30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1004, 'M30D5Y1103', 'November to March 30-day low flow that occurs on average once in 5 years', 'Nov to Mar 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1023, 'M10D2Y0405', 'April to May 10-day low flow that occurs on average once in 2 years', 'Apr to May 10 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (911, 'KYVARINDEX', 'Variability Index as defined by KY WRIR 1991-4097', 'KY Variability Index');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1024, 'M10D5Y0405', 'April to May 10-day low flow that occurs on average once in 5 years', 'Apr to May 10 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1026, 'M10D20Y45', 'April to May 10-day low flow that occurs on average once in 20 years', 'Apr to May 10 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1045, 'M10D2Y0610', 'June to October 10-day low flow that occurs on average once in 2 years', 'Jun to Oct 10 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1044, 'M7D20Y0610', 'June to October 7-day low flow that occurs on average once in 20 years', 'Jun to Oct 7 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1043, 'M7D5Y0610', 'June to October 7-day low flow that occurs on average once in 5 years', 'Jun to Oct 7 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1042, 'M3D20Y0610', 'June to October 3-day low flow that occurs on average once in 20 years', 'Jun to Oct 3 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1041, 'M3D10Y0610', 'June to October 3-day low flow that occurs on average once in 10 years', 'Jun to Oct 3 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1040, 'M3D5Y0610', 'June to October 3-day low flow that occurs on average once in 5 years', 'Jun to Oct 3 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1039, 'M3D2Y0610', 'June to October 3-day low flow that occurs on average once in 2 years', 'Jun to Oct 3 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1038, 'M1D20Y0610', 'June to October 1-day low flow that occurs on average once in 20 years', 'Jun to Oct 1 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1037, 'M1D10Y0610', 'June to October 1-day low flow that occurs on average once in 10 years', 'Jun to Oct 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1036, 'M1D5Y0610', 'June to October 1-day low flow that occurs on average once in 5 years', 'Jun to Oct 1 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1035, 'M1D2Y0610', 'June to October 1-day low flow that occurs on average once in 2 years', 'Jun to Oct 1 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1034, 'M60D20Y45', 'April to May 60-day low flow that occurs on average once in 20 years', 'Apr to May 60 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1033, 'M60D10Y45', 'April to May 60-day low flow that occurs on average once in 10 years', 'Apr to May 60 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1032, 'M60D5Y0405', 'April to May 60-day low flow that occurs on average once in 5 years', 'Apr to May 60 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1031, 'M60D2Y0405', 'April to May 60-day low flow that occurs on average once in 2 years', 'Apr to May 60 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1030, 'M30D20Y45', 'April to May 30-day low flow that occurs on average once in 20 years', 'Apr to May 30 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1029, 'M30D10Y45', 'April to May 30-day low flow that occurs on average once in 10 years', 'Apr to May 30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1028, 'M30D5Y0405', 'April to May 30-day low flow that occurs on average once in 5 years', 'Apr to May 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1027, 'M30D2Y0405', 'April to May 30-day low flow that occurs on average once in 2 years', 'Apr to May 30 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1025, 'M10D10Y45', 'April to May 10-day low flow that occurs on average once in 10 years', 'Apr to May 10 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (910, 'B4D3Y', 'Biologically based minimum average streamflow for 4 consecutive days expected on average once in 3 years', '4 Day 3 Year Bio Based Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (909, 'B1D3Y', 'Biologically based minimum average streamflow for 1 day expected on average once in 3 years', '1 Day 3 Year Bio Based Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (908, 'BASELNDPTH', 'Depth of the 50-percent flow duration in feet above the zero-flow point.', 'Baseline Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (793, 'FPS80', 'Streamflow not exceeded 80 percent of the time', '80th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (792, 'FPS79', 'Streamflow not exceeded 79 percent of the time', '79th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (791, 'FPS78', 'Streamflow not exceeded 78 percent of the time', '78th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (790, 'FPS77', 'Streamflow not exceeded 77 percent of the time', '77th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (789, 'FPS76', 'Streamflow not exceeded 76 percent of the time', '76th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (788, 'FPS75', 'Streamflow not exceeded 75 percent of the time', '75th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (787, 'FPS74', 'Streamflow not exceeded 74 percent of the time', '74th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (786, 'FPS73', 'Streamflow not exceeded 73 percent of the time', '73rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (794, 'FPS81', 'Streamflow not exceeded 81 percent of the time.', '81st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (785, 'FPS72', 'Streamflow not exceeded 72 percent of the time', '72nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (783, 'FPS70', 'Streamflow not exceeded 70 percent of the time', '70th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (782, 'FPS69', 'Streamflow not exceeded 69 percent of the time', '69th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (781, 'FPS68', 'Streamflow not exceeded 68 percent of the time', '68th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (780, 'FPS67`', 'Streamflow not exceeded 67 percent of the time', '67th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (779, 'FPS66', 'Streamflow not exceeded 66 percent of the time', '66th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (778, 'FPS65', 'Streamflow not exceeded 65 percent of the time.', '65th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (777, 'FPS64', 'Streamflow not exceeded 64 percent of the time.', '64th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (776, 'FPS63', 'Streamflow not exceeded 63 percent of the time', '63rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (784, 'FPS71', 'Streamflow not exceeded 71 percent of the time', '71st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (795, 'FPS82', 'Streamflow not exceeded 82 percent of the time', '82nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (796, 'FPS83', 'Streamflow not exceeded 83 percent of the time.', '83rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (797, 'FPS84', 'Streamflow not exceeded 84 percent of the time', '84th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (816, 'D9', 'Streamflow exceeded 9 percent of the time', '9 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (815, 'D8', 'Streamflow exceeded 8 percent of the time', '8 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (814, 'D6', 'Streamflow exceeded 6 percent of the time', '6 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (813, 'D4', 'Streamflow exceeded 4 percent of the time', '4 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (812, 'FPS99', 'Streamflow not exceeded 99 percent of the time', '99th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (811, 'FPS98', 'Streamflow not exceeded 98 percent of the time.', '98th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (810, 'FPS97', 'Streamflow not exceeded 97 percent of the time.', '97th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (809, 'FPS96', 'Streamflow not exceeded 96 percent of the time', '96th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (808, 'FPS95', 'Streamflow not exceeded 95 percent of the time.', '95th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (807, 'FPS94', 'Streamflow not exceeded 94 percent of the time.', '94th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (806, 'FSP93', 'Streamflow not exceeded 93 percent of the time', '93rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (805, 'FPS92', 'Streamflow not exceeded 92 percent of the time.', '92nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (804, 'FPS91', 'Streamflow not exceeded 91 percent of the time.', '91st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (803, 'FPS90', 'Streamflow not exceeded 90 percent of the time', '90th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (802, 'FPS89', 'Streamflow not exceeded 89 percent of the time', '89th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (801, 'FPS88', 'Streamflow not exceeded 88 percent of the time', '88th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (800, 'FPS87', 'Streamflow not exceeded 87 percent of the time.', '87th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (799, 'FPS86', 'Streamflow not exceeded 86 percent of the time', '86th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (798, 'FPS85', 'Streamflow not exceeded 85 percent of the time', '85th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (775, 'FPS62', 'Streamflow not exceeded 62 percent of the time', '62nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (774, 'FPS61', 'Streamflow not exceeded 61 percent of the time', '61st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (773, 'FPS60', 'Streamflow not exceeded 60 percent of the time', '60th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (772, 'FPS59', 'Streamflow not exceeded 59 percent of the time', '59th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (748, 'FPS35', 'Streamflow not exceeded 35 percent of the time', '35th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (747, 'FPS34', 'Streamflow not exceeded 34 percent of the time', '34th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (746, 'FPS33', 'Streamflow not exceeded 33 percent of the time', '33rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (745, 'FPS32', 'Streamflow not exceeded 32 percent of the time', '32nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (744, 'FPS31', 'Streamflow not exceeded 31 percent of the time', '31st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (743, 'FPS30', 'Streamflow not exceeded 30 percent of the time', '30th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (742, 'FPS29', 'Streamflow not exceeded 29 percent of the time', '29th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (741, 'FPS28', 'Streamflow not exceeded 28 percent of the time', '28th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (740, 'FPS27', 'Streamflow not exceeded 27 percent of the time', '27th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (739, 'FPS26', 'Streamflow not exceeded 26 percent of the time', '26th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (738, 'FPS25', 'Streamfow not exceeded 25 percent of the time', '25th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (737, 'FPS24', 'Streamflow not exceeded 24 percent of the time', '24th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (736, 'FPS23', 'Streamflow not exceeded 23 percent of the time', '23rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (735, 'FPS22', 'Streamflow not exceeded 22 percent of the time', '22nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (734, 'FPS21', 'Streamflow not exceeded 21 percent of the time', '21st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (733, 'FPS20', 'Streamflow not exceeded 20 percent of the time', '20th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (732, 'FPS19', 'Streamflow not exceeded 19 percent of the time', '19th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (731, 'FPS18', 'Streamflow not exceeded 18 percent of the time', '18th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (730, 'FPS17', 'Streamflow not exceeded 17th of the time', '17th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (749, 'FPS36', 'Streamflow not exceeded 36 percent of the time', '36th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (817, 'D85_07_09', 'July to September flow exceeded 85 percent of the time', 'July to Sept 85 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (750, 'FPS37', 'Streamflow not exceeded 37 percent of the time', '37th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (752, 'FPS39', 'Streamflow not exceeded 39 percent of the time', '39th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (771, 'FPS58', 'Streamflow not exceeded 58 percent of the time.', '58th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (770, 'FPS57', 'Streamflow not exceeded 57 percent of the time', '57th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (769, 'FPS56', 'Streamflow not exceeded 56 percent of the time', '56th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (768, 'FPS55', 'Streamflow not exceeded 55 percent of the time', '55th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (767, 'FPS54', 'Streamflow not exceeded 54 percent of the time', '54th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (766, 'FPS53', 'Streamflow not exceeded 53 percent of the time', '53rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (765, 'FPS52', 'Streamflow not exceeded 52 percent of the time', '52nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (764, 'FPS51', 'Streamflow not exceeded 51 percent of the time', '51st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (763, 'FPS50', 'Median   Streamflow not exceeded 50 percent of the time', '50th Percentile Flow  Median');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (762, 'FPS49', 'Streamflow not exceeded 49 percent of the time.', '49th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (761, 'FPS48', 'Streamflow not exceeded 48 percent of the time', '48th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (760, 'FPS47', 'Streamflow not exceeded 47 percent of the time', '47th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (759, 'FPS46', 'Streamflow not exceeded 46 percent of the time', '46th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (758, 'FPS45', 'Streamflow not exceeded 45 percent of the time', '45th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (757, 'FPS44', 'Streamflow not exceeded 44 percent of the time.', '44th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (756, 'FPS43', 'Streamflow not exceeded 43 percent of the time', '43rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (755, 'FPS42', 'Streamflow not exceeded 42 percent of the time.', '42nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (754, 'FPS41', 'Streamflow not exceeded 41 percent of the time', '41st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (753, 'FPS40', 'Streamflow not exceeded 40 percent of the time', '40th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (751, 'FPS38', 'Streamflow not exceeded 38 percent of the time', '38th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1091, 'D10C', 'Estimate of regulated streamflow exceeded 10 percent of the time', 'Controlled 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (818, 'PK15', 'Maximum instantaneous flood that occurs on average once in 15 years', '15 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (820, 'M1D100Y', '1-Day mean low-flow that occurs on average once in 100 years', '1 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (884, 'M7D10Y03', 'March 7-Day mean low-flow that occurs on average once in 10 years', 'Mar 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (883, 'M7D10Y02', 'February 7-Day mean low-flow that occurs on average once in 10 years', 'Feb 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (882, 'M7D10Y01', 'January 7-Day mean low-flow that occurs on average once in 10 years', 'Jan 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (881, 'M7D2Y12', 'December 7-Day mean low-flow that occurs on average once in 2 years', 'Dec 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (880, 'M7D2Y11', 'November 7-Day mean low-flow that occurs on average once in 2 years', 'Nov 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (879, 'M7D2Y10', 'October 7-Day mean low-flow that occurs on average once in 2 years', 'Oct 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (878, 'M7D2Y09', 'September 7-Day mean low-flow that occurs on average once in 2 years', 'Sep 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (877, 'M7D2Y08', 'August 7-Day mean low-flow that occurs on average once in 2 years', 'Aug 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (885, 'M7D10Y04', 'April 7-Day mean low-flow that occurs on average once in 10 years', 'Apr 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (876, 'M7D2Y07', 'July 7-Day mean low-flow that occurs on average once in 2 years', 'Jul 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (874, 'M7D2Y05', 'May 7-Day mean low-flow that occurs on average once in 2 years', 'May 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (873, 'M7D2Y04', 'April 7-Day mean low-flow that occurs on average once in 2 years', 'Apr 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (872, 'M7D2Y03', 'March 7-Day mean low-flow that occurs on average once in 2 years', 'Mar 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (871, 'M7D2Y02', 'February 7-Day mean low-flow that occurs on average once in 2 years', 'Feb 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (870, 'M7D2Y01', 'January 7-Day mean low-flow that occurs on average once in 2 years', 'Jan 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (869, 'PROB_30DAY', 'Probability of a stream having a flow of zero for 30 consecutive days in any given climatic year', 'Probability zero flow 30Day');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (868, 'PROB_7DAY', 'Probability of a stream having a flow of zero for 7 consecutive days in any given climatic year', 'Probability zero flow 7Day');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (867, 'PROB_1DAY', 'Probability of a stream having a flow of zero for 1 day in any given climatic year', 'Probability zero flow 1Day');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (875, 'M7D2Y06', 'June 7-Day mean low-flow that occurs on average once in 2 years', 'Jun 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (886, 'M7D10Y05', 'May 7-Day mean low-flow that occurs on average once in 10 years', 'May 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (887, 'M7D10Y06', 'June 7-Day mean low-flow that occurs on average once in 10 years', 'Jun 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (888, 'M7D10Y07', 'July 7-Day mean low-flow that occurs on average once in 10 years', 'Jul 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (907, 'PKDPTH500', 'Depth of the 500-year flood discharge in feet above the baseline_depth.', 'Five Hundred Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (906, 'PKDPTH100', 'Depth of the 100-year flood discharge in feet above the baseline_depth.', 'One Hundred Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (905, 'PKDPTH50', 'Depth of the 50-year flood discharge in feet above the baseline_depth.', 'Fifty Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (904, 'PKDPTH25', 'Depth of the 25-year flood discharge in feet above the baseline_depth.', 'Twenty Five Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (903, 'PKDPTH10', 'Depth of the 10-year flood discharge in feet above the baseline_depth.', 'Ten Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (902, 'PKDPTH2', 'Depth of the 2-year flood discharge in feet above the baseline_depth.', 'Two Year Flood Depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (901, 'Q2R_OK', 'Two year rural flow as described in OK WRIR 1997-4202', 'Two Year Rural Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (900, 'M7D10Y0304', 'March to April 7-Day mean low-flow that occurs on average once in 10 years', 'Mar to Apr 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (899, 'M7D10Y0102', 'January to February 7-Day mean low-flow that occurs on average once in 10 years', 'Jan to Feb 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (898, 'M7D10Y1104', 'November to April 7-Day mean low-flow that occurs on average once in 10 years', 'Nov to Apr 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (897, 'M7D10Y1112', 'November to December 7-Day mean low-flow that occurs on average once in 10 years', 'Nov to Dec 7 day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (896, 'TAU_SPR', 'Tau, Average base-flow recession time constant determined from daily values for March through April as defined in SIR 2008-5065', 'Tau Mar Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (895, 'TAU_WIN', 'Tau, Average base-flow recession time constant determined from daily values for November through December as defined in SIR 2008-5065', 'Tau Nov Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (894, 'TAU_ANN', 'Tau, Average annual base-flow recession time constant as defined in SIR 2008-5065', 'Tau Annual');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (893, 'M7D10Y12', 'December 7-Day mean low-flow that occurs on average once in 10 years', 'Dec 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (892, 'M7D10Y11', 'November 7-Day mean low-flow that occurs on average once in 10 years', 'Nov 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (891, 'M7D10Y10', 'October 7-Day mean low-flow that occurs on average once in 10 years', 'Oct 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (890, 'M7D10Y09', 'September 7-Day mean low-flow that occurs on average once in 10 years', 'Sep 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (889, 'M7D10Y08', 'August 7-Day mean low-flow that occurs on average once in 10 years', 'Aug 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (866, 'BF_D95', 'Base flow component of streamflow that is exceeded 95 percent of the time as defined in SIR 2004-5262', 'Base Flow 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (865, 'BF_D50', 'Base flow component of streamflow that is exceeded 50 percent of the time as defined in SIR 2004-5262', 'Base Flow 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (864, 'M30D20Y124', 'Winter 30-Day mean low-flow that occurs on average once in 20 years as defined in WRIR 86-4007', '30 Day 20 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (863, 'M30D10Y124', 'Winter 30-Day mean low-flow that occurs on average once in 10 years as defined in WRIR 86-4007', '30 Day 10 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (839, 'M120D2Y', '120_Day_2_Year_Low_Flow', '120 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (838, 'M60D100Y', '60-Day mean low-flow that occurs on average once in 100 years', '60 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (837, 'M60D50Y', '60-Day mean low-flow that occurs on average once in 50 years', '60 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (836, 'M60D20Y', '60-Day mean low-flow that occurs on average once in 20 years', '60 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (835, 'M60D10Y', '60-Day mean low-flow that occurs on average once in 10 years', '60 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (834, 'M60D5Y', '60-Day mean low-flow that occurs on average once in 5 years', '60 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (833, 'M60D2Y', '60-Day mean low-flow that occurs on average once in 2 years', '60 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (832, 'M120D100Y', '120 Day 100 Year low flow computed from climatic years Apr-Mar', '120 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (831, 'M90D100Y', '90 Day 100 Year low flow computed from climatic years Apr-Mar', '90 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (830, 'M30D100Y', '30 Day 100 Year low flow computed based on climatic years Apr-Mar', '30 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (829, 'M14D100Y', '14-Day mean low-flow that occurs on average once in 100 years', '14 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (828, 'M14D50Y', '14-Day mean low-flow that occurs on average once in 50 years', '14 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (827, 'M120D1_01Y', '120-Day mean low-flow that occurs on average once in 1.01 years', '120 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (826, 'M90D1_01Y', '90-Day mean low-flow that occurs on average once in 1.01 years', '90 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (825, 'M60D1_01Y', '60-Day mean low-flow that occurs on average once in 1.01 years', '60 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (824, 'M30D1_01Y', '30-Day mean low-flow that occurs on average once in 1.01 years', '30 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (823, 'M14D1_01Y', '14-Day mean low-flow that occurs on average once in 1.01 years', '14 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (822, 'M7D100Y', '7-Day mean low-flow that occurs on average once in 100 years', '7 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (821, 'M7D1_01Y', '7-Day mean low-flow that occurs on average once in 1.01 years', '7 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (840, 'M120D5Y', '120_Day_5_Year_Low_Flow', '120 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (819, 'M1D1_01Y', '1-Day mean low-flow that occurs on average once in 1.01 years', '1 Day 1.01 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (841, 'M120D10Y', '120_Day_10_Year_Low_Flow', '120 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (843, 'M120D50Y', '120_Day_50_Year_Low_Flow', '120 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (862, 'M30D2Y1204', 'Winter 30-Day mean low-flow that occurs on average once in 2 years as defined in WRIR 86-4007', '30 Day 2 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (861, 'M7D20Y1204', 'Winter 7-Day mean low-flow that occurs on average once in 20 years as defined in WRIR 86-4007', '7 Day 20 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (860, 'M7D10Y1204', 'Winter 7-Day mean low-flow that occurs on average once in 10 years as defined in WRIR 86-4007', '7 Day 10 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (859, 'M7D2Y1204', 'Winter 7-Day mean low-flow that occurs on average once in 2 years as defined in WRIR 86-4007', '7 Day 2 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (858, 'M3D20Y1204', 'Winter 3-Day mean low-flow that occurs on average once in 20 years as defined in WRIR 86-4007', '3 Day 20 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (857, 'M3D10Y1204', 'Winter 3-Day mean low-flow that occurs on average once in 10 years as defined in WRIR 86-4007', '3 Day 10 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (856, 'M3D2Y1204', 'Winter 3-Day mean low-flow that occurs on average once in 2 years as defined in WRIR 86-4007', '3 Day 2 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (855, 'M1D20Y1204', 'Winter 1-Day mean low-flow that occurs on average once in 20 years as defined in WRIR 86-4007', '1 Day 20 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (854, 'M1D10Y1204', 'Winter 1-Day mean low-flow that occurs on average once in 10 years as defined in WRIR 86-4007', '1 Day 10 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (853, 'M1D2Y1204', 'Winter 1-Day mean low-flow that occurs on average once in 2 years as defined in WRIR 86-4007', '1 Day 2 Year Low Flow Dec to Apr');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (852, 'PK250', 'Maximum instantaneous flow that occurs on average once in 250 years', '250 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (851, 'PK1_25', 'Maximum instantaneous flow that occurs on average once in 1.25 years', '1.25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (850, 'YRMAX', 'Year during which the maximum record flood occurred', 'Year of Maximum Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (849, 'PKMAX', 'Maximum instantaneous flow during the period of record', 'Maximum Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (848, 'SAMP_SKEW', 'Skew computed from sample annual series of peak flows', 'Sample Skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (847, 'M7DMIN', 'Minimum 7-day mean flow that occurred during the period of record', 'Minimum 7 Day Mean Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (846, 'QAMIN', 'Minimum value of time series of annual mean flows', 'Minimum Annual Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (845, 'QAMAX', 'Maximum value of time series of annual mean flows', 'Maximum Annual Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (844, 'PROBPEREN', 'Probability of a stream flowing perenially', 'Probability Stream Flowing Perennially');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (842, 'M120D20Y', '120_Day_20_Year_Low_Flow', '120 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1092, 'D15C', 'Estimate of regulated streamflow exceeded 15 percent of the time', 'Controlled 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1093, 'D20C', 'Estimate of regulated streamflow exceeded 20 percent of the time', 'Controlled 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1094, 'D30C', 'Estimate of regulated streamflow exceeded 30 percent of the time', 'Controlled 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1340, 'V1D200YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 200 years', 'Weighted 1 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1339, 'V1D100YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 100 years', 'Weighted 1 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1338, 'V1D50YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 50 years', 'Weighted 1 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1337, 'V1D25YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 25 years', 'Weighted 1 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1336, 'V1D10YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 10 years', 'Weighted 1 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1335, 'V1D5YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 5 years', 'Weighted 1 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1334, 'V1D20YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 20 years', 'Weighted 1 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1333, 'V1D2YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 2 years', 'Weighted 1 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1341, 'V1D500YW', 'Weighted 1-Day mean maximum flow that occurs on average once in 500 years', 'Weighted 1 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1332, 'PK1_11U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.11 years', 'Urban 1.11 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1330, 'PK1_01U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.01 years', 'Urban 1.01 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1329, 'PK1_005U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.005 years', 'Urban 1.005 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1328, 'PK1_25C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 1.25 years', 'Controlled 1.25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1327, 'PK1_25U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.25 years', 'Urban 1.25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1326, 'M7D10Y0406', 'April to June 7-Day mean low-flow that occurs on average once in 10 years', 'Apr to Jun 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1325, 'M7D10Y0103', 'January to March 7-Day mean low-flow that occurs on average once in 10 years', 'Jan to Mar 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1324, 'M30D5Y1012', 'October to December 30-Day mean low-flow that occurs on average once in 5 years', 'Oct to Dec 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1323, 'M30D5Y0709', 'July to September 30-Day mean low-flow that occurs on average once in 5 years', 'Jul to Sep 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1331, 'PK1_05U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.05 years', 'Urban 1.05 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1342, 'V3D2YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 2 years', 'Weighted 3 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1343, 'V3D5YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 5 years', 'Weighted 3 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1344, 'V3D10YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 10 years', 'Weighted 3 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1363, 'V15D20YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 20 years', 'Weighted 15 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1362, 'V15D10YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 10 years', 'Weighted 15 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1361, 'V15D5YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 5 years', 'Weighted 15 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1360, 'V15D2YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 2 years', 'Weighted 15 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1359, 'V7D500YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 500 years', 'Weighted 7 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1358, 'V7D200YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 200 years', 'Weighted 7 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1357, 'V7D100YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 100 years', 'Weighted 7 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1356, 'V7D50YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 50 years', 'Weighted 7 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1355, 'V7D25YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 25 years', 'Weighted 7 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1354, 'V7D20YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 20 years', 'Weighted 7 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1353, 'V7D10YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 10 years', 'Weighted 7 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1352, 'V7D5YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 5 years', 'Weighted 7 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1351, 'V7D2YW', 'Weighted 7-Day mean maximum flow that occurs on average once in 2 years', 'Weighted 7 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1350, 'V3D500YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 500 years', 'Weighted 3 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1349, 'V3D200YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 200 years', 'Weighted 3 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1348, 'V3D100YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 100 years', 'Weighted 3 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1347, 'V3D50YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 50 years', 'Weighted 3 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1346, 'V3D25YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 25 years', 'Weighted 3 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1345, 'V3D20YW', 'Weighted 3-Day mean maximum flow that occurs on average once in 20 years', 'Weighted 3 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1322, 'M30D5Y0406', 'April to June 30-Day mean low-flow that occurs on average once in 5 years', 'Apr to Jun 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1321, 'M30D5Y0103', 'January to March 30-Day mean low-flow that occurs on average once in 5 years', 'Jan to Mar 30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1320, 'M1D10Y0709', 'July to September 1-Day mean low-flow that occurs on average once in 10 years', 'Jul to Sep 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1319, 'M1D10Y0406', 'April to June 1-Day mean low-flow that occurs on average once in 10 years', 'Apr to Jun 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1295, 'Q10MED', 'Median monthly mean flow for October', 'October Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1294, 'Q9MED', 'Median monthly mean flow for September', 'September Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1293, 'Q8MED', 'Median monthly mean flow for August', 'August Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1292, 'Q7MED', 'Median monthly mean flow for July', 'July Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1291, 'Q6MED', 'Median monthly mean flow for June', 'June Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1290, 'Q5MED', 'Median monthly mean flow for May', 'May Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1289, 'Q4MED', 'Median monthly mean flow for April', 'April Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1288, 'Q3MED', 'Median monthly mean flow for March', 'March Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1287, 'Q2MED', 'Median monthly mean flow for February', 'February Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1286, 'Q1MED', 'Median monthly mean flow for January', 'January Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1285, 'Q12MIN', 'Minimum monthly mean flow for December', 'December Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1284, 'Q11MIN', 'Minimum monthly mean flow for November', 'November Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1283, 'Q10MIN', 'Minimum monthly mean flow for October', 'October Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1282, 'Q9MIN', 'Minimum monthly mean flow for September', 'September Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1281, 'Q8MIN', 'Minimum monthly mean flow for August', 'August Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1280, 'Q7MIN', 'Minimum monthly mean flow for July', 'July Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1279, 'Q6MIN', 'Minimum monthly mean flow for June', 'June Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1278, 'Q5MIN', 'Minimum monthly mean flow for May', 'May Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1277, 'Q4MIN', 'Minimum monthly mean flow for April', 'April Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1296, 'Q11MED', 'Median monthly mean flow for November', 'November Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1364, 'V15D25YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 25 years', 'Weighted 15 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1297, 'Q12MED', 'Median monthly mean flow for December', 'December Median Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1299, 'Q2MAX', 'Maximum monthly mean flow for February', 'February Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1318, 'M1D10Y0103', 'January to March 1-Day mean low-flow that occurs on average once in 10 years', 'Jan to Mar 1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1317, 'QAH_10_12', 'October to December harmonic mean flow', 'Oct to Dec Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1316, 'QAH_07_09', 'July to September harmonic mean flow', 'Jul to Sep Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1315, 'QAH_04_06', 'April to June harmonic mean flow', 'Apr to Jun Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1314, 'QAH_01_03', 'January to March harmonic mean flow', 'Jan to Mar Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1313, 'D50_10_12', 'October to December flow exceeded 50 percent of the time', 'Oct to Dec 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1312, 'D50_04_06', 'April to June flow exceeded 50 percent of the time', 'Apr to Jun 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1311, 'D50_01_03', 'January to March flow exceeded 50 percent of the time', 'Jan to Mar 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1310, 'M2D10Y', '2-Day mean low-flow that occurs on average once in 10 years', '2 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1309, 'Q12MAX', 'Maximum monthly mean flow for December', 'December Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1308, 'Q11MAX', 'Maximum monthly mean flow for November', 'November Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1307, 'Q10MAX', 'Maximum monthly mean flow for October', 'October Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1306, 'Q9MAX', 'Maximum monthly mean flow for September', 'September Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1305, 'Q8MAX', 'Maximum monthly mean flow for August', 'August Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1304, 'Q7MAX', 'Maximum monthly mean flow for July', 'July Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1303, 'Q6MAX', 'Maximum monthly mean flow for June', 'June Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1302, 'Q5MAX', 'Maximum monthly mean flow for May', 'May Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1301, 'Q4MAX', 'Maximum monthly mean flow for April', 'April Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1300, 'Q3MAX', 'Maximum monthly mean flow for March', 'March Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1298, 'Q1MAX', 'Maximum monthly mean flow for January', 'January Maximum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1276, 'Q3MIN', 'Minimum monthly mean flow for March', 'March Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1365, 'V15D50YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 50 years', 'Weighted 15 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1367, 'V15D200YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 200 years', 'Weighted 15 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1431, 'M7D90P', '7-Day mean low-flow that is exceeded in 90 percent of all years during the period of record', '7 Day 90 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1430, 'M7D80P', '7-Day mean low-flow that is exceeded in 80 percent of all years during the period of record', '7 Day 80 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1429, 'M7D70P', '7-Day mean low-flow that is exceeded in 70 percent of all years during the period of record', '7 Day 70 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1428, 'M7D60P', '7-Day mean low-flow that is exceeded in 60 percent of all years during the period of record', '7 Day 60 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1427, 'M7D50P', '7-Day mean low-flow that is exceeded in 50 percent of all years during the period of record', '7 Day 50 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1426, 'M7D40P', '7-Day mean low-flow that is exceeded in 40 percent of all years during the period of record', '7 Day 40 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1425, 'M7D30P', '7-Day mean low-flow that is exceeded in 30 percent of all years during the period of record', '7 Day 30 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1424, 'M7D20P', '7-Day mean low-flow that is exceeded in 20 percent of all years during the period of record', '7 Day 20 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1432, 'M7D2YPSM', '7-Day mean low-flow per square mile that occurs on average once in 2 years', '7 Day 2 Year Low Flow Per SqMi');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1423, 'M7D10P', '7-Day mean low-flow that is exceeded in 10 percent of all years during the period of record', '7 Day 10 Percent Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1421, 'V30D200YR', 'Regression 30-Day mean maximum flow that occurs on average once in 200 years', 'Regression 30 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1420, 'V30D100YR', 'Regression 30-Day mean maximum flow that occurs on average once in 100 years', 'Regression 30 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1419, 'V30D50YR', 'Regression 30-Day mean maximum flow that occurs on average once in 50 years', 'Regression 30 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1418, 'V30D25YR', 'Regression 30-Day mean maximum flow that occurs on average once in 25 years', 'Regression 30 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1417, 'V30D20YR', 'Regression 30-Day mean maximum flow that occurs on average once in 20 years', 'Regression 30 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1416, 'V30D10YR', 'Regression 30-Day mean maximum flow that occurs on average once in 10 years', 'Regression 30 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1415, 'V30D5YR', 'Regression 30-Day mean maximum flow that occurs on average once in 5 years', 'Regression 30 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1414, 'V30D2YR', 'Regression 30-Day mean maximum flow that occurs on average once in 2 years', 'Regression 30 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1422, 'V30D500YR', 'Regression 30-Day mean maximum flow that occurs on average once in 500 years', 'Regression 30 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1433, 'M7D10YPSM', '7-Day mean low-flow per square mile that occurs on average once in 10 years', '7 Day 10 Year Low Flow Per SqMi');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1434, 'D99_8', 'Streamflow exceeded 99.8 percent of the time', '99.8 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1435, 'D0_2', 'Streamflow exceeded 0.2 percent of the time', '0.2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1454, 'PROBINTRM', 'Probability of a stream flowing intermittently', 'Probability of stream flowing intermittent');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1453, 'P7D2Y', 'Probability of zero 7-day, 2-year low flow', 'Probability zero flow 7 day 2 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1452, 'P7D20Y', 'Probability of zero 7-day, 20-year low flow', 'Probability zero flow 7 day 20 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1451, 'P30D2Y', 'Probability of zero 30 day, 2 year low flow', 'Probability zero flow 30day 2 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1450, 'RCHRG_WIN', 'Ground water recharge rate during winter season January 1 to March 15', 'GW_Recharge_Jan_to_Mar15');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1449, 'RCHRG_ANN', 'Annual ground water recharge rate', 'GW_Recharge_Ann');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1448, 'RCHRG_FAL', 'Ground water recharge rate during fall season November 1 to December 31', 'GW_Recharge_Nov_to_Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1447, 'RCHRG_SUM', 'Ground water recharge rate during summer season June 1 to October 31', 'GW_Recharge_Jun_to_Oct');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1446, 'RCHRG_SPR', 'Ground water recharge rate during spring season March 16 to May 31', 'GW_Recharge_Mar16_to_May');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1445, 'M14D5Y710', 'July to October 14-day low flow that occurs on average once in 5 years', 'Jul_to_Oct_14_Day_5_Yr_Low_Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1444, 'M122D10Y69', '122 Day 10 Year lowflow Jun-Sep', '122_Day_10_Year_Low_Flow_Jun_to_Sep');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1443, 'M30D10Y69', '30 Day 10 Year lowflow Jun-Sep', '30_Day_10_Year_Low_Flow_Jun_to_Sep');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1442, 'M7D10Y0609', '7 Day 10 Year lowflow Jun-Sep', '7_Day_10_Year_lowflow_Jun_to_Sep');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1441, 'M30D10Y123', '30 Day 10 Year lowflow Dec-Mar', '30_Day_10_Year_Low_Flow_Dec_to_Mar');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1440, 'M7D10Y1203', '7 Day 10 Year lowflow Dec-Mar', '7_Day_10_Year_lowflow_Dec_to_Mar');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1439, 'M30D10YON', '30 Day 10 Year lowflow Oct-Nov', '30_Day_10_Year_Low_Flow_Oct_to_Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1438, 'PK1_01', 'Maximum instantaneous flow that occurs on average once in 1.01 years', '1.01 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1437, 'M30D10YOD', 'October to December 30-day low flow that occurs on average once in 10 years', 'Oct_to_Dec_30_Day_10_Year_Low_Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1436, 'M30D10Y46', 'April to June 30-day low flow that occurs on average once in 10 years', 'Apr to Jun 30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1413, 'V15D500YR', 'Regression 15-Day mean maximum flow that occurs on average once in 500 years', 'Regression 15 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1412, 'V15D200YR', 'Regression 15-Day mean maximum flow that occurs on average once in 200 years', 'Regression 15 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1411, 'V15D100YR', 'Regression 15-Day mean maximum flow that occurs on average once in 100 years', 'Regression 15 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1410, 'V15D50YR', 'Regression 15-Day mean maximum flow that occurs on average once in 50 years', 'Regression 15 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1386, 'V1D500YR', 'Regression 1-Day mean maximum flow that occurs on average once in 500 years', 'Regression 1 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1385, 'V1D200YR', 'Regression 1-Day mean maximum flow that occurs on average once in 200 years', 'Regression 1 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1384, 'V1D100YR', 'Regression 1-Day mean maximum flow that occurs on average once in 100 years', 'Regression 1 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1383, 'V1D50YR', 'Regression 1-Day mean maximum flow that occurs on average once in 50 years', 'Regression 1 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1382, 'V1D25YR', 'Regression 1-Day mean maximum flow that occurs on average once in 25 years', 'Regression 1 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1381, 'V1D10YR', 'Regression 1-Day mean maximum flow that occurs on average once in 10 years', 'Regression 1 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1380, 'V1D5YR', 'Regression 1-Day mean maximum flow that occurs on average once in 5 years', 'Regression 1 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1379, 'V1D20YR', 'Regression 1-Day mean maximum flow that occurs on average once in 20 years', 'Regression 1 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1378, 'V1D2YR', 'Regression 1-Day mean maximum flow that occurs on average once in 2 years', 'Regression 1 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1377, 'V30D500YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 500 years', 'Weighted 30 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1376, 'V30D200YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 200 years', 'Weighted 30 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1375, 'V30D100YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 100 years', 'Weighted 30 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1374, 'V30D50YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 50 years', 'Weighted 30 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1373, 'V30D25YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 25 years', 'Weighted 30 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1372, 'V30D20YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 20 years', 'Weighted 30 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1371, 'V30D10YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 10 years', 'Weighted 30 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1370, 'V30D5YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 5 years', 'Weighted 30 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1369, 'V30D2YW', 'Weighted 30-Day mean maximum flow that occurs on average once in 2 years', 'Weighted 30 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1368, 'V15D500YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 500 years', 'Weighted 15 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1387, 'V3D2YR', 'Regression 3-Day mean maximum flow that occurs on average once in 2 years', 'Regression 3 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1366, 'V15D100YW', 'Weighted 15-Day mean maximum flow that occurs on average once in 100 years', 'Weighted 15 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1388, 'V3D5YR', 'Regression 3-Day mean maximum flow that occurs on average once in 5 years', 'Regression 3 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1390, 'V3D20YR', 'Regression 3-Day mean maximum flow that occurs on average once in 20 years', 'Regression 3 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1409, 'V15D25YR', 'Regression 15-Day mean maximum flow that occurs on average once in 25 years', 'Regression 15 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1408, 'V15D20YR', 'Regression 15-Day mean maximum flow that occurs on average once in 20 years', 'Regression 15 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1407, 'V15D10YR', 'Regression 15-Day mean maximum flow that occurs on average once in 10 years', 'Regression 15 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1406, 'V15D5YR', 'Regression 15-Day mean maximum flow that occurs on average once in 5 years', 'Regression 15 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1405, 'V15D2YR', 'Regression 15-Day mean maximum flow that occurs on average once in 2 years', 'Regression 15 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1404, 'V7D500YR', 'Regression 7-Day mean maximum flow that occurs on average once in 500 years', 'Regression 7 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1403, 'V7D200YR', 'Regression 7-Day mean maximum flow that occurs on average once in 200 years', 'Regression 7 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1402, 'V7D100YR', 'Regression 7-Day mean maximum flow that occurs on average once in 100 years', 'Regression 7 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1401, 'V7D50YR', 'Regression 7-Day mean maximum flow that occurs on average once in 50 years', 'Regression 7 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1400, 'V7D25YR', 'Regression 7-Day mean maximum flow that occurs on average once in 25 years', 'Regression 7 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1399, 'V7D20YR', 'Regression 7-Day mean maximum flow that occurs on average once in 20 years', 'Regression 7 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1398, 'V7D10YR', 'Regression 7-Day mean maximum flow that occurs on average once in 10 years', 'Regression 7 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1397, 'V7D5YR', 'Regression 7-Day mean maximum flow that occurs on average once in 5 years', 'Regression 7 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1396, 'V7D2YR', 'Regression 7-Day mean maximum flow that occurs on average once in 2 years', 'Regression 7 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1395, 'V3D500YR', 'Regression 3-Day mean maximum flow that occurs on average once in 500 years', 'Regression 3 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1394, 'V3D200YR', 'Regression 3-Day mean maximum flow that occurs on average once in 200 years', 'Regression 3 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1393, 'V3D100YR', 'Regression 3-Day mean maximum flow that occurs on average once in 100 years', 'Regression 3 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1392, 'V3D50YR', 'Regression 3-Day mean maximum flow that occurs on average once in 50 years', 'Regression 3 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1391, 'V3D25YR', 'Regression 3-Day mean maximum flow that occurs on average once in 25 years', 'Regression 3 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1389, 'V3D10YR', 'Regression 3-Day mean maximum flow that occurs on average once in 10 years', 'Regression 3 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (729, 'FPS16', 'Streamflow not exceeded 16 percent of the time.', '16th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1275, 'Q2MIN', 'Minimum monthly mean flow for February', 'February Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1273, 'D96', 'Streamflow exceeded 96 percent of the time', '96 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1158, 'M4D1000Y', '4-Day mean low-flow that occurs on average once in 1000 years (0.1% chance)', '4 Day 1000 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1157, 'M4D500Y', '4-Day mean low-flow that occurs on average once in 500 years (0.2% chance)', '4 Day 500 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1156, 'M4D200Y', '4-Day mean low-flow that occurs on average once in 200 years (0.5% chance)', '4 Day 200 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1155, 'M4D100Y', '4-Day mean low-flow that occurs on average once in 100 years (1% chance)', '4 Day 100 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1154, 'M4D50Y', '4-Day mean low-flow that occurs on average once in 50 years (2% chance)', '4 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1153, 'M4D20Y', '4-Day mean low-flow that occurs on average once in 20 years (5% chance)', '4 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1152, 'M4D10Y', '4-Day mean low-flow that occurs on average once in 10 years (10% chance)', '4 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1151, 'M4D5Y', '4-Day mean low-flow that occurs on average once in 5 years (20% chance)', '4 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1159, 'M7D1_11Y', '7-Day mean low-flow that occurs on average once in 1.11 years (90% chance)', '7 Day 1.11 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1150, 'M4D3_33Y', '4-Day mean low-flow that occurs on average once in 3.33 years (30% chance)', '4 Day 3.33 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1148, 'M4D2Y', '4-Day mean low-flow that occurs on average once in 2 years (50% chance)', '4 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1147, 'M4D1_67Y', '4-Day mean low-flow that occurs on average once in 1.67 years (60% chance)', '4 Day 1.67 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1146, 'M4D1_43Y', '4-Day mean low-flow that occurs on average once in 1.43 years (70% chance)', '4 Day 1.43 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1145, 'M4D1_25Y', '4-Day mean low-flow that occurs on average once in 1.25 years (80% chance)', '4 Day 1.25 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1144, 'M4D1_11Y', '4-Day mean low-flow that occurs on average once in 1.11 years (90% chance)', '4 Day 1.11 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1143, 'M1D3_33Y', '1-Day mean low-flow that occurs on average once in 3.33 years (30% chance)', '1 Day 3.33 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1142, 'M1D2_5Y', '1-Day mean low-flow that occurs on average once in 2.5 years (40% chance)', '1 Day 2.5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1141, 'M1D1_67Y', '1-Day mean low-flow that occurs on average once in 1.67 years (60% chance)', '1 Day 1.67 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1149, 'M4D2_5Y', '4-Day mean low-flow that occurs on average once in 2.5 years (40% chance)', '4 Day 2.5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1160, 'M7D1_25Y', '7-Day mean low-flow that occurs on average once in 1.25 years (80% chance)', '7 Day 1.25 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1161, 'M7D1_43Y', '7-Day mean low-flow that occurs on average once in 1.43 years (70% chance)', '7 Day 1.43 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1162, 'M7D1_67Y', '7-Day mean low-flow that occurs on average once in 1.67 years (60% chance)', '7 Day 1.67 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1181, 'KYVARIND10', 'Mapped streamflow-variability index as defined in SIR 2010-5217', 'KY Streamflow Variability Index 2010');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1180, 'KYVARIND93', 'Mapped streamflow variability index as defined in WRIR 92-4173', 'KY Streamflow Variability Index 1993');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1179, 'DLOWER', 'Streamflow exceeded at the lowermost computed percent of the time', 'Lower Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1178, 'DUPPER', 'Streamflow exceeded at the uppermost computed percent of the time', 'Upper Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1177, 'HYSEP', 'Median percentage of baseflow to annual streamflow', 'Hydrograph separation percent');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1176, 'M30D1000Y', '30-Day mean low-flow that occurs on average once in 1000 years (0.1% chance)', '30 Day 1000 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1175, 'M30D500Y', '30-Day mean low-flow that occurs on average once in 500 years (0.2% chance)', '30 Day 500 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1174, 'M30D200Y', '30-Day mean low-flow that occurs on average once in 200 years (0.5% chance)', '30 Day 200 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1173, 'M30D3_33Y', '30-Day mean low-flow that occurs on average once in 3.33 years (30% chance)', '30 Day 3.33 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1172, 'M30D2_5Y', '30-Day mean low-flow that occurs on average once in 2.5 years (40% chance)', '30 Day 2.5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1171, 'M30D1_67Y', '30-Day mean low-flow that occurs on average once in 1.67 years (60% chance)', '30 Day 1.67 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1170, 'M30D1_43Y', '30-Day mean low-flow that occurs on average once in 1.43 years (70% chance)', '30 Day 1.43 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1169, 'M30D1_25Y', '30-Day mean low-flow that occurs on average once in 1.25 years (80% chance)', '30 Day 1.25 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1168, 'M30D1_11Y', '30-Day mean low-flow that occurs on average once in 1.11 years (90% chance)', '30 Day 1.11 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1167, 'M7D1000Y', '7-Day mean low-flow that occurs on average once in 1000 years (0.1% chance)', '7 Day 1000 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1166, 'M7D500Y', '7-Day mean low-flow that occurs on average once in 500 years (0.2% chance)', '7 Day 500 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1165, 'M7D200Y', '7-Day mean low-flow that occurs on average once in 200 years (0.5% chance)', '7 Day 200 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1164, 'M7D3_33Y', '7-Day mean low-flow that occurs on average once in 3.33 years (30% chance)', '7 Day 3.33 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1163, 'M7D2_5Y', '7-Day mean low-flow that occurs on average once in 2.5 years (40% chance)', '7 Day 2.5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1140, 'M1D1_43Y', '1-Day mean low-flow that occurs on average once in 1.43 years (70% chance)', '1 Day 1.43 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1139, 'M1D1_25Y', '1-Day mean low-flow that occurs on average once in 1.25 years (80% chance)', '1 Day 1.25 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1138, 'M1D1_11Y', '1-Day mean low-flow that occurs on average once in 1.11 years (90% chance)', '1 Day 1.11 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1137, 'P7D10Y1012', 'Probability of a stream having a flow of zero for flow durations', 'Prob zero flow 7 day 10 yr Oct to Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1113, 'WRC_MEANC', 'WRC Mean_controlled', 'Controlled WRC Mean');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1112, 'WRC_SKEWC', 'WRC Skew_controlled', 'Controlled WRC Skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1111, 'YRSPKC', 'Number of years of regulated systematic peak flow record', 'Controlled Systematic Peak Years');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1110, 'DYRSC', 'Number of Years used in duration analysis_controlled', 'Controlled Years used in Duration Anal');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1109, 'BFYRSC', 'Number of Years used in  base flow analysis_controlled', 'Controlled Years used in Base Flow Analy');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1108, 'YRSLOWC', 'Number of years of low-flow record_controlled', 'Controlled Low Flow Years');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1107, 'YRSHISPKC', 'Number of consecutive years used for historic-peak adjustment to regulated flood-frequency data', 'Controlled Peak Years with Historic adj');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1106, 'DYRS', 'Number of Years used in duration analysis', 'Years used in Duration Analysis');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1105, 'BFYRS', 'Number of Years used in base flow analysis', 'Years used in Base Flow Analysis');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1104, 'D99C', 'Estimate of regulated streamflow exceeded 99 percent of the time', 'Controlled 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1103, 'D95C', 'Estimate of regulated streamflow exceeded 95 percent of the time', 'Controlled 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1102, 'D90C', 'Estimate of regulated streamflow exceeded 90 percent of the time', 'Controlled 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1101, 'D85C', 'Estimate of regulated streamflow exceeded 85 percent of the time', 'Controlled 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1100, 'D80C', 'Estimate of regulated streamflow exceeded 80 percent of the time', 'Controlled 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1099, 'D70C', 'Estimate of regulated streamflow exceeded 70 percent of the time', 'Controlled 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1098, 'D5C', 'Estimate of regulated streamflow exceeded 5 percent of the time', 'Controlled 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1097, 'D60C', 'Estimate of regulated streamflow exceeded 60 percent of the time', 'Controlled 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1096, 'D50C', 'Estimate of regulated streamflow exceeded 50 percent of the time', 'Controlled 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1095, 'D40C', 'Estimate of regulated streamflow exceeded 40 percent of the time', 'Controlled 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1114, 'WRC_STDC', 'WRD Standard Deviation_controlled', 'Controlled WRC STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1182, 'PROBZ30Q2', 'Probability of zero 30-day, 2-year low flow', 'Probability of Zero 30Q2');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1115, 'QAC', 'Mean Annual Flow_controlled', 'Controlled Mean Annual Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1117, 'PK1_5U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 1.5 years', 'Urban 1.5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1136, 'P1D10Y1012', 'Probability of a stream having a flow of zero for flow durations', 'Prob zero flow 1 day 10 yr Oct to Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1135, 'P30D5Y', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 30 day 5 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1134, 'P30D10Y', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 30 day 10 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1133, 'P7D10Y', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 7 day 10 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1132, 'P1D10Y', 'Probability of a stream having a flow of zero for flow durations', 'Probability zero flow 1 day 10 year');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1131, 'M7D10Y1012', '7 Day 10 Year lowflow October to December', '7 Day 10 Year lowflow Oct to Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1130, 'M1D10Y1012', '1 Day 10 Year lowflow October to December', '1 Day 10 Year lowflow Oct to Dec');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1129, 'MSE_WTSKEW', 'Mean squared error of weighted skew coefficient', 'Mean square error of weighted skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1128, 'WT_SKEW', 'Skew of logs of annual peak flows computed by weighting site and regional skew estimates based on Bulletin 17B', 'Weighted Skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1127, 'PK500U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 500 years', 'Urban 500 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1126, 'PK200U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 200 years', 'Urban 200 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1125, 'PK100U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 100 years', 'Urban 100 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1124, 'PK50U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 50 years', 'Urban 50 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1123, 'PK25U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 25 years', 'Urban 25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1122, 'PK15U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 15 years', 'Urban 15 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1121, 'PK10U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 10 years', 'Urban 10 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1120, 'PK5U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 5 years', 'Urban 5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1119, 'PK2_33U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 2.33 years', 'Urban 2.33 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1118, 'PK2U', 'Maximum instantaneous flow affected by urbanization that occurs on average once in 2 years', 'Urban 2 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1116, 'QAHC', 'Harmonic mean flow_controlled', 'Controlled Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1274, 'Q1MIN', 'Minimum monthly mean flow for January', 'January Minimum Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1183, 'PROBZ30Q5', 'Probability of zero 30-day, 5-year low flow', 'Probability of Zero 30Q5');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1185, 'PROBZ7Q10', 'Probability of zero 7-day, 10-year low flow', 'Probability of Zero 7Q10');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1249, 'M1D10D99', 'October minimum 1-day mean flow that is exceeded 99 percent of the time', 'Oct 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1248, 'M1D10D90', 'October minimum 1-day mean flow that is exceeded 90 percent of the time', 'Oct 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1247, 'M1D10D85', 'October minimum 1-day mean flow that is exceeded 85 percent of the time', 'Oct 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1246, 'M1D10D75', 'October minimum 1-day mean flow that is exceeded 75 percent of the time', 'Oct 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1245, 'M1D10D50', 'October minimum 1-day mean flow that is exceeded 50 percent of the time', 'Oct 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1244, 'M1D10D25', 'October minimum 1-day mean flow that is exceeded 25 percent of the time', 'Oct 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1243, 'M1D09D99', 'September minimum 1-day mean flow that is exceeded 99 percent of the time', 'Sep 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1242, 'M1D09D90', 'September minimum 1-day mean flow that is exceeded 90 percent of the time', 'Sep 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1250, 'M1D11D25', 'November minimum 1-day mean flow that is exceeded 25 percent of the time', 'Nov 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1241, 'M1D09D85', 'September minimum 1-day mean flow that is exceeded 85 percent of the time', 'Sep 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1239, 'M1D09D50', 'September minimum 1-day mean flow that is exceeded 50 percent of the time', 'Sep 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1238, 'M1D09D25', 'September minimum 1-day mean flow that is exceeded 25 percent of the time', 'Sep 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1237, 'M1D08D99', 'August minimum 1-day mean flow that is exceeded 99 percent of the time', 'Aug 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1236, 'M1D08D90', 'August minimum 1-day mean flow that is exceeded 90 percent of the time', 'Aug 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1235, 'M1D08D85', 'August minimum 1-day mean flow that is exceeded 85 percent of the time', 'Aug 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1234, 'M1D08D75', 'August minimum 1-day mean flow that is exceeded 75 percent of the time', 'Aug 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1233, 'M1D08D50', 'August minimum 1-day mean flow that is exceeded 50 percent of the time', 'Aug 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1232, 'M1D08D25', 'August minimum 1-day mean flow that is exceeded 25 percent of the time', 'Aug 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1240, 'M1D09D75', 'September minimum 1-day mean flow that is exceeded 75 percent of the time', 'Sep 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1251, 'M1D11D50', 'November minimum 1-day mean flow that is exceeded 50 percent of the time', 'Nov 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1252, 'M1D11D75', 'November minimum 1-day mean flow that is exceeded 75 percent of the time', 'Nov 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1253, 'M1D11D85', 'November minimum 1-day mean flow that is exceeded 85 percent of the time', 'Nov 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1272, 'D94', 'Streamflow exceeded 94 percent of the time', '94 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1271, 'D92', 'Streamflow exceeded 92 percent of the time', '92 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1270, 'D91', 'Streamflow exceeded 91 percent of the time', '91 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1269, 'D0_01', 'Streamflow exceeded 0.01 percent of the time', '0.01 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1268, 'D0_05', 'Streamflow exceeded 0.05 percent of the time', '0.05 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1267, 'D0_1', 'Streamflow exceeded 0.1 percent of the time', '0.1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1266, 'D0_5', 'Streamflow exceeded 0.5 percent of the time', '0.5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1265, 'D99_99', 'Streamflow exceeded 99.99 percent of the time', '99.99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1264, 'D99_95', 'Streamflow exceeded 99.95 percent of the time', '99.95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1263, 'D99_9', 'Streamflow exceeded 99.9 percent of the time', '99.9 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1262, 'MEDKSA', 'Median annual flow for most recent 10-year period (Kansas)', 'Median Annual Flow KSA');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1261, 'M1D12D99', 'December minimum 1-day mean flow that is exceeded 99 percent of the time', 'Dec 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1260, 'M1D12D90', 'December minimum 1-day mean flow that is exceeded 90 percent of the time', 'Dec 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1259, 'M1D12D85', 'December minimum 1-day mean flow that is exceeded 85 percent of the time', 'Dec 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1258, 'M1D12D75', 'December minimum 1-day mean flow that is exceeded 75 percent of the time', 'Dec 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1257, 'M1D12D50', 'December minimum 1-day mean flow that is exceeded 50 percent of the time', 'Dec 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1256, 'M1D12D25', 'December minimum 1-day mean flow that is exceeded 25 percent of the time', 'Dec 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1255, 'M1D11D99', 'November minimum 1-day mean flow that is exceeded 99 percent of the time', 'Nov 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1254, 'M1D11D90', 'November minimum 1-day mean flow that is exceeded 90 percent of the time', 'Nov 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1231, 'M1D0809D99', 'August and September  minimum 1-day mean flow that is exceeded 99 percent of the time', 'Aug Sep 99 Pct Dur Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1230, 'M1D0809D90', 'August and September  minimum 1-day mean flow that is exceeded 90 percent of the time', 'Aug Sep 90 Pct Dur Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1229, 'M1D0809D75', 'August and September  minimum 1-day mean flow that is exceeded 75 percent of the time', 'Aug Sep 75 Pct Dur Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1228, 'M1D07D99', 'July minimum 1-day mean flow that is exceeded 99 percent of the time', 'Jul 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1204, 'M1D03D99', 'March minimum 1-day mean flow that is exceeded 99 percent of the time', 'Mar 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1203, 'M1D03D90', 'March minimum 1-day mean flow that is exceeded 90 percent of the time', 'Mar 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1202, 'M1D03D85', 'March minimum 1-day mean flow that is exceeded 85 percent of the time', 'Mar 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1201, 'M1D03D75', 'March minimum 1-day mean flow that is exceeded 75 percent of the time', 'Mar 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1200, 'M1D03D50', 'March minimum 1-day mean flow that is exceeded 50 percent of the time', 'Mar 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1199, 'M1D03D25', 'March minimum 1-day mean flow that is exceeded 25 percent of the time', 'Mar 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1198, 'M1D02D99', 'February minimum 1-day mean flow that is exceeded 99 percent of the time', 'Feb 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1197, 'M1D02D90', 'February minimum 1-day mean flow that is exceeded 90 percent of the time', 'Feb 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1196, 'M1D02D85', 'February minimum 1-day mean flow that is exceeded 85 percent of the time', 'Feb 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1195, 'M1D02D75', 'February minimum 1-day mean flow that is exceeded 75 percent of the time', 'Feb 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1194, 'M1D02D50', 'February minimum 1-day mean flow that is exceeded 50 percent of the time', 'Feb 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1193, 'M1D02D25', 'February minimum 1-day mean flow that is exceeded 25 percent of the time', 'Feb 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1192, 'M1D01D99', 'January minimum 1-day mean flow that is exceeded 99 percent of the time', 'Jan 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1191, 'M1D01D90', 'January minimum 1-day mean flow that is exceeded 90 percent of the time', 'Jan 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1190, 'M1D01D85', 'January minimum 1-day mean flow that is exceeded 85 percent of the time', 'Jan 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1189, 'M1D01D75', 'January minimum 1-day mean flow that is exceeded 75 percent of the time', 'Jan 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1188, 'M1D01D50', 'January minimum 1-day mean flow that is exceeded 50 percent of the time', 'Jan 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1187, 'M1D01D25', 'January minimum 1-day mean flow that is exceeded 25 percent of the time', 'Jan 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1186, 'PROBZ7Q20', 'Probability of zero 7-day, 20-year low flow', 'Probability of Zero 7Q20');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1205, 'M1D04D25', 'April minimum 1-day mean flow that is exceeded 25 percent of the time', 'Apr 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1184, 'PROBZ7Q2', 'Probability of zero 7-day, 2-year low flow', 'Probability of Zero 7Q2');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1206, 'M1D04D50', 'April minimum 1-day mean flow that is exceeded 50 percent of the time', 'Apr 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1208, 'M1D04D85', 'April minimum 1-day mean flow that is exceeded 85 percent of the time', 'Apr 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1227, 'M1D07D90', 'July minimum 1-day mean flow that is exceeded 90 percent of the time', 'Jul 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1226, 'M1D07D85', 'July minimum 1-day mean flow that is exceeded 85 percent of the time', 'Jul 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1225, 'M1D07D75', 'July minimum 1-day mean flow that is exceeded 75 percent of the time', 'Jul 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1224, 'M1D07D50', 'July minimum 1-day mean flow that is exceeded 50 percent of the time', 'Jul 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1223, 'M1D07D25', 'July minimum 1-day mean flow that is exceeded 25 percent of the time', 'Jul 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1222, 'M1D06D99', 'June minimum 1-day mean flow that is exceeded 99 percent of the time', 'Jun 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1221, 'M1D06D90', 'June minimum 1-day mean flow that is exceeded 90 percent of the time', 'Jun 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1220, 'M1D06D85', 'June minimum 1-day mean flow that is exceeded 85 percent of the time', 'Jun 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1219, 'M1D06D75', 'June minimum 1-day mean flow that is exceeded 75 percent of the time', 'Jun 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1218, 'M1D06D50', 'June minimum 1-day mean flow that is exceeded 50 percent of the time', 'Jun 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1217, 'M1D06D25', 'June minimum 1-day mean flow that is exceeded 25 percent of the time', 'Jun 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1216, 'M1D05D99', 'May minimum 1-day mean flow that is exceeded 99 percent of the time', 'May 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1215, 'M1D05D90', 'May minimum 1-day mean flow that is exceeded 90 percent of the time', 'May 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1214, 'M1D05D85', 'May minimum 1-day mean flow that is exceeded 85 percent of the time', 'May 85 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1213, 'M1D05D75', 'May minimum 1-day mean flow that is exceeded 75 percent of the time', 'May 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1212, 'M1D05D50', 'May minimum 1-day mean flow that is exceeded 50 percent of the time', 'May 50 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1211, 'M1D05D25', 'May minimum 1-day mean flow that is exceeded 25 percent of the time', 'May 25 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1210, 'M1D04D99', 'April minimum 1-day mean flow that is exceeded 99 percent of the time', 'Apr 99 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1209, 'M1D04D90', 'April minimum 1-day mean flow that is exceeded 90 percent of the time', 'Apr 90 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1207, 'M1D04D75', 'April minimum 1-day mean flow that is exceeded 75 percent of the time', 'Apr 75 Pct Duration Min 1 Day Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (728, 'FPS15', 'Streamflow not exceeded 15 percent of the time', '15th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (977, 'MARMINMON', 'Minimum of  March monthly mean flows', 'Min Mar Monthly Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (726, 'FPS13', 'Streamflow not exceeded 13 percent of the time', '13th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (247, 'MARD80', 'March streamflow exceeded 80 percent of the time', 'March 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (246, 'MARD75', 'March streamflow exceeded 75 percent of the time', 'March 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (245, 'MARD70', 'March streamflow exceeded 70 percent of the time', 'March 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (244, 'MARD65', 'March streamflow exceeded 65 percent of the time', 'March 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (243, 'MARD60', 'March streamflow exceeded 60 percent of the time', 'March 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (242, 'MARD55', 'March streamflow exceeded 55 percent of the time', 'March 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (241, 'MARD50', 'March streamflow exceeded 50 percent of the time', 'March 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (240, 'MARD45', 'March streamflow exceeded 45 percent of the time', 'March 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (248, 'MARD85', 'March streamflow exceeded 85 percent of the time', 'March 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (239, 'MARD40', 'March streamflow exceeded 40 percent of the time', 'March 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (237, 'MARD30', 'March streamflow exceeded 30 percent of the time', 'March 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (236, 'MARD25', 'March streamflow exceeded 25 percent of the time', 'March 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (235, 'MARD20', 'March streamflow exceeded 20 percent of the time', 'March 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (234, 'MARD15', 'March streamflow exceeded 15 percent of the time', 'March 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (233, 'MARD10', 'March streamflow exceeded 10 percent of the time', 'March 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (232, 'MARD7', 'March streamflow exceeded 7 percent of the time', 'March 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (231, 'MARD5', 'March streamflow exceeded 5 percent of the time', 'March 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (230, 'MARD3', 'March streamflow exceeded 3 percent of the time', 'March 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (238, 'MARD35', 'March streamflow exceeded 35 percent of the time', 'March 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (249, 'MARD90', 'March streamflow exceeded 90 percent of the time', 'March 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (250, 'MARD93', 'March streamflow exceeded 93 percent of the time', 'March 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (251, 'MARD95', 'March streamflow exceeded 95 percent of the time', 'March 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (270, 'APRD60', 'April streamflow exceeded 60 percent of the time', 'April 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (269, 'APRD55', 'April streamflow exceeded 55 percent of the time', 'April 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (268, 'APRD50', 'April streamflow exceeded 50 percent of the time', 'April 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (267, 'APRD45', 'April streamflow exceeded 45 percent of the time', 'April 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (266, 'APRD40', 'April streamflow exceeded 40 percent of the time', 'April 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (265, 'APRD35', 'April streamflow exceeded 35 percent of the time', 'April 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (264, 'APRD30', 'April streamflow exceeded 30 percent of the time', 'April 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (263, 'APRD25', 'April streamflow exceeded 25 percent of the time', 'April 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (262, 'APRD20', 'April streamflow exceeded 20 percent of the time', 'April 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (261, 'APRD15', 'April streamflow exceeded 15 percent of the time', 'April 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (260, 'APRD10', 'April streamflow exceeded 10 percent of the time', 'April 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (259, 'APRD7', 'April streamflow exceeded 7 percent of the time', 'April 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (258, 'APRD5', 'April streamflow exceeded 5 percent of the time', 'April 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (257, 'APRD3', 'April streamflow exceeded 3 percent of the time', 'April 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (256, 'APRD2', 'April streamflow exceeded 2 percent of the time', 'April 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (255, 'APRD1', 'Streamflow exceeded 1 percent of the time', 'April 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (254, 'MARD99', 'March streamflow exceeded 99 percent of the time', 'March 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (253, 'MARD98', 'March streamflow exceeded 98 percent of the time', 'March 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (252, 'MARD97', 'March streamflow exceeded 97 percent of the time', 'March 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (229, 'MARD2', 'March streamflow exceeded 2 percent of the time', 'March 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (228, 'MARD1', 'Streamflow exceeded 1 percent of the time', 'March 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (227, 'FEBD99', 'February streamflow exceeded 99 percent of the time', 'February 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (226, 'FEBD98', 'February streamflow exceeded 98 percent of the time', 'February 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (202, 'FEBD2', 'February streamflow exceeded 2 percent of the time', 'February 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (201, 'FEBD1', 'Streamflow exceeded 1 percent of the time', 'February 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (200, 'JAND99', 'January streamflow exceeded 99 percent of the time', 'January 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (199, 'JAND98', 'January streamflow exceeded 98 percent of the time', 'January 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (198, 'JAND97', 'January streamflow exceeded 97 percent of the time', 'January 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (197, 'JAND95', 'January streamflow exceeded 95 percent of the time', 'January 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (196, 'JAND93', 'January streamflow exceeded 93 percent of the time', 'January 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (195, 'JAND90', 'January streamflow exceeded 90 percent of the time', 'January 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (194, 'JAND85', 'January streamflow exceeded 85 percent of the time', 'January 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (193, 'JAND80', 'January streamflow exceeded 80 percent of the time', 'January 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (192, 'JAND75', 'January streamflow exceeded 75 percent of the time', 'January 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (191, 'JAND70', 'January streamflow exceeded 70 percent of the time', 'January 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (190, 'JAND65', 'January streamflow exceeded 65 percent of the time', 'January 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (189, 'JAND60', 'January streamflow exceeded 60 percent of the time', 'January 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (188, 'JAND55', 'January streamflow exceeded 55 percent of the time', 'January 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (187, 'JAND50', 'January streamflow exceeded 50 percent of the time', 'January 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (186, 'JAND45', 'January streamflow exceeded 45 percent of the time', 'January 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (185, 'JAND40', 'January streamflow exceeded 40 percent of the time', 'January 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (184, 'JAND35', 'January streamflow exceeded 35 percent of the time', 'January 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (203, 'FEBD3', 'February streamflow exceeded 3 percent of the time', 'February 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (271, 'APRD65', 'April streamflow exceeded 65 percent of the time', 'April 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (204, 'FEBD5', 'February streamflow exceeded 5 percent of the time', 'February 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (206, 'FEBD10', 'February streamflow exceeded 10 percent of the time', 'February 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (225, 'FEBD97', 'February streamflow exceeded 97 percent of the time', 'February 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (224, 'FEBD95', 'February streamflow exceeded 95 percent of the time', 'February 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (223, 'FEBD93', 'February streamflow exceeded 93 percent of the time', 'February 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (222, 'FEBD90', 'February streamflow exceeded 90 percent of the time', 'February 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (221, 'FEBD85', 'February streamflow exceeded 85 percent of the time', 'February 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (220, 'FEBD80', 'February streamflow exceeded 80 percent of the time', 'February 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (219, 'FEBD75', 'February streamflow exceeded 75 percent of the time', 'February 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (218, 'FEBD70', 'February streamflow exceeded 70 percent of the time', 'February 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (217, 'FEBD65', 'February streamflow exceeded 65 percent of the time', 'February 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (216, 'FEBD60', 'February streamflow exceeded 60 percent of the time', 'February 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (215, 'FEBD55', 'February streamflow exceeded 55 percent of the time', 'February 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (214, 'FEBD50', 'February streamflow exceeded 50 percent of the time', 'February 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (213, 'FEBD45', 'February streamflow exceeded 45 percent of the time', 'February 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (212, 'FEBD40', 'February streamflow exceeded 40 percent of the time', 'February 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (211, 'FEBD35', 'February streamflow exceeded 35 percent of the time', 'February 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (210, 'FEBD30', 'February streamflow exceeded 30 percent of the time', 'February 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (209, 'FEBD25', 'February streamflow exceeded 25 percent of the time', 'February 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (208, 'FEBD20', 'February streamflow exceeded 20 percent of the time', 'February 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (207, 'FEBD15', 'February streamflow exceeded 15 percent of the time', 'February 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (205, 'FEBD7', 'February streamflow exceeded 7 percent of the time', 'February 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (183, 'JAND30', 'January streamflow exceeded 30 percent of the time', 'January 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (272, 'APRD70', 'April streamflow exceeded 70 percent of the time', 'April 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (274, 'APRD80', 'April streamflow exceeded 80 percent of the time', 'April 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (338, 'JULD3', 'July streamflow exceeded 3 percent of the time', 'July 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (337, 'JULD2', 'July streamflow exceeded 2 percent of the time', 'July 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (336, 'JULD1', 'Streamflow exceeded 1 percent of the time', 'July 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (335, 'JUND99', 'June streamflow exceeded 99 percent of the time', 'June 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (334, 'JUND98', 'June streamflow exceeded 98 percent of the time', 'June 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (333, 'JUND97', 'June streamflow exceeded 97 percent of the time', 'June 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (332, 'JUND95', 'June streamflow exceeded 95 percent of the time', 'June 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (331, 'JUND93', 'June streamflow exceeded 93 percent of the time', 'June 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (339, 'JULD5', 'July streamflow exceeded 5 percent of the time', 'July 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (330, 'JUND90', 'June streamflow exceeded 90 percent of the time', 'June 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (328, 'JUND80', 'June streamflow exceeded 80 percent of the time', 'June 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (327, 'JUND75', 'June streamflow exceeded 75 percent of the time', 'June 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (326, 'JUND70', 'June streamflow exceeded 70 percent of the time', 'June 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (325, 'JUND65', 'June streamflow exceeded 65 percent of the time', 'June 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (324, 'JUND60', 'June streamflow exceeded 60 percent of the time', 'June 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (323, 'JUND55', 'June streamflow exceeded 55 percent of the time', 'June 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (322, 'JUND50', 'June streamflow exceeded 50 percent of the time', 'June 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (321, 'JUND45', 'June streamflow exceeded 45 percent of the time', 'June 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (329, 'JUND85', 'June streamflow exceeded 85 percent of the time', 'June 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (340, 'JULD7', 'July streamflow exceeded 7 percent of the time', 'July 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (341, 'JULD10', 'July streamflow exceeded 10 percent of the time', 'July 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (342, 'JULD15', 'July streamflow exceeded 15 percent of the time', 'July 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (361, 'JULD98', 'July streamflow exceeded 98 percent of the time', 'July 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (360, 'JULD97', 'July streamflow exceeded 97 percent of the time', 'July 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (359, 'JULD95', 'July streamflow exceeded 95 percent of the time', 'July 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (358, 'JULD93', 'July streamflow exceeded 93 percent of the time', 'July 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (357, 'JULD90', 'July streamflow exceeded 90 percent of the time', 'July 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (356, 'JULD85', 'July streamflow exceeded 85 percent of the time', 'July 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (355, 'JULD80', 'July streamflow exceeded 80 percent of the time', 'July 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (354, 'JULD75', 'July streamflow exceeded 75 percent of the time', 'July 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (353, 'JULD70', 'July streamflow exceeded 70 percent of the time', 'July 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (352, 'JULD65', 'July streamflow exceeded 65 percent of the time', 'July 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (351, 'JULD60', 'July streamflow exceeded 60 percent of the time', 'July 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (350, 'JULD55', 'July streamflow exceeded 55 percent of the time', 'July 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (349, 'JULD50', 'July streamflow exceeded 50 percent of the time', 'July 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (348, 'JULD45', 'July streamflow exceeded 45 percent of the time', 'July 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (347, 'JULD40', 'July streamflow exceeded 40 percent of the time', 'July 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (346, 'JULD35', 'July streamflow exceeded 35 percent of the time', 'July 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (345, 'JULD30', 'July streamflow exceeded 30 percent of the time', 'July 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (344, 'JULD25', 'July streamflow exceeded 25 percent of the time', 'July 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (343, 'JULD20', 'July streamflow exceeded 20 percent of the time', 'July 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (320, 'JUND40', 'June streamflow exceeded 40 percent of the time', 'June 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (319, 'JUND35', 'June streamflow exceeded 35 percent of the time', 'June 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (318, 'JUND30', 'June streamflow exceeded 30 percent of the time', 'June 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (317, 'JUND25', 'June streamflow exceeded 25 percent of the time', 'June 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (293, 'MAYD40', 'May streamflow exceeded 40 percent of the time', 'May 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (292, 'MAYD35', 'May streamflow exceeded 35 percent of the time', 'May 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (291, 'MAYD30', 'May streamflow exceeded 30 percent of the time', 'May 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (290, 'MAYD25', 'May streamflow exceeded 25 percent of the time', 'May 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (289, 'MAYD20', 'May streamflow exceeded 20 percent of the time', 'May 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (288, 'MAYD15', 'May streamflow exceeded 15 percent of the time', 'May 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (287, 'MAYD10', 'May streamflow exceeded 10 percent of the time', 'May 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (286, 'MAYD7', 'May streamflow exceeded 7 percent of the time', 'May 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (285, 'MAYD5', 'May streamflow exceeded 5 percent of the time', 'May 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (284, 'MAYD3', 'May streamflow exceeded 3 percent of the time', 'May 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (283, 'MAYD2', 'May streamflow exceeded 2 percent of the time', 'May 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (282, 'MAYD1', 'Streamflow exceeded 1 percent of the time', 'May 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (281, 'APRD99', 'April streamflow exceeded 99 percent of the time', 'April 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (280, 'APRD98', 'April streamflow exceeded 98 percent of the time', 'April 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (279, 'APRD97', 'April streamflow exceeded 97 percent of the time', 'April 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (278, 'APRD95', 'April streamflow exceeded 95 percent of the time', 'April 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (277, 'APRD93', 'April streamflow exceeded 93 percent of the time', 'April 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (276, 'APRD90', 'April streamflow exceeded 90 percent of the time', 'April 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (275, 'APRD85', 'April streamflow exceeded 85 percent of the time', 'April 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (294, 'MAYD45', 'May streamflow exceeded 45 percent of the time', 'May 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (273, 'APRD75', 'April streamflow exceeded 75 percent of the time', 'April 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (295, 'MAYD50', 'May streamflow exceeded 50 percent of the time', 'May 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (297, 'MAYD60', 'May streamflow exceeded 60 percent of the time', 'May 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (316, 'JUND20', 'June streamflow exceeded 20 percent of the time', 'June 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (315, 'JUND15', 'June streamflow exceeded 15 percent of the time', 'June 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (314, 'JUND10', 'June streamflow exceeded 10 percent of the time', 'June 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (313, 'JUND7', 'June streamflow exceeded 7 percent of the time', 'June 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (312, 'JUND5', 'June streamflow exceeded 5 percent of the time', 'June 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (311, 'JUND3', 'June streamflow exceeded 3 percent of the time', 'June 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (310, 'JUND2', 'June streamflow exceeded 2 percent of the time', 'June 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (309, 'JUND1', 'Streamflow exceeded 1 percent of the time', 'June 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (308, 'MAYD99', 'May streamflow exceeded 99 percent of the time', 'May 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (307, 'MAYD98', 'May streamflow exceeded 98 percent of the time', 'May 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (306, 'MAYD97', 'May streamflow exceeded 97 percent of the time', 'May 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (305, 'MAYD95', 'May streamflow exceeded 95 percent of the time', 'May 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (304, 'MAYD93', 'May streamflow exceeded 93 percent of the time', 'May 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (303, 'MAYD90', 'May streamflow exceeded 90 percent of the time', 'May 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (302, 'MAYD85', 'May streamflow exceeded 85 percent of the time', 'May 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (301, 'MAYD80', 'May streamflow exceeded 80 percent of the time', 'May 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (300, 'MAYD75', 'May streamflow exceeded 75 percent of the time', 'May 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (299, 'MAYD70', 'May streamflow exceeded 70 percent of the time', 'May 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (298, 'MAYD65', 'May streamflow exceeded 65 percent of the time', 'May 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (296, 'MAYD55', 'May streamflow exceeded 55 percent of the time', 'May 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (182, 'JAND25', 'January streamflow exceeded 25 percent of the time', 'January 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (181, 'JAND20', 'January streamflow exceeded 20 percent of the time', 'January 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (180, 'JAND15', 'January streamflow exceeded 15 percent of the time', 'January 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (64, 'V15D2Y', '15-Day mean maximum flow that occurs on average once in 2 years', '15 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (63, 'V7D500Y', '7-Day mean maximum flow that occurs on average once in 500 years', '7 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (62, 'V7D200Y', '7-Day mean maximum flow that occurs on average once in 200 years', '7 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (61, 'V7D100Y', '7-Day mean maximum flow that occurs on average once in 100 years', '7 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (60, 'V7D50Y', '7-Day mean maximum flow that occurs on average once in 50 years', '7 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (59, 'V7D25Y', '7-Day mean maximum flow that occurs on average once in 25 years', '7 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (58, 'V7D20Y', '7-Day mean maximum flow that occurs on average once in 20 years', '7 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (57, 'V7D10Y', '7-Day mean maximum flow that occurs on average once in 10 years', '7 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (65, 'V15D5Y', '15-Day mean maximum flow that occurs on average once in 5 years', '15 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (56, 'V7D5Y', '7-Day mean maximum flow that occurs on average once in 5 years', '7 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (54, 'V3D500Y', '3-Day mean maximum flow that occurs on average once in 500 years', '3 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (53, 'V3D200Y', '3-Day mean maximum flow that occurs on average once in 200 years', '3 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (52, 'V3D100Y', '3-Day mean maximum flow that occurs on average once in 100 years', '3 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (51, 'V3D50Y', '3-Day mean maximum flow that occurs on average once in 50 years', '3 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (50, 'V3D25Y', '3-Day mean maximum flow that occurs on average once in 25 years', '3 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (49, 'V3D20Y', '3-Day mean maximum flow that occurs on average once in 20 years', '3 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (48, 'V3D10Y', '3-Day mean maximum flow that occurs on average once in 10 years', '3 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (47, 'V3D5Y', '3-Day mean maximum flow that occurs on average once in 5 years', '3 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (55, 'V7D2Y', '7-Day mean maximum flow that occurs on average once in 2 years', '7 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (66, 'V15D10Y', '15-Day mean maximum flow that occurs on average once in 10 years', '15 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (67, 'V15D20Y', '15-Day mean maximum flow that occurs on average once in 20 years', '15 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (68, 'V15D25Y', '15-Day mean maximum flow that occurs on average once in 25 years', '15 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (87, 'M3D5Y', '3-Day mean low-flow that occurs on average once in 5 years', '3 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (86, 'M3D2Y', '3-Day mean low-flow that occurs on average once in 2 years', '3 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (85, 'M1D20Y', '1-Day mean low-flow that occurs on average once in 20 years', '1 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (84, 'M1D10Y', '1-Day mean low-flow that occurs on average once in 10 years', '1 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (83, 'M1D5Y', '1-Day mean low-flow that occurs on average once in 5 years', '1 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (82, 'M1D2Y', '1-Day mean low-flow that occurs on average once in 2 years', '1 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (81, 'V30D500Y', '30-Day mean maximum flow that occurs on average once in 500 years', '30 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (80, 'V30D200Y', '30-Day mean maximum flow that occurs on average once in 200 years', '30 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (79, 'V30D100Y', '30-Day mean maximum flow that occurs on average once in 100 years', '30 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (78, 'V30D50Y', '30-Day mean maximum flow that occurs on average once in 50 years', '30 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (77, 'V30D25Y', '30-Day mean maximum flow that occurs on average once in 25 years', '30 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (76, 'V30D20Y', '30-Day mean maximum flow that occurs on average once in 20 years', '30 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (75, 'V30D10Y', '30-Day mean maximum flow that occurs on average once in 10 years', '30 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (74, 'V30D5Y', '30-Day mean maximum flow that occurs on average once in 5 years', '30 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (73, 'V30D2Y', '30-Day mean maximum flow that occurs on average once in 2 years', '30 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (72, 'V15D500Y', '15-Day mean maximum flow that occurs on average once in 500 years', '15 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (71, 'V15D200Y', '15-Day mean maximum flow that occurs on average once in 200 years', '15 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (70, 'V15D100Y', '15-Day mean maximum flow that occurs on average once in 100 years', '15 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (69, 'V15D50Y', '15-Day mean maximum flow that occurs on average once in 50 years', '15 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (46, 'V3D2Y', '3-Day mean maximum flow that occurs on average once in 2 years', '3 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (45, 'V1D500Y', '1-Day mean maximum flow that occurs on average once in 500 years', '1 Day 500 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (44, 'V1D200Y', '1-Day mean maximum flow that occurs on average once in 200 years', '1 Day 200 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (43, 'V1D100Y', '1-Day mean maximum flow that occurs on average once in 100 years', '1 Day 100 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (19, 'PKMEANW', 'Weighted maximum instantaneous flow that occurs on average once in 1.25 years', 'Weighted Mean Annual Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (18, 'PK500R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 500 years', 'Regression 500 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (17, 'PK200R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 200 years', 'Regression 200 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (16, 'PK100R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 100 years', 'Regression 100 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (15, 'PK50R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 50 years', 'Regression 50 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (14, 'PK25R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 25 years', 'Regression 25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (13, 'PK10R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 10 years', 'Regression 10 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (12, 'PK5R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 5 years', 'Regression 5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (11, 'PK2R', 'Regression estimate of maximum instantaneous flow that occurs on average once in 2 years', 'Regression 2 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (10, 'PKMEANR', 'Regression estimate of maximum instantaneous flow that occurs on average once in 1.25 years', 'Regression Mean Annual Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (9, 'PK500', 'Maximum instantaneous flow that occurs on average once in 500 years', '500 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (8, 'PK200', 'Maximum instantaneous flow that occurs on average once in 200 years', '200 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (7, 'PK100', 'Maximum instantaneous flow that occurs on average once in 100 years', '100 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (6, 'PK50', 'Maximum instantaneous flow that occurs on average once in 50 years', '50 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (5, 'PK25', 'Maximum instantaneous flow that occurs on average once in 25 years', '25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (4, 'PK10', 'Maximum instantaneous flow that occurs on average once in 10 years', '10 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (3, 'PK5', 'Maximum instantaneous flow that occurs on average once in 5 years', '5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (2, 'PK2', 'Maximum instantaneous flow that occurs on average once in 2 years', '2 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (1, 'PKMEAN', 'Maximum instantaneous flow that occurs on average once in 2.33 years', 'Mean Annual Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (20, 'PK1_5W', 'Weighted maximum instantaneous flow that occurs on average once in 1.5 years', 'Weighted 1.5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (88, 'M3D10Y', '3-Day mean low-flow that occurs on average once in 10 years', '3 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (21, 'PK5W', 'Weighted maximum instantaneous flow that occurs on average once in 5 years', 'Weighted 5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (23, 'PK25W', 'Weighted maximum instantaneous flow that occurs on average once in 25 years', 'Weighted 25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (42, 'V1D50Y', '1-Day mean maximum flow that occurs on average once in 50 years', '1 Day 50 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (41, 'V1D25Y', '1-Day mean maximum flow that occurs on average once in 25 years', '1 Day 25 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (40, 'V1D10Y', '1-Day mean maximum flow that occurs on average once in 10 years', '1 Day 10 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (39, 'V1D5Y', '1-Day mean maximum flow that occurs on average once in 5 years', '1 Day 5 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (38, 'V1D20Y', '1-Day mean maximum flow that occurs on average once in 20 years', '1 Day 20 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (37, 'V1D2Y', '1-Day mean maximum flow that occurs on average once in 2 years', '1 Day 2 Year Maximum');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (36, 'YRSHISPK', 'Number of consecutive years used for historic-peak adjustment to flood-frequency data', 'Peak years with historic adjustment');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (35, 'YRSPK', 'Number of years of systematic peak flow record', 'Systematic peak years');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (34, 'PKEQYRS', 'Equivalent years of peak flow record', 'Equivalent Years of Peak Record');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (33, 'WRC_SKEW', 'WRC Skew', 'WRC Skew');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (32, 'WRC_STD', 'WRD Standard Deviation', 'WRC STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (31, 'WRC_MEAN', 'WRC Mean', 'WRC Mean');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (30, 'SKEWLOGPK', 'Skew of logarithms base 10 of systematic annual peak floods', 'Log Skew of Annual Peaks');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (29, 'SDLOGPK', 'Standard deviation of logarithms base 10 of systematic annual peak floods', 'Log STD of Annual Peaks');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (28, 'LOGMEANPK', 'Mean of logarithms base 10 of systematic annual peak floods', 'Log Mean of Annual Peaks');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (27, 'PK500W', 'Weighted maximum instantaneous flow that occurs on average once in 500 years', 'Weighted 500 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (26, 'PK200W', 'Weighted maximum instantaneous flow that occurs on average once in 200 years', 'Weighted 200 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (25, 'PK100W', 'Weighted maximum instantaneous flow that occurs on average once in 100 years', 'Weighted 100 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (24, 'PK50W', 'Weighted maximum instantaneous flow that occurs on average once in 50 years', 'Weighted 50 Year Peak Flood');

INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (22, 'PK10W', 'Weighted maximum instantaneous flow that occurs on average once in 10 years', 'Weighted 10 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (89, 'M3D20Y', '3-Day mean low-flow that occurs on average once in 20 years', '3 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (90, 'M7D2Y', '7-Day mean low-flow that occurs on average once in 2 years', '7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (91, 'M7D5Y', '7-Day mean low-flow that occurs on average once in 5 years', '7 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (156, 'Q10', 'Mean October Flow', 'October Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (155, 'SDQ9', 'Standard Deviation of September Flow', 'September STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (154, 'Q9', 'Mean September Flow', 'September Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (153, 'SDQ8', 'Standard Deviation of August Flow', 'August STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (152, 'Q8', 'Mean August Flow', 'August Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (151, 'SDQ7', 'Standard Deviation of July Flow', 'July STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (150, 'Q7', 'Mean July Flow', 'July Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (149, 'SDQ6', 'Standard Deviation of June Flow', 'June STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (148, 'Q6', 'Mean June Flow', 'June Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (147, 'SDQ5', 'Standard Deviation of May Flow', 'May STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (146, 'Q5', 'Mean May Flow', 'May Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (145, 'SDQ4', 'Standard Deviation of April Flow', 'April STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (144, 'Q4', 'Mean April Flow', 'April Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (143, 'SDQ3', 'Standard Deviation of March Flow', 'March STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (142, 'Q3', 'Mean March Flow', 'March Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (141, 'SDQ2', 'Standard Deviation of February Flow', 'February STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (140, 'Q2', 'Mean February Flow', 'February Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (139, 'SDQ1', 'Standard Deviation of January Flow', 'January STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (138, 'Q1', 'Mean January Flow', 'January Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (157, 'SDQ10', 'Standard Deviation of October Flow', 'October STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (137, 'YRSDAY', 'Number of years of daily flow record', 'Daily flow years');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (158, 'Q11', 'Mean November Flow', 'November Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (160, 'Q12', 'Mean December Flow', 'December Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (179, 'JAND10', 'January streamflow exceeded 10 percent of the time', 'January 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (178, 'JAND7', 'January streamflow exceeded 7 percent of the time', 'January 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (177, 'JAND5', 'January streamflow exceeded 5 percent of the time', 'January 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (176, 'JAND3', 'January streamflow exceeded 3 percent of the time', 'January 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (175, 'JAND2', 'January streamflow exceeded 2 percent of the time', 'January 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (174, 'JAND1', 'Streamflow exceeded 1 percent of the time', 'January 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (173, 'STDSUMR', 'Standard Deviation Of Summer Daily Flows', 'Stand Dev Of Summer Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (172, 'MEDSUMR', 'Summer Median Flow', 'Summer Median Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (171, 'MNSUMMER', 'Summer Mean Flow', 'Summer Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (170, 'STDSPRG', 'Standard Deviation Of Spring Daily Flows', 'Stand Dev Of Spring Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (169, 'MEDSPRG', 'Spring Median Flow', 'Spring Median Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (168, 'MNSPRING', 'Spring Mean Flow', 'Spring Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (167, 'STDWNTR', 'Standard Deviation Of Winter Daily Flows', 'Stand Dev Of Winter Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (166, 'MEDWNTR', 'Winter Median Flow', 'Winter Median Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (165, 'MNWINTER', 'Winter Mean Flow', 'Winter Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (164, 'STDFALL', 'Standard Deviation Of Fall Daily Flows', 'Stand Dev Of Fall Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (163, 'MEDFALL', 'Fall Median Flow', 'Fall Median Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (162, 'MNFALL', 'Fall Mean Flow', 'Fall Mean Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (161, 'SDQ12', 'Standard Deviation of December Flow', 'December STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (159, 'SDQ11', 'Standard Deviation of November Flow', 'November STD');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (362, 'JULD99', 'July streamflow exceeded 99 percent of the time', 'July 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (136, 'SDMEDAN', 'Standard deviation of median annual flow', 'Stand Dev of Median Annual Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (134, 'QA', 'Mean Annual Flow', 'Mean Annual Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (110, 'D5', 'Streamflow exceeded 5 percent of the time', '5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (109, 'D3', 'Streamflow exceeded 3 percent of the time', '3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (108, 'D2', 'Streamflow exceeded 2 percent of the time', '2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (107, 'D1', 'Streamflow exceeded 1 percent of the time', '1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (106, 'YRSLOW', 'Number of years of low-flow record', 'Low flow years');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (105, 'M90D20Y', '90-Day mean low-flow that occurs on average once in 20 years', '90 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (104, 'M90D10Y', '90-Day mean low-flow that occurs on average once in 10 years', '90 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (103, 'M90D5Y', '90-Day mean low-flow that occurs on average once in 5 years', '90 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (102, 'M90D2Y', '90-Day mean low-flow that occurs on average once in 2 years', '90 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (101, 'M30D20Y', '30-Day mean low-flow that occurs on average once in 20 years', '30 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (100, 'M30D10Y', '30-Day mean low-flow that occurs on average once in 10 years', '30 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (99, 'M30D5Y', '30-Day mean low-flow that occurs on average once in 5 years', '30 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (98, 'M30D2Y', '30-Day mean low-flow that occurs on average once in 2 years', '30 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (97, 'M14D20Y', '14-Day mean low-flow that occurs on average once in 20 years', '14 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (96, 'M14D10Y', '14-Day mean low-flow that occurs on average once in 10 years', '14 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (95, 'M14D5Y', '14-Day mean low-flow that occurs on average once in 5 years', '14 Day 5 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (94, 'M14D2Y', '14-Day mean low-flow that occurs on average once in 2 years', '14 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (93, 'M7D20Y', '7-Day mean low-flow that occurs on average once in 20 years', '7 Day 20 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (92, 'M7D10Y', '7-Day mean low-flow that occurs on average once in 10 years', '7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (111, 'D7', 'Streamflow exceeded 7 percent of the time', '7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (135, 'MEDAN', 'Median Annual Flow', 'Median Annual Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (112, 'D10', 'Streamflow exceeded 10 percent of the time', '10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (114, 'D20', 'Streamflow exceeded 20 percent of the time', '20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (133, 'D99', 'Streamflow exceeded 99 percent of the time', '99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (132, 'D98', 'Streamflow exceeded 98 percent of the time', '98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (131, 'D97', 'Streamflow exceeded 97 percent of the time', '97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (130, 'D95', 'Streamflow exceeded 95 percent of the time', '95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (129, 'D93', 'Streamflow exceeded 93 percent of the time', '93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (128, 'D90', 'Streamflow exceeded 90 percent of the time', '90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (127, 'D85', 'Streamflow exceeded 85 percent of the time', '85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (126, 'D80', 'Streamflow exceeded 80 percent of the time', '80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (125, 'D75', 'Streamflow exceeded 75 percent of the time', '75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (124, 'D70', 'Streamflow exceeded 70 percent of the time', '70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (123, 'D65', 'Streamflow exceeded 65 percent of the time', '65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (122, 'D60', 'Streamflow exceeded 60 percent of the time', '60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (121, 'D55', 'Streamflow exceeded 55 percent of the time', '55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (120, 'D50', 'Streamflow exceeded 50 percent of the time', '50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (119, 'D45', 'Streamflow exceeded 45 percent of the time', '45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (118, 'D40', 'Streamflow exceeded 40 percent of the time', '40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (117, 'D35', 'Streamflow exceeded 35 percent of the time', '35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (116, 'D30', 'Streamflow exceeded 30 percent of the time', '30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (115, 'D25', 'Streamflow exceeded 25 percent of the time', '25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (727, 'FPS14', 'Streamflow not exceeded 14 percent of the time', '14th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (363, 'AUGD1', 'Streamflow exceeded 1 percent of the time', 'August 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (113, 'D15', 'Streamflow exceeded 15 percent of the time', '15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (365, 'AUGD3', 'August streamflow exceeded 3 percent of the time', 'August 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (611, 'M30D50Y511', '30 Day 50 Year lowflow May-Nov', '30 Day 50 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (610, 'M30D20Y511', '30 Day 20 Year lowflow May-Nov', '30 Day 20 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (609, 'M30D10Y511', '30 Day 10 Year lowflow May-Nov', '30 Day 10 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (608, 'M30D5Y511', '30 Day 5 Year lowflow May-Nov', '30 Day 5 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (607, 'M30D2Y511', '30 Day 2 Year lowflow May-Nov', '30 Day 2 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (606, 'M7D50Y0511', '7 Day 50 Year lowflow May-Nov', '7 Day 50 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (605, 'M7D20Y0511', '7 Day 20 Year lowflow May-Nov', '7 Day 20 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (604, 'M7D10Y0511', '7 Day 10 Year lowflow May-Nov', '7 Day 10 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (612, 'M90D2Y511', '90 Day 2 Year lowflow May-Nov', '90 Day 2 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (603, 'M7D5Y0511', '7 Day 5 Year lowflow May-Nov', '7 Day 5 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (601, 'M1D50Y0511', '1 Day 50 Year lowflow May-Nov', '1 Day 50 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (600, 'M1D20Y0511', '1 Day 20 Year lowflow May-Nov', '1 Day 20 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (599, 'M1D10Y0511', '1 Day 10 Year lowflow May-Nov', '1 Day 10 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (598, 'M1D5Y0511', '1 Day 5 Year lowflow May-Nov', '1 Day 5 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (597, 'M1D2Y0511', '1 Day 2 Year lowflow May-Nov', '1 Day 2 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (596, 'M1D50Y', '1 Day 50 Year lowflow computed using climatic years Apr-Mar', '1 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (595, 'LAGTIME', 'Elapsed time from center of mass of rainfall excess to center of mass of flood hydrograph', 'Lagtime');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (594, 'WM7D10Y', '7-Day mean low-flow that occurs on average once in 10 years during November through March', 'Winter 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (602, 'M7D2Y0511', '7 Day 2 Year lowflow May-Nov', '7 Day 2 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (613, 'M90D5Y511', '90 Day 5 Year lowflow May-Nov', '90 Day 5 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (614, 'M90D10Y511', '90 Day 10 Year lowflow May-Nov', '90 Day 10 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (615, 'M90D20Y511', '90 Day 20 Year lowflow May-Nov', '90 Day 20 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (634, 'M90D10Y122', '90 Day 10 Year lowflow Dec-Feb', '90 Day 10 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (633, 'M90D5Y1202', '90 Day 5 Year lowflow Dec-Feb', '90 Day 5 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (632, 'M90D2Y1202', '90 Day 2 Year lowflow Dec-Feb', '90 Day 2 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (631, 'M30D50Y122', '30 Day 50 Year lowflow Dec-Feb', '30 Day 50 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (630, 'M30D20Y122', '30 Day 20 Year lowflow Dec-Feb', '30 Day 20 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (629, 'M30D10Y122', '30 Day 10 Year lowflow Dec-Feb', '30 Day 10 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (628, 'M30D5Y1202', '30 Day 5 Year lowflow Dec-Feb', '30 Day 5 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (627, 'M30D2Y1202', '30 Day 2 Year lowflow Dec-Feb', '30 Day 2 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (626, 'M7D50Y1202', '7 Day 50 Year lowflow Dec-Feb', '7 Day 50 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (625, 'M7D20Y1202', '7 Day 20 Year lowflow Dec-Feb', '7 Day 20 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (624, 'M7D10Y1202', '7 Day 10 Year lowflow Dec-Feb', '7 Day 10 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (623, 'M7D5Y1202', '7 Day 5 Year lowflow Dec-Feb', '7 Day 5 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (622, 'M7D2Y1202', '7 Day 2 Year lowflow Dec-Feb', '7 Day 2 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (621, 'M1D50Y1202', '1 Day 50 Year lowflow Dec-Feb', '1 Day 50 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (620, 'M1D20Y1202', '1 Day 20 Year lowflow Dec-Feb', '1 Day 20 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (619, 'M1D10Y1202', '1 Day 10 Year lowflow Dec-Feb', '1 Day 10 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (618, 'M1D5Y1202', '1 Day 5 Year lowflow Dec-Feb', '1 Day 5 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (617, 'M1D2Y1202', '1 Day 2 Year lowflow Dec-Feb', '1 Day 2 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (616, 'M90D50Y511', '90 Day 50 Year lowflow May-Nov', '90 Day 50 Year lowflow May to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (593, 'BF365D50Y', 'Mean annual base flow determined through hydrograph separation that occurs on average once in 50 years', '50 Year Base Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (592, 'BF365D25Y', 'Mean annual base flow determined through hydrograph separation that occurs on average once in 25 years', '25 Year Base Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (591, 'BF365D10Y', 'Mean annual base flow determined through hydrograph separation that occurs on average once in 10 years', '10 Year Base Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (590, 'RECESS', 'Number of days required for streamflow to recede one order of magnitude when hydrograph is plotted on logarithmic scale', 'Recession Index');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (566, 'D70SUM', 'June 1 to October 31 flow exceeded 70 percent of the time', 'Jun to Oct 70 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (565, 'D60SUM', 'June 1 to October 31 flow exceeded 60 percent of the time', 'Jun to Oct 60 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (564, 'D98SPR', 'March 16 to May 31 flow exceeded 98 percent of the time', 'Mar16 to May 98 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (563, 'D95SPR', 'March 16 to May 31 flow exceeded 95 percent of the time', 'Mar16 to May 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (562, 'D90SPR', 'March 16 to May 31 flow exceeded 90 percent of the time', 'Mar16 to May 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (561, 'D80SPR', 'March 16 to May 31 flow exceeded 80 percent of the time', 'Mar16 to May 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (560, 'D70SPR', 'March 16 to May 31 flow exceeded 70 percent of the time', 'Mar16 to May 70 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (559, 'D60SPR', 'March 16 to May 31 flow exceeded 60 percent of the tme', 'Mar16 to May 60 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (558, 'D98WIN', 'January 1 to March 1 flow exceeded 98 percent of the time', 'Jan to Mar15 98 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (557, 'D95WIN', 'January 1 to March 15 flow exceeded 95 percent of the time', 'Jan to Mar15 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (556, 'D90WIN', 'January 1 to March 15 flow exceeded 90 percent of the time', 'Jan to Mar15 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (555, 'D70WIN', 'January 1 to March 15 flow exceeded 70 percent of the time', 'Jan to Mar15 70 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (554, 'D60WIN', 'January 1 to March 15 flow exceeded 60 percent of the time', 'Jan to Mar15 60 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (553, 'M4D3Y', '4-day mean low flow that occurs an average of once in 3 years', '4 Day 3 Year Low FLow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (552, 'M7D2Y0709', 'July to September 7-day mean flow that occurs an average of once in 2 years', 'July to Sept 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (551, 'M7D10Y0709', 'July to September 7-day mean flow occurring an average of once in 10 years', 'July to Sept 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (550, 'D50_07_09', 'July to September flow exceeded 50 percent of the time', 'July to Sept 50 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (549, 'D60_07_09', 'July to September flow exceeded 60 percent of the time', 'July to Sept 60 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (548, 'D70_07_09', 'July to September flow exceeded 70 percent of the time', 'July to Sept 70 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (567, 'D80SUM', 'June 1 to October 31 flow exceeded 80 percent of the time', 'Jun to Oct 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (635, 'M90D20Y122', '90 Day 20 Year lowflow Dec-Feb', '90 Day 20 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (568, 'D90SUM', 'June 1 to October 31 flpw exceeded 90 percent of the time', 'Jun to Oct 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (570, 'D98SUM', 'June 1 to October 31 flow exceeded 98 percent of the time', 'Jun to Oct 98 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (589, 'M7D50Y', '7-Day mean low-flow that occurs on average once in 50 years', '7 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (588, 'BFI', 'Proportion of mean annual flow that is from ground water (base flow)', 'Base Flow Index');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (587, 'QAH', 'Harmonic mean flow', 'Harmonic Mean Streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (586, 'STREAM_VARC', 'Streamflow variability index as defined in WRIR 02-4068, computed from at-site data', 'Streamflow Variability Index At Site');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (585, 'D80WIN', 'January 1 to March 15 flow exceeded 80 percent of the time', 'Jan to Mar15 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (584, 'M7D10Y_FAL', 'October to November fall period 7-day 10-year low flow', 'Oct to Nov 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (583, 'M7D2Y_FAL', 'October to November fall period 7-day 2-year low flow', 'Oct to Nov 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (582, 'M7D10Y_SUM', 'June to October summer period 7-day 10-year low flow', 'Jun to Oct 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (581, 'M7D2Y_SUM', 'June to October summer period 7-day 2-year low flow', 'Jun to Oct 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (580, 'M7D10Y_SPR', 'March 16 to May 31 spring period 7-day 10-year low flow', 'Mar16 to May 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (579, 'M7D2Y_SPR', 'March 16 to May 31 spring period 7-day 2-year low flow', 'Mar16 to May 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (578, 'M7D10Y_WIN', 'January 1 to March 15 winter period 7-day 10-year low flow', 'Jan to Mar15 7 Day 10 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (577, 'M7D2Y_WIN', 'January1 to March 16 winter period 7-day 10-year low flow', 'Jan to Mar15 7 Day 2 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (576, 'D98FALL', 'November to December flow exceeded 98 percent of the time', 'Nov to Dec 98 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (575, 'D95FALL', 'November to December flow exceeded 95 percent of the time', 'Nov to Dec 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (574, 'D90FALL', 'November to December flow exceeded 90 percent of the time', 'Nov to Dec 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (573, 'D80FALL', 'November to December flow exceeded 80 percent of the time', 'Nov to Dec 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (572, 'D70FALL', 'November to December flow exceeded 70 percent of the time', 'Nov to Dec 70 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (571, 'D60FALL', 'November to December flow exceeded 60 percent of the time', 'Nov to Dec 60 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (569, 'D95SUM', 'June 1 to October 31 flow exceeded 95 percent of the time', 'Jun to Oct 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (547, 'D80_07_09', 'July to September flow exceeded 80 percent of the time', 'July to Sept 80 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (636, 'M90D50Y122', '90 Day 50 Year lowflow Dec-Feb', '90 Day 50 Year lowflow Dec to Feb');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (638, 'M1D5Y0911', '1 Day 5 Year lowflow Sep-Nov', '1 Day 5 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (703, 'D75_09_11', '75 Percent Duration SEP NOV', '75 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (702, 'D80_09_11', '80 Percent Duration SEP NOV', '80 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (701, 'D85_09_11', '85 Percent Duration SEP NOV', '85 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (700, 'D90_09_11', '90 Percent Duration SEP NOV', '90 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (699, 'D95_09_11', '95 Percent Duration SEP NOV', '95 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (698, 'D98_09_11', '98 Percent Duration SEP NOV', '98 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (697, 'D10_12_02', '10 Percent Duration DEC FEB', '10 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (696, 'D20_12_02', '20 Percent Duration DEC FEB', '20 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (704, 'D70_09_11', '70 Percent Duration SEP NOV', '70 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (695, 'D30_12_02', '30 Percent Duration DEC FEB', '30 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (693, 'D50_12_02', 'Streamflow exceeded 50 percent of the time during December to February', '50 Percent Duration December to February');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (692, 'D60_12_02', '60 Percent Duration DEC FEB', '60 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (691, 'D70_12_02', '70 Percent Duration DEC FEB', '70 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (690, 'D75_12_02', 'Streamflow exceeded 75 percent of the time during December to February', '75 Percent Duration December to February');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (689, 'D80_12_02', '80 Percent Duration DEC FEB', '80 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (688, 'D85_12_02', '85 Percent Duration DEC FEB', '85 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (687, 'D90_12_02', '90 Percent Duration DEC FEB', '90 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (686, 'D95_12_02', 'Streamflow exceeded 95 percent of the time during December to February', '95 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (694, 'D40_12_02', '40 Percent Duration DEC FEB', '40 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (705, 'D60_09_11', '60 Percent Duration SEP NOV', '60 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (706, 'D50_09_11', '50 Percent Duration SEP NOV', '50 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (707, 'D40_09_11', '40 Percent Duration SEP NOV', '40 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (364, 'AUGD2', 'August streamflow exceeded 2 percent of the time', 'August 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (725, 'FPS12', 'Streamflow not exceeded 12 percent of the time', '12th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (724, 'FPS11', 'Streamflow not exceeded 11 percent of the time.', '11th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (723, 'FPS10', 'Streamflow not exceeded 10 percent of the time', '10th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (722, 'FPS9', 'Streamflow not exceeded 9 percent of the time', '9th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (721, 'FPS8', 'Streamflow not exceeded 8 percent of the time', '8th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (720, 'FPS7', 'Streamflow not exceeded 7 percent of the time', '7th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (719, 'FPS6', 'Streamflow not exceeded 6 percent of the time', '6th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (718, 'FPS5', 'Streamflow not exceeded 5 percent of the time', '5th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (717, 'FPS4', 'Streamflow not exceeded 4 percent of the time', '4th Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (716, 'FPS3', 'Streamflow not exceeded 3 percent of the time', '3rd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (715, 'FPS2', 'Streamflow not exceeded 2 percent of the time', '2nd Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (714, 'FPS1', 'Streamflow not exceeded one percent ot the time', '1st Percentile Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (713, 'BF50YR', 'Base flow component of streamflow that occurs on average once in 50 years', 'Base Flow 50 Year Recurrence Interval');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (712, 'BF25YR', 'Base flow component of streamflow that occurs on average once in 25 years', 'Base Flow 25 Year Recurrence Interval');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (711, 'BF10YR', 'Base flow component of streamflow that occurs on average once in 10 years', 'Base Flow 10 Year Recurrence Interval');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (710, 'D10_09_11', '10 Percent Duration SEP NOV', '10 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (709, 'D20_09_11', '20 Percent Duration SEP NOV', '20 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (708, 'D30_09_11', '30 Percent Duration SEP NOV', '30 Percent Duration SEP NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (685, 'D98_12_02', '98 Percent Duration DEC FEB', '98 Percent Duration DEC FEB');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (684, 'D10_05_11', '10 Percent Duration MAY NOV', '10 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (683, 'D20_05_11', '20 Percent Duration MAY NOV', '20 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (682, 'D30_05_11', '30 Percent Duration MAY NOV', '30 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (657, 'M30D50Y', '30 Day 50 Year low flow computed based on climatic years Apr-Mar', '30 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (656, 'M90D50Y911', '90 Day 50 Year lowflow Sep-Nov', '90 Day 50 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (655, 'M90D20Y911', '90 Day 20 Year lowflow Sep-Nov', '90 Day 20 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (654, 'M90D10Y911', '90 Day 10 Year lowflow Sep-Nov', '90 Day 10 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (653, 'M90D5Y0911', '90 Day 5 Year lowflow Sep-Nov', '90 Day 5 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (652, 'M90D2Y0911', '90 Day 2 Year lowflow Sep-Nov', '90 Day 2 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (651, 'M30D50Y911', '30 Day 50 Year lowflow Sep-Nov', '30 Day 50 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (650, 'M30D20Y911', '30 Day 20 Year lowflow Sep-Nov', '30 Day 20 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (649, 'M30D10Y911', '30 Day 10 Year lowflow Sep-Nov', '30 Day 10 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (648, 'M30D5Y0911', '30 Day 5 Year lowflow Sep-Nov', '30 Day 5 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (647, 'M30D2Y0911', '30 Day 2 Year lowflow Sep-Nov', '30 Day 2 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (646, 'M7D50Y0911', '7 Day 50 Year lowflow Sep-Nov', '7 Day 50 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (645, 'M7D20Y0911', '7 Day 20 Year lowflow Sep-Nov', '7 Day 20 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (644, 'M7D10Y0911', '7 Day 10 Year lowflow Sep-Nov', '7 Day 10 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (643, 'M7D5Y0911', '7 Day 5 Year lowflow Sep-Nov', '7 Day 5 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (642, 'M7D2Y0911', '7 Day 2 Year lowflow Sep-Nov', '7 Day 2 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (641, 'M1D50Y0911', '1 Day 50 Year lowflow Sep-Nov', '1 Day 50 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (640, 'M1D20Y0911', '1 Day 20 Year lowflow Sep-Nov', '1 Day 20 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (639, 'M1D10Y0911', '1 Day 10 Year lowflow Sep-Nov', '1 Day 10 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (658, 'M90D50Y', '90 Day 50 Year low flow computed from climatic years Apr-Mar', '90 Day 50 Year Low Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (637, 'M1D2Y0911', '1 Day 2 Year lowflow Sep-Nov', '1 Day 2 Year lowflow Sep to Nov');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (659, 'D98_04_03', '98 Percent Duration APR MAR', '98 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (661, 'D90_04_03', '90 Percent Duration APR MAR', '90 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (681, 'D40_05_11', '40 Percent Duration MAY NOV', '40 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (680, 'D50_05_11', '50 Percent Duration MAY NOV', '50 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (679, 'D60_05_11', '60 Percent Duration MAY NOV', '60 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (677, 'D75_05_11', '75 Percent Duration MAY NOV', '75 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (676, 'D80_05_11', '80 Percent Duration MAY NOV', '80 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (675, 'D85_05_11', '85 Percent Duration MAY NOV', '85 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (674, 'D90_05_11', '90 Percent Duration MAY NOV', '90 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (673, 'D95_05_11', '95 Percent Duration MAY NOV', '95 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (672, 'D98_05_11', '98 Percent Duration MAY NOV', '98 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (671, 'D10_04_03', '10 Percent Duration APR MAR', '10 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (670, 'D20_04_03', '20 Percent Duration APR MAR', '20 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (669, 'D30_04_03', '30 Percent Duration APR MAR', '30 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (668, 'D40_04_03', '40 Percent Duration APR MAR', '40 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (667, 'D50_04_03', '50 Percent Duration APR MAR', '50 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (666, 'D60_04_03', '60 Percent Duration APR MAR', '60 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (665, 'D70_04_03', '70 Percent Duration APR MAR', '70 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (664, 'D75_04_03', '75 Percent Duration APR MAR', '75 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (663, 'D80_04_03', '80 Percent Duration APR MAR', '80 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (662, 'D85_04_03', '85 Percent Duration APR MAR', '85 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (660, 'D95_04_03', '95 Percent Duration APR MAR', '95 Percent Duration APR MAR');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (546, 'D90_07_09', 'July to September flow exceeded 90 percent of the time', 'July to Sept 90 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (678, 'D70_05_11', '70 Percent Duration MAY NOV', '70 Percent Duration MAY NOV');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (544, 'D98_07_09', 'July to September flow exceeded 98-percent of the time', 'July to Sept 98 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (429, 'OCTD45', 'October streamflow exceeded 45 percent of the time', 'October 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (428, 'OCTD40', 'October streamflow exceeded 40 percent of the time', 'October 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (427, 'OCTD35', 'October streamflow exceeded 35 percent of the time', 'October 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (426, 'OCTD30', 'October streamflow exceeded 30 percent of the time', 'October 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (425, 'OCTD25', 'October streamflow exceeded 25 percent of the time', 'October 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (424, 'OCTD20', 'October streamflow exceeded 20 percent of the time', 'October 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (423, 'OCTD15', 'October streamflow exceeded 15 percent of the time', 'October 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (422, 'OCTD10', 'October streamflow exceeded 10 percent of the time', 'October 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (430, 'OCTD50', 'October streamflow exceeded 50 percent of the time', 'October 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (421, 'OCTD7', 'October streamflow exceeded 7 percent of the time', 'October 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (419, 'OCTD3', 'October streamflow exceeded 3 percent of the time', 'October 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (418, 'OCTD2', 'October streamflow exceeded 2 percent of the time', 'October 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (417, 'OCTD1', 'Streamflow exceeded 1 percent of the time', 'October 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (416, 'SEPD99', 'September streamflow exceeded 99 percent of the time', 'September 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (415, 'SEPD98', 'September streamflow exceeded 98 percent of the time', 'September 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (414, 'SEPD97', 'September streamflow exceeded 97 percent of the time', 'September 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (413, 'SEPD95', 'September streamflow exceeded 95 percent of the time', 'September 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (412, 'SEPD93', 'September streamflow exceeded 93 percent of the time', 'September 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (420, 'OCTD5', 'October streamflow exceeded 5 percent of the time', 'October 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (431, 'OCTD55', 'October streamflow exceeded 55 percent of the time', 'October 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (432, 'OCTD60', 'October streamflow exceeded 60 percent of the time', 'October 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (433, 'OCTD65', 'October streamflow exceeded 65 percent of the time', 'October 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (452, 'NOVD25', 'November streamflow exceeded 25 percent of the time', 'November 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (451, 'NOVD20', 'November streamflow exceeded 20 percent of the time', 'November 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (450, 'NOVD15', 'November streamflow exceeded 15 percent of the time', 'November 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (449, 'NOVD10', 'November streamflow exceeded 10 percent of the time', 'November 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (448, 'NOVD7', 'November streamflow exceeded 7 percent of the time', 'November 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (447, 'NOVD5', 'November streamflow exceeded 5 percent of the time', 'November 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (446, 'NOVD3', 'November streamflow exceeded 3 percent of the time', 'November 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (445, 'NOVD2', 'November streamflow exceeded 2 percent of the time', 'November 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (444, 'NOVD1', 'Streamflow exceeded 1 percent of the time', 'November 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (443, 'OCTD99', 'October streamflow exceeded 99 percent of the time', 'October 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (442, 'OCTD98', 'October streamflow exceeded 98 percent of the time', 'October 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (441, 'OCTD97', 'October streamflow exceeded 97 percent of the time', 'October 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (440, 'OCTD95', 'October streamflow exceeded 95 percent of the time', 'October 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (439, 'OCTD93', 'October streamflow exceeded 93 percent of the time', 'October 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (438, 'OCTD90', 'October streamflow exceeded 90 percent of the time', 'October 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (437, 'OCTD85', 'October streamflow exceeded 85 percent of the time', 'October 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (436, 'OCTD80', 'October streamflow exceeded 80 percent of the time', 'October 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (435, 'OCTD75', 'October streamflow exceeded 75 percent of the time', 'October 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (434, 'OCTD70', 'October streamflow exceeded 70 percent of the time', 'October 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (411, 'SEPD90', 'September streamflow exceeded 90 percent of the time', 'September 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (410, 'SEPD85', 'September streamflow exceeded 85 percent of the time', 'September 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (409, 'SEPD80', 'September streamflow exceeded 80 percent of the time', 'September 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (408, 'SEPD75', 'September streamflow exceeded 75 percent of the time', 'September 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (384, 'AUGD90', 'August streamflow exceeded 90 percent of the time', 'August 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (383, 'AUGD85', 'August streamflow exceeded 85 percent of the time', 'August 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (381, 'AUGD75', 'August streamflow exceeded 75 percent of the time', 'August 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (380, 'AUGD70', 'August streamflow exceeded 70 percent of the time', 'August 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (379, 'AUGD65', 'August streamflow exceeded 65 percent of the time', 'August 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (378, 'AUGD60', 'August streamflow exceeded 60 percent of the time', 'August 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (377, 'AUGD55', 'August streamflow exceeded 55 percent of the time', 'August 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (376, 'AUGD50', 'August streamflow exceeded 50 percent of the time', 'August 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (375, 'AUGD45', 'August streamflow exceeded 45 percent of the time', 'August 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (374, 'AUGD40', 'August streamflow exceeded 40 percent of the time', 'August 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (373, 'AUGD35', 'August streamflow exceeded 35 percent of the time', 'August 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (372, 'AUGD30', 'August streamflow exceeded 30 percent of the time', 'August 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (371, 'AUGD25', 'August streamflow exceeded 25 percent of the time', 'August 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (370, 'AUGD20', 'August streamflow exceeded 20 percent of the time', 'August 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (369, 'AUGD15', 'August streamflow exceeded 15 percent of the time', 'August 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (368, 'AUGD10', 'August streamflow exceeded 10 percent of the time', 'August 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (367, 'AUGD7', 'August streamflow exceeded 7 percent of the time', 'August 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (545, 'D95_07_09', 'July to September flow exceeded 95 percent of the time', 'July to Sept 95 Percent Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (366, 'AUGD5', 'August streamflow exceeded 5 percent of the time', 'August 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (385, 'AUGD93', 'August streamflow exceeded 93 percent of the time', 'August 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (453, 'NOVD30', 'November streamflow exceeded 30 percent of the time', 'November 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (386, 'AUGD95', 'August streamflow exceeded 95 percent of the time', 'August 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (388, 'AUGD98', 'August streamflow exceeded 98 percent of the time', 'August 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (407, 'SEPD70', 'September streamflow exceeded 70 percent of the time', 'September 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (406, 'SEPD65', 'September streamflow exceeded 65 percent of the time', 'September 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (405, 'SEPD60', 'September streamflow exceeded 60 percent of the time', 'September 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (404, 'SEPD55', 'September streamflow exceeded 55 percent of the time', 'September 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (403, 'SEPD50', 'September streamflow exceeded 50 percent of the time', 'September 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (402, 'SEPD45', 'September streamflow exceeded 45 percent of the time', 'September 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (401, 'SEPD40', 'September streamflow exceeded 40 percent of the time', 'September 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (400, 'SEPD35', 'September streamflow exceeded 35 percent of the time', 'September 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (399, 'SEPD30', 'September streamflow exceeded 30 percent of the time', 'September 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (398, 'SEPD25', 'September streamflow exceeded 25 percent of the time', 'September 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (397, 'SEPD20', 'September streamflow exceeded 20 percent of the time', 'September 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (396, 'SEPD15', 'September streamflow exceeded 15 percent of the time', 'September 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (395, 'SEPD10', 'September streamflow exceeded 10 percent of the time', 'September 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (394, 'SEPD7', 'September streamflow exceeded 7 percent of the time', 'September 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (393, 'SEPD5', 'September streamflow exceeded 5 percent of the time', 'September 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (392, 'SEPD3', 'September streamflow exceeded 3 percent of the time', 'September 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (391, 'SEPD2', 'September streamflow exceeded 2 percent of the time', 'September 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (390, 'SEPD1', 'Streamflow exceeded 1 percent of the time', 'September 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (389, 'AUGD99', 'August streamflow exceeded 99 percent of the time', 'August 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (387, 'AUGD97', 'August streamflow exceeded 97 percent of the time', 'August 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (454, 'NOVD35', 'November streamflow exceeded 35 percent of the time', 'November 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (382, 'AUGD80', 'August streamflow exceeded 80 percent of the time', 'August 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (456, 'NOVD45', 'November streamflow exceeded 45 percent of the time', 'November 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (519, 'PK5C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 5 years', 'Controlled 5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (518, 'PK2_33C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 2.33 years', 'Controlled 2.33 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (517, 'PK1_5C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 1.5 years', 'Controlled 1.5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (516, 'PK2C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 2 years', 'Controlled 2 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (515, 'PK2_33R', 'Regression estimate of maximum regulated instantaneous flow that occurs on average once in 2.33 years', 'Regression 2.33 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (514, 'PK1_5R', 'Regression estimate of maximum regulated instantaneous flow that occurs on average once in 1.5 years', 'Regression 1.5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (513, 'PK2W', 'Weighted maximum instantaneous flow that occurs on average once in 2 years', 'Weighted 2 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (512, 'PK2_33W', 'Weighted maximum instantaneous flow that occurs on average once in 2.33 years', 'Weighted 2.33 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (520, 'PK10C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 10 years', 'Controlled 10 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (511, 'BFI_STDEV', 'Standard deviation of annual BFI values', 'Std dev of annual BFI values');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (509, 'BFI_YRS', 'Number of complete years of record used to compute Base Flow Index', 'Number of years to compute BFI');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (508, 'AVE_DV', 'Average of mean daily streamflows for period of record', 'Average daily streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (507, 'SDDV', 'Standard deviation of daily mean flows for period of record', 'Std Dev of daily flows');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (506, 'MAX_INST', 'Maximum instantaneous streamflow for period of record', 'Maximum instantaneous streamflow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (504, 'MAXDV', 'Maximum daily mean streamflow for period of record', 'Maximum daily flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (503, 'MINDV', 'Minimum daily mean streamflow for period of record', 'Minimum daily flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (502, 'D99_5', 'Streamflow exceeded 99.5 percent of the time', '99.5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (501, 'PK2_33', 'Maximum instantaneous flow that occurs on average once in 2.33 years', '2 33 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (510, 'BFI_AVE', 'Average BFI value for all years', 'Average BFI value');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (521, 'PK25C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 25 years', 'Controlled 25 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (522, 'PK50C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 50 years', 'Controlled 50 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (523, 'PK100C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 100 years', 'Controlled 100 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (455, 'NOVD40', 'November streamflow exceeded 40 percent of the time', 'November 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (541, 'M30D2Yp05', '30-day 2-year low flow plus 0.05 (Maryland)', '30 Day 2 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (540, 'M14D20Yp05', '14-day 20-year low flow plus 0.05 (Maryland)', '14 Day 20 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (539, 'M14D10Yp05', '14-day 10-year low flow plus 0.05 (Maryland)', '14 Day 10 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (538, 'M14D2Yp05', '14-day 2-year low flow plus 0.05 (Maryland)', '14 Day 2 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (537, 'M7D20Yp05', '7-day 20-year low flow plus 0.05 (Maryland)', '7 Day 20 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (536, 'M7D10Yp05', '7-day, 10-year low flow plus 0.05 (Maryland)', '7 Day 1 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (535, 'M7D2Yp05', '7-day, 2-year low flow plus 0.05 (Maryland)', '7 Day 2 Year Low Flow add 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (534, 'D10p4_0', 'Flow exceeded 10 percent of the time plus 4.0 (Kansas)', '10 Percent Duration add 4.0');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (533, 'D25p2_5', 'Flow exceeded 25 percent of the time plus 2.5 (Kansas)', '25 Percent Duration add 2.5');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (532, 'D50p1_2', 'Flow exceeded 50 percent of the time plus 1.2 (Kansas)', '50 Percent Duration add 1.2');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (531, 'D75p1_5', 'Flow exceeded 75 percent of the time plus 1.5 (Kansas)', '75 Percent Duration add 1.5');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (530, 'D90p2_0', 'Flow exceeded 90 percent of the time plus 2.0 (Kansas)', '90 Percent Duration add 2.0');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (529, 'QMEANa1_5', 'Mean annual flow + 1.5 (Kansas)', 'Mean Annual Flow add 1.5');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (528, 'MEDAAHa1_2', 'Median annual flow for period of record + 1.2 (Kansas)', 'Median Annual Flow AAH add 1.2');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (527, 'MEDKSAa1_4', 'Median annual flow for most recent 10-year period +1.4 (Kansas)', 'Median Annual Flow KSA add 1.4');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (526, 'FDRATIO', '20-percent flow duration divided by 90-percent flow duration', 'Flow Duration Ratio');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (525, 'PK500C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 500 years', 'Controlled 500 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (524, 'PK200C', 'Estimate of regulated maximum instantaneous flow that occurs on average once in 200 years', 'Controlled 200 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (500, 'PK1_5', 'Maximum instantaneous flow that occurs on average once in 1.5 years', '1.5 Year Peak Flood');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (499, 'SDQA', 'Standard deviation of mean annual flow', 'Stand Dev of Mean Annual Flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (505, 'MIN_INST', 'Minimum instantaneous streamflow for period of record', 'Minimum instantaneous flow');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (497, 'DECD99', 'December streamflow exceeded 99 percent of the time', 'December 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (475, 'DECD7', 'December streamflow exceeded 7 percent of the time', 'December 7 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (474, 'DECD5', 'December streamflow exceeded 5 percent of the time', 'December 5 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (473, 'DECD3', 'December streamflow exceeded 3 percent of the time', 'December 3 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (498, 'DEPH25', 'Difference in water level between the 25-percent duration flow and the point of zero flow', 'Flow depth');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (471, 'DECD1', 'Streamflow exceeded 1 percent of the time', 'December 1 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (470, 'NOVD99', 'November streamflow exceeded 99 percent of the time', 'November 99 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (469, 'NOVD98', 'November streamflow exceeded 98 percent of the time', 'November 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (468, 'NOVD97', 'November streamflow exceeded 97 percent of the time', 'November 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (467, 'NOVD95', 'November streamflow exceeded 95 percent of the time', 'November 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (466, 'NOVD93', 'November streamflow exceeded 93 percent of the time', 'November 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (465, 'NOVD90', 'November streamflow exceeded 90 percent of the time', 'November 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (464, 'NOVD85', 'November streamflow exceeded 85 percent of the time', 'November 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (463, 'NOVD80', 'November streamflow exceeded 80 percent of the time', 'November 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (462, 'NOVD75', 'November streamflow exceeded 75 percent of the time', 'November 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (461, 'NOVD70', 'November streamflow exceeded 70 percent of the time', 'November 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (460, 'NOVD65', 'November streamflow exceeded 65 percent of the time', 'November 65 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (459, 'NOVD60', 'November streamflow exceeded 60 percent of the time', 'November 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (458, 'NOVD55', 'November streamflow exceeded 55 percent of the time', 'November 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (457, 'NOVD50', 'November streamflow exceeded 50 percent of the time', 'November 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (476, 'DECD10', 'December streamflow exceeded 10 percent of the time', 'December 10 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (477, 'DECD15', 'December streamflow exceeded 15 percent of the time', 'December 15 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (472, 'DECD2', 'December streamflow exceeded 2 percent of the time', 'December 2 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (478, 'DECD20', 'December streamflow exceeded 20 percent of the time', 'December 20 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (496, 'DECD98', 'December streamflow exceeded 98 percent of the time', 'December 98 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (542, 'M30D10Yp05', '30-day 10-year low flow plus 0.05 (Maryland)', '30 Day 10 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (495, 'DECD97', 'December streamflow exceeded 97 percent of the time', 'December 97 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (494, 'DECD95', 'December streamflow exceeded 95 percent of the time', 'December 95 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (543, 'M30D20Yp05', '30-day 20-year low flow plus 0.05 (Maryland)', '30 Day 20 Year Low Flow plus 0.05');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (492, 'DECD90', 'December streamflow exceeded 90 percent of the time', 'December 90 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (491, 'DECD85', 'December streamflow exceeded 85 percent of the time', 'December 85 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (490, 'DECD80', 'December streamflow exceeded 80 percent of the time', 'December 80 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (489, 'DECD75', 'December streamflow exceeded 75 percent of the time', 'December 75 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (488, 'DECD70', 'December streamflow exceeded 70 percent of the time', 'December 70 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (493, 'DECD93', 'December streamflow exceeded 93 percent of the time', 'December 93 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (486, 'DECD60', 'December streamflow exceeded 60 percent of the time', 'December 60 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (485, 'DECD55', 'December streamflow exceeded 55 percent of the time', 'December 55 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (484, 'DECD50', 'December streamflow exceeded 50 percent of the time', 'December 50 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (483, 'DECD45', 'December streamflow exceeded 45 percent of the time', 'December 45 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (482, 'DECD40', 'December streamflow exceeded 40 percent of the time', 'December 40 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (481, 'DECD35', 'December streamflow exceeded 35 percent of the time', 'December 35 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (480, 'DECD30', 'December streamflow exceeded 30 percent of the time', 'December 30 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (479, 'DECD25', 'December streamflow exceeded 25 percent of the time', 'December 25 Percent Duration');
INSERT INTO shared."RegressionType" ("ID", "Code", "Description", "Name")
VALUES (487, 'DECD65', 'December streamflow exceeded 65 percent of the time', 'December 65 Percent Duration');

INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (19, 'NOVFDS', 'November Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (20, 'DECFDS', 'December Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (21, 'GFS', 'General Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (22, 'BF', 'Base Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (23, 'FPS', 'Flow Percentile Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (24, 'BNKF', 'Bankfull Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (29, 'LFBS', 'Low-Flow Base Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (26, 'RCHG', 'Recharge Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (27, 'DPS', 'Depth Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (28, 'LFCS', 'Low-Flow Current Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (30, 'PROB', 'Probability Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (32, 'RPFS', 'Rural Peak-Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (18, 'OCTFDS', 'October Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (25, 'YIELD', 'Yield Statistics (flow per area)');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (17, 'SEPFDS', 'September Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (31, 'UPFS', 'Urban Peak-Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (15, 'JULFDS', 'July Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (1, 'PC', 'Physical Characteristics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (2, 'PFS', 'Peak-Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (3, 'FVS', 'Flood-Volume Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (5, 'FDS', 'Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (6, 'AFS', 'Annual Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (7, 'MFS', 'Monthly Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (4, 'LFS', 'Low-Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (9, 'JANFDS', 'January Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (10, 'FEBFDS', 'February Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (11, 'MARFDS', 'March Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (12, 'APRFDS', 'April Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (13, 'MAYFDS', 'May Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (14, 'JUNFDS', 'June Flow-Duration Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (8, 'SFS', 'Seasonal Flow Statistics');
INSERT INTO shared."StatisticGroupType" ("ID", "Code", "Name")
VALUES (16, 'AUGFDS', 'August Flow-Duration Statistics');

INSERT INTO shared."UnitSystemType" ("ID", "UnitSystem")
VALUES (1, 'Metric');
INSERT INTO shared."UnitSystemType" ("ID", "UnitSystem")
VALUES (2, 'US Customary');
INSERT INTO shared."UnitSystemType" ("ID", "UnitSystem")
VALUES (3, 'Universal');

INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (313, 'PERM24IN', 'Area-weighted average soil permeability for top 24 inches of soil', 'PERM24IN');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (310, 'LC01EVERG', 'Percentage of area evergreen forest, NLCD 2001 category 42', 'LC01EVERG');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (311, 'LC01CROP', 'Percentage of area crop, NLCD 2001 category ', 'LC01CROP');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (312, 'PERM12IN', 'Area-weighted average soil permeability for top 12 inches of soil', 'PERM12IN');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (319, 'LC11ADEVMD', 'Area of developed land, medium intensity, NLCD 2011 class 23', 'LC11ADEVMD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (315, 'MAXTEMPC', 'Mean annual maximum air temperature over basin area, in degrees Centigrade', 'MAXTEMPC');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (316, 'LC11ADEV', 'Area of developed land-use from NLCD 2011 classes 21-24', 'LC11ADEV');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (317, 'LC11ADVOPN', 'Area of developed open land from NLCD 2011 class 21', 'LC11ADVOPN');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (318, 'LC11ADEVLO', 'Area of developed land, low intensity, from NLCD 2011 class 22', 'LC11ADEVLO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (309, 'LC01BARE', 'Percentage of area barren land, NLCD 2001 category 31', 'LC01BARE');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (314, 'ACRSDFT', 'Area underlain by stratified drift', 'ACRSDFT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (308, 'LC01DEVHI', 'Percentage of area developed, high intensity, NLCD 2001 category 24', 'LC01DEVHI');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (300, 'I60M100Y', 'Maximum 60-min precipitation that occurs on average once in 100 years', 'I60M100Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (306, 'LC01OPNLO', 'Percentage of area developed, open space and low intensity combined, NLCD2001 cat. 21 and 22', 'LC01OPNLO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (305, 'I48H500Y', 'Maximum 48-hour precipitation that occurs on average once in 500 years', 'I48H500Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (304, 'I24H500Y', 'Maximum 24-hour precipitation that occurs on average once in 500 years', 'I24H500Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (303, 'I6H500Y', 'Maximum 6-hour precipitation that occurs on average once in 500 years', 'I6H500Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (302, 'I60M500Y', 'Maximum 60-min precipitation that occurs on average once in 500 years', 'I60M500Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (301, 'I48H100Y', 'Maximum 48-hour precipitation that occurs on average once in 100 years', 'I48H100Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (320, 'LC11ADEVHI', 'Area of developed land, high intensity, NLCD 2011 class 24', 'LC11ADEVHI');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (299, 'I48H50Y', 'Maximum 48-hour precipitation that occurs on average once in 50 years', 'I48H50Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (298, 'I6H50Y', 'Maximum 6-hour precipitation that occurs on average once in 50 years', 'I6H50Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (297, 'I60M50Y', 'Maximum 60-min precipitation that occurs on average once in 50 years', 'I60M50Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (296, 'I48H25Y', 'Maximum 48-hour precipitation that occurs on average once in 25 years', 'I48H25Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (307, 'LC01DEVMD', 'Percentage of area developed, medium intensity, NLCD 2001 category 23', 'LC01DEVMD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (321, 'LC11DEVLMH', 'Percentage drainage area that is in low to high developed land-use classes 22-24 from NLCD 2011', 'LC11DEVLMH');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (330, 'LAKESNHDH', 'Percent of basin in lakes, ponds, and reservoirs fom high resolution National Hydrography Dataset', 'LAKESNHDH');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (323, 'LC11FOREST', 'Percentage of forest from NLCD 2011 classes 41-43', 'LC11FOREST');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (349, 'LC11EMWET', 'Percentage of area of emergent herbaceous wetlands from NLCD 2011 class 95', 'LC11EMWET');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (348, 'LC11WDWET', 'Percentage of area of wooded wetlands from NLCD 2011 class 90', 'LC11WDWET');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (347, 'CLIFAC100Y', '100-year climate factor from Litchy and Karlinger (1990)', 'CLIFAC100Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (346, 'CLIFAC25Y', '25-year climate factor from Litchy and Karlinger (1990)', 'CLIFAC25Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (345, 'LC01WATER', 'Percentage of open water, class 11, from NLCD 2001', 'LC01WATER');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (344, 'LC01HERB', 'Percentage of herbaceous upland from NLCD 2001 class 71', 'LC01HERB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (343, 'NFSL30', 'North-Facing Slopes Greater Than 30 Percent', 'NFSL30');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (342, 'LC11DEVHI', 'Percentage of area developed, high intensity, NLCD 2011 class 24', 'LC11DEVHI');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (341, 'LC11DVMD', 'Percentage of area developed, medium intensity, NLCD 2011 class 23', 'LC11DVMD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (340, 'LC11DVLO', 'Percentage of developed area, low intensity, from NLCD 2011 class 22', 'LC11DVLO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (339, 'LC11HERB', 'Percentage of herbaceous from NLCD 2011 classes 71-74', 'LC11HERB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (338, 'LC11FORSHB', 'Percentage of forests and shrub lands, classes 41 to 52, from NLCD 2011', 'LC11FORSHB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (322, 'LC11DINT', 'Impervious percentage computed as ((.10*A21+.25*A22+.65*A23+.90*A24)/DA)*100 from NLCD 2011', 'LC11DINT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (337, 'LC11BARE', 'Percentage of barren from NLCD 2011 class 31', 'LC11BARE');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (335, 'MAJ_ROADS', 'Length of non-state major roads in basin', 'MAJ_ROADS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (334, 'STATE_HWY', 'Length of state highways in basin', 'STATE_HWY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (333, 'ET0710MOD', 'Summer (July-October) mean monthly evapotranspiration (2001-2011), MODIS', 'ET0710MOD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (332, 'LC01WETLND', 'Percentage of wetlands, classes 90 and 95,  from NLCD 2001', 'LC01WETLND');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (331, 'LC01CRPHAY', 'Percentage of cultivated crops and hay, classes 81 and 82, from NLCD 2001', 'LC01CRPHAY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (295, 'I6H25Y', 'Maximum 6-hour precipitation that occurs on average once in 25 years', 'I6H25Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (329, 'IRRIGAT_MT', 'Percent of basin that is irrigated based on Montana Final Land Unit (FLU) classification', 'IRRIGAT_MT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (328, 'LC11STOR', 'Percentage of water bodies and wetlands determined from the NLCD 2011', 'LC11STOR');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (327, 'LC11WETLND', 'Percentage of wetlands, classes 90 and 95,  from NLCD 2011', 'LC11WETLND');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (326, 'LC11AWETL', 'Area of wetlands from NLCD 2011 classes 90 and 95', 'LC11AWETL');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (325, 'LC11WATER', 'Percent of open water, class 11, from NLCD 2011', 'LC11WATER');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (324, 'LC11AWATER', 'Area of water from NLCD 2011 class 11', 'LC11AWATER');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (336, 'MIN_ROADS', 'Length of non-state minor roads in basin', 'MIN_ROADS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (294, 'I60M25Y', 'Maximum 60-min precipitation that occurs on average once in 25 years', 'I60M25Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (284, 'ELEV10FT3D', 'Elevation at 10 percent from outlet along longest flow path slope using 3D line', 'ELEV10FT3D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (292, 'I6H10Y', 'Maximum 6-hour precipitation that occurs on average once in 10 years', 'I6H10Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (261, 'PKREGNO', 'Peak Flow Region Number', 'Peak Flow Region Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (260, 'LFREGNO', 'Low Flow Region Number', 'Low Flow Region Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (259, 'ORREG2', 'Oregon Region Number', 'Oregon Region Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (258, 'BFREGNO', 'BFREGNO', 'BFREGNO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (257, 'PZNSSREGNO', 'Zeroflow Region Number', 'PZNSSREGNO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (256, 'HIGHREG', 'HIGHREG', 'HIGHREG');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (255, 'LOWREG', 'Low Flow Region Number', 'Low Flow Region Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (254, 'LC01DEV', 'Percentage of land-use from NLCD 2001 classes 21-24', 'Percent_Developed_from_NLCD2001');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (253, 'WATCAPORR', 'Available water capacity from STATSGO data using methods from SIR 2008-5126', 'Available_Water_Capacity_OR_Risley');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (252, 'WATCAPORC', 'Available water capacity  from STATSGO data using methods from SIR 2005-5116', 'Available_Water_Capacity_OR_Cooper');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (251, 'RUGGED', 'Ruggedness number computed as stream density times basin relief', 'Ruggedness_Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (250, 'LC11IMP', 'Average percentage of impervious area determined from NLCD 2011 impervious dataset', 'Percent_Impervious_NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (249, 'PERENNIAL', 'Stream characterized as having flow at all times', 'Perennial_Stream_Flag_1_if_peren_else_0');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (248, 'EL5000', 'Percent of area above 5000 ft', 'Percent_above_5000_ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (247, 'ET0306MOD', 'Spring (March-June) mean monthly evapotranspiration (2001-2011), MODIS', 'Mean_Monthly_EvapTrans_Mar_to_Jun_MODIS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (237, 'LC11CRPHAY', 'Percentage of cultivated crops and hay, classes 81 and 82, from NLCD 2011', 'Percent_Crops_and_Hay_from_NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (246, 'SLOP50_30M', 'Percent area with slopes greater than 50 percent from 30-meter DEM.', 'Slopes_gt_50pct_from_30m_DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (245, 'LC06FOREST', 'Percentage of forest from NLCD 2006 classes 41-43', 'Percent_Forest_from_NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (244, 'LC06CROP', 'Percentage of area of cultivated crops from NLCD 2006 class 82', 'Percent_Cultivated_Crops_from_NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (243, 'PFLATLOW', 'Flat lands lower than median elevation from Wolock 2003 unpublished data', 'Flat_Lands_Below_Median_Elevation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (242, 'PMPE', 'Precipitation minus potential evaporation from Wolock 2003 unpublished data', 'Precip_Minus_Potential_Evap');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (241, 'SSURGOM', 'Percentage of organic matter in soils from SSURGO', 'Percent_SSURGO_Soil_Organic_Matter');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (240, 'LC06WATER', 'Percent of open water, class 11, from NLCD 2006', 'Percent_Water_from_NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (238, 'PRJULDEC10', 'Basin average mean precipitation for July to December from PRISM 1981-2010', 'Basin_Ave_Precip_Jul_Dec_PRISM_2010');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (350, 'MAYAVPRE', 'Mean May Precipitation', 'Mean May Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (262, 'FD_Region', 'FD_Region', 'FD_Region');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (263, 'LC11DVOPN', 'Percentage of developed open area from NLCD 2011 class 21', 'Percent_Open_Developed_from_NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (264, 'ALVM', 'Percentage of the basin covered by Quaternary alluvial deposits from Reed & Bush (2005)', 'Percent_Quaternary_Alluvium');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (265, 'LC11PAST', 'Percentage of area of pasture area from NLCD 2011 class 81', 'Percent_Pasture_from_NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (291, 'I60M10Y', 'Maximum 60-min precipitation that occurs on average once in 10 years', 'I60M10Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (290, 'I24H5Y', 'Maximum 24-hour precipitation that occurs on average once in 5 years', 'I24H5Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (289, 'I6H5Y', 'Maximum 6-hour precipitation that occurs on average once in 5 years', 'I6H5Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (288, 'I60M5Y', 'Maximum 60-min precipitation that occurs on average once in 5 years', 'I60M5Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (287, 'I6H2Y', 'Maximum 6-hour precipitation that occurs on average once in 2 years', '6 Hour 2 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (286, 'I60M2Y', 'Maximum 60-min precipitation that occurs on average once in 2 years', 'I60M2Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (285, 'ELEV85FT3D', 'Elevation at 85 percent from outlet along longest flow path slope using 3D line', 'ELEV85FT3D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (283, 'ELEV85FT', 'Elevation at 85 percent from outlet along longest flow path slope using DEM', 'ELEV85FT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (282, 'ELEV10FT', 'Elevation at 10 percent from outlet along longest flow path slope using DEM', 'ELEV10FT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (281, 'SLPFM3D', 'Change in elevation divided by length between points 10 and 85 percent of distance along the longest flow path to the basin divide, LFP from 3D grid', 'SLPFM3D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (280, 'LC06AGRI', 'Percent agriculture computed as total of grass, pasture, and crops, NLCD classes 71, 81 and 82', 'Percent agriculture');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (279, 'OUTLETX', 'Basin outlet horizontal (x) location in state plane coordinates', 'OUTLETX');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (293, 'I48H10Y', 'Maximum 48-hour precipitation that occurs on average once in 10 years', 'I48H10Y');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (278, 'OUTLETY', 'Basin outlet vertical (y) location in state plane coordinates', 'OUTLETY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (276, 'CENTROIDY', 'Basin centroid vertical (y) location in state plane units', 'CENTROIDY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (275, 'MAPM', 'Mean Annual Precip Basin Average', 'Mean Annual Precip Basin Average');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (274, 'FOSTREAM', 'Number of First Order Streams', 'Number of First Order Streams');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (273, 'JANMINT2K', 'Mean Minimum January Temperature from 2K resolution PRISM PRISM 1961-1990 data', 'Mean Minimum January Temperature from 2K resolution PRISM PRISM 1961-1990 data');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (272, 'JANMAXT2K', 'Mean Maximum January Temperature from 2K resolution PRISM 1961-1990 data', 'Mean Maximum January Temperature from 2K resolution PRISM 1961-1990 data');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (236, 'PRSEPNOV00', 'Basin average mean precipitation for September to November from PRISM 1971-2000', 'Basin_Ave_Precip_Sept_Nov_PRISM_2000');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (271, 'ST2INDNR', 'Average transmissivity (ft2/d) for the full depth of unconsolidated deposits within 1000 ft of stream channel from InDNR well database.', 'Avg_Transmissivity_Near_Channel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (270, 'LC01FOREST', 'Percentage of forest from NLCD 2001 classes 41-43', 'Percent_Forest_from_NLCD2001');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (269, 'K2INDNR', 'Average hydraulic conductivity (ft/d) for the full depth of unconsolidated deposits from InDNR well database.', 'Avg_Hydraulic_Conductivity_Full_Depth');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (268, 'T2INDNR', 'Average transmissivity (ft2/d) for the full depth of unconsolidated deposits from InDNR well database.', 'Avg_Transmissivity');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (267, 'K1INDNR', 'Average hydraulic conductivity (ft/d) for the top 70 ft of unconsolidated deposits from InDNR well database.', 'Avg_Hydraulic_Conductivity_Upper_70ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (266, 'UPZ', 'Percentage of the basin covered by upper Paleozoic strata from Reed & Bush (2005)', 'Percent_Upper_Paleozoic');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (277, 'CENTROIDX', 'Basin centroid horizontal (x) location in state plane coordinates', 'CENTROIDX');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (351, 'JULAVPRE', 'Mean July Precipitation', 'Mean July Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (457, 'TOC', 'Time of concentration in hours', 'Time of concentration in hours');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (353, 'FEBAVTMP', 'Mean February Temperature', 'Mean February Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (437, 'STATSCLY40', 'Percentage of  soils with greater than 30 percent and less than or equal to 40 percent  clay from STATSGO', 'STATSCLY40');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (436, 'STATSCLY30', 'Percentage of  soils with greater than 20 percent and less than or equal to 30 percent  clay from STATSGO', 'STATSCLY30');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (435, 'STATSCLY20', 'Percentage of  soils with greater than 10 percent and less than or equal to 20 percent clay from STATSGO', 'STATSCLY20');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (434, 'STATSCLAY10', 'Percentage of  soils with less than 10 percent clay from STATSGO', 'STATSCLAY10');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (433, 'STATSGODEP', 'Area-weighted average soil depth from NRCS STATSGO database', 'STATSGODEP');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (432, 'STATSWATCP', 'Available water capacity of the top 60 inches of soil - determined from STATSGO data', 'STATSWATCP');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (431, 'VRCARB', 'Percent of area of carbonate rocks within the Valley and Ridge Physiographic Region', 'VRCARB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (430, 'VRPLSLC', 'Percent of area of siliciclastic rocks witin the Valley and Ridge or Appalachian Plateau Physiographic Regions', 'VRPLSLC');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (429, 'MESZOIC', 'Percent of area within the Mesozoic Basins', 'MESZOIC');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (428, 'PDIGMET', 'Percent area of igneous and metamorphic  within the Piedmont Physiographic Region', 'PDIGMET');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (427, 'CPSED', 'Percent area of sedimentary rockswithin the Coastal Plain Physiographic Region', 'CPSED');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (426, 'BRMETA', 'Percent area of metamorphic rocks within the Blue Ridge Physiographic Region', 'BRMETA');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (425, 'LC06CRPHAY', 'Percentage of cultivated crops and hay, classes 81 and 82, from NLCD 2006', 'LC06CRPHAY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (424, 'LC06FORSHB', 'Percentage of forests and shrub lands, classes 41 to 52, from NLCD 2006', 'LC06FORSHB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (423, 'LC01FORSHB', 'Percentage of forests and shrub lands, classes 41 to 52, from NLCD 2001', 'LC01FORSHB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (422, 'BSHAPELFP', 'Basin Shape Factor computed as the square of the longest flow path divided by drainage area', 'Basin Shape Factor, Longest flow path method');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (421, 'STRMTOTED', 'Total stream length in miles - edited NHD', 'STRMTOTED');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (420, 'CROPS', 'Percent of area covered by agriculture', 'CROPS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (419, 'WATER', 'Percent of area covered by open water (lakes, ponds, reservoirs)', 'WATER');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (418, 'OUTLETYA83', 'Y coordinate of the outlet, in NAD_1983_Albers, meters', 'Y coordinate of the outlet, in NAD_1983_Albers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (417, 'OUTLETXA83', 'X coordinate of the outlet, in NAD_1983_Albers,meters', 'X coordinate of the outlet, in NAD_1983_Albers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (416, 'CENTROYA83', 'Basin centroid horizontal (y) location in NAD 1983 Albers', 'Basin centroid Y, in NAD 1983 Albers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (415, 'CENTROXA83', 'X coordinate of the centroid, in NAD_1983_Albers, meters', 'X coordinate of the centroid, in NAD_1983_Albers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (414, 'BSLOPDRAW', 'Unadjusted basin slope, in degrees', 'Unadjusted basin slope');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (413, 'DRN', 'Drainage quality index from STATSGO', 'Drainage quality index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (438, 'STATSCLY50', 'Percentage of  soils with greater than 40 percent and less than or equal to 50 percent  clay from STATSGO', 'STATSCLY50');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (412, 'JUNMAXTMP', 'Maximum June Temperature, in degrees F', 'Maximum June Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (439, 'STATSCLY60', 'Percentage of  soils with greater than 50 percent and less than or equal to 60 percent  clay from STATSGO', 'STATSCLY60');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (441, 'STATSOM2_6', 'Percentage of  soils with greater than 0.50 percent and less than or equal to 2.60 percent organic matter from STATSGO', 'STATSOM2_6');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (466, 'ASPECT', 'basin average of topographic slope compass directions from elevation grid', 'Basin average aspect');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (235, 'PRDECFEB00', 'Basin average mean precipitation for December to  February from PRISM 1971-2000', 'Basin_Ave_Precip_Dec_Feb_PRISM_2000');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (465, 'HA4PCT', 'Percent of area within Hydrologic Area 4', 'HA4PCT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (464, 'HA3PCT', 'Percent of area within Hydrologic Area 3', 'HA3PCT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (463, 'HA2PCT', 'Percent of area within Hydrologic Area 2', 'HA2PCT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (462, 'HA1PCT', 'Percent of area within Hydrologic Area 1', 'HA1PCT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (461, 'GWHEAD', 'Mean basin elevation minus minimum basin elevation', 'Ground water head');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (460, 'CN', 'Composite NRCS curve number', 'Curve Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (459, 'CSL1085ADJ', 'Adjusted 10-85 slope in feet per mile', 'CSL1085ADJ');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (458, 'CSL1085RAW', 'Unadjusted 10-85 stream slope method in feet per mile.', 'CSL1085RAW');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (456, 'RUNCO_CO', 'Soil runoff coefficient as defined by Verdin and Gross (2017)', 'Soil Runoff Coefficient');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (455, 'RCN', 'Runoff-curve number as defined by NRCS (http://policy.nrcs.usda.gov/OpenNonWebContent.aspx?content=17758.wba)', 'Runoff-Curve Number');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (454, 'LC11SNOIC', 'Percent snow and ice from NLCD 2011 class 12', 'Percent snow and ice from NLCD 2011 class 12');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (453, 'SGSL', 'Total stream length intersecting sand and gravel deposits ( in miles )', 'Total stream length intersecting sand and gravel deposits ( in miles )');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (452, 'SSURGWDRN', 'Percentage of well drained soil, from SSURGO', 'Percent Well Drained Soil from SSURGO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (451, 'RRMEAN', 'Relief ratio defined as (ELEV-MINBELEV)/(ELEVMAX-MINBELEV)', 'Relief Ratio Mean');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (450, 'RELRELFff', 'Basin relief divided by basin parameter in ft per ft', 'Relative Relief ft/ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (449, 'INSINKING', 'Percent Sinking stream drainage area from Indiana Geological Survey.', 'Percent Sinking Stream Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (448, 'INSINKHOLE', 'Percent Sinkhole drainage area per basin from Indiana Geological Survey.', 'Percent Sinkhole Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (447, 'I24H100YA2', 'Maximum 24-hour precipitation that occurs on average once in 100 years from NOAA Atlas 2', '24 Hour 100 Year Precipitation Atlas2');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (446, 'CENTRYUTM', 'Basin centroid horizontal (y) location in UTM meters', 'Basin Centroid Y UTM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (445, 'CENTRXUTM', 'Basin centroid horizontal (x) location in UTM meters', 'Basin Centroid X UTM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (444, 'STATOM55_7', 'Percentage of  soils with greater than 19.8 percent and less than or equal to 55.7 percent organic matter from STATSGO', 'STATOM55_7');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (443, 'STATOM19_8', 'Percentage of  soils with greater than 7.3 percent and less than or equal to 19.8 percent organic matter from STATSGO', 'STATOM19_8');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (442, 'STATSOM7_3', 'Percentage of  soils with greater than 2.6 percent and less than or equal to 7.3 percent organic matter from STATSGO', 'STATSOM7_3');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (440, 'STATSOM0_5', 'Percentage of  soils with less than 0.5 percent organic matter from STATSGO', 'STATSOM0_5');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (411, 'PRJUNAUG00', 'Basin average mean precip for June to August from PRISM 1971-2000', 'Basin average mean precip for June to August');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (410, 'CONTOUR', 'Total length of all elevation contours in drainage area in miles', 'Total length of all elevation contours in drainage area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (409, 'CSL1085UP', '10-85 slope of upper half of main channel in feet per mile.', '10-85 slope of upper half of main channel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (378, 'WETLNDNWI', 'Percent wetlands as determined from the National Wetlands Inventory (2001)', 'Percent Wetlands');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (377, 'LAKESNWI', 'Percent lakes and ponds as determined from the National Wetlands Inventory (2001)', 'Percent Lakes and Ponds');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (376, 'NONCONTDA', 'Area covered by noncontributing drainage area', 'Area Covered by Noncontributing Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (375, 'SLOPERAT', 'Slope ratio computed as longest flow path (10-85) slope divided by basin slope', 'Slope Ratio');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (374, 'MINBSLOP', 'Minimum basin slope, in percent', 'Minimum Basin Slope in Percent');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (373, 'MAXBSLOP', 'Maximum basin slope, in percent', 'Maximum Basin Slope in Percent');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (372, 'NRCSPCT', 'Percent of contributing drainage area regulated by NRCS floodwater-retarding structures', 'Percent of contributing drainage area regulated by NRCS floodwater-retarding structures');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (371, 'NFSL30_10M', 'Percent area with north-facing slopes greater than 30 percent from 10-meter NED.', 'Percent area with north-facing slopes greater than 30 percent from 10-meter NED.');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (370, 'AZ_HIPERMG', 'Percent basin surface area containing high permeability geologic units as defined for Arizona in SIR 2014-5211', 'Percent basin surface area containing high permeability geologic units as defined for Arizona');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (369, 'AZ_HIPERMA', 'Percent basin surface area containing high permeability aquifer units as defined for Arizona in SIR 2014-5211', 'Percent basin surface area containing high permeability aquifer units as defined for Arizona');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (368, 'CH92_01DEV', 'Percent Difference between 1992 and 2001 area covered by developed land using NLCD', 'Percent Difference between 1992 and 2001 area covered by developed land using NLCD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (367, 'CH92_01FOR', 'Percent Difference between 1992 and 2001 area covered by forest using NLCD', 'Percent Difference between 1992 and 2001 area covered by forest using NLCD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (366, 'LC92DEV', 'Percentage of developed land from NLCD 1992', 'Percentage of developed land from NLCD 1992');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (365, 'LC92FOREST', 'Percentage of forest from NLCD 1992 classes 41-43', 'Percentage of forest');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (364, 'I24H200Y', 'Maximum 24-hour precipitation that occurs on average once in 200 years', 'Maximum 24-hour precipitation that occurs on average once in 200 years');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (363, 'DECAVTMP', 'Mean December Temperature', 'Mean December Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (362, 'NOVAVTMP', 'Mean November Temperature', 'Mean November Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (361, 'OCTAVTMP', 'Mean October Temperature', 'Mean October Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (360, 'SEPAVTMP', 'Mean September Temperature', 'Mean September Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (359, 'AUGAVTMP', 'Mean August Temperature', 'Mean AugustTemperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (358, 'JULYAVTMP', 'Mean July Temperature', 'Mean July Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (357, 'JUNEAVTMP', 'Mean June Temperature', 'Mean June Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (356, 'MAYAVTMP', 'Mean May Temperature', 'Mean May Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (355, 'APRAVTMP', 'Mean AprilTemperature', 'Mean April Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (354, 'MARAVTMP', 'Mean March Temperature', 'Mean March Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (379, 'SD_LSTLZ', 'Percent Limestone Loss Zone from Sando and others (2008)', 'Percent Limestone Loss Zone');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (380, 'SD_LSTHW', 'Percent Limestone Headwaters from Sando and others (2008)', 'Percent Limestone Headwaters');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (381, 'SD_CC', 'Percent Crystalline Core from Sando and others (2008)', 'Percent Crystalline Core');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (382, 'SD_AS', 'Percent Artesian Spring from Sando and others (2008)', 'Percent Artesian Spring');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (408, 'CSL1085LO', '10-85 slope of lower half of main channel in feet per mile.', '10-85 slope of lower half of main channel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (407, 'TNCLFACT2', 'Tennessee climate factor, 2-year interval', 'Tennessee Climate Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (406, 'LC11SHRUB', 'Percent of area covered by shrubland using 2011 NLCD', 'LC11SHRUB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (405, 'LC11GRASS', 'Percent of area covered by grassland/herbaceous using 2011 NLCD', 'LC11GRASS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (404, 'LC06WETLND', 'Percent of area covered by wetland using 2006 NLCD', 'LC06WETLND');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (403, 'LC06SHRUB', 'Percent of area covered by shrubland using 2006 NLCD', 'LC06SHRUB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (402, 'LC06GRASS', 'Percent of area covered by grassland/herbaceous using 2006 NLCD', 'LC06GRASS');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (401, 'LC06PLANT', 'Percent of area in cultivation using 2006 NLCD', 'LC06PLANT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (400, 'LC06BARE', 'Percent of area covered by barren rock using 2006 NLCD', 'LC06BARE');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (399, 'LC01SHRUB', 'Percent of area covered by shrubland using 2001 NLCD', 'LC01SHRUB');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (398, 'LC01IMP', 'Percent imperviousness of basin area 2001 NLCD', 'LC01IMP');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (397, 'LU92WETLN', 'Percent of area covered by wetland using 1992 NLCD', 'LU92WETLN');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (352, 'JANAVTMP', 'Mean January Temperature', 'Mean January Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (396, 'LU92WATER', 'Percent of area covered by water using 1992 NLCD', 'LU92WATER');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (394, 'LU92DEV', 'Percent of area covered by all densities of developed land using 1992 NLCD', 'LU92DEV');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (393, 'LU92BARE', 'Percent of area covered by barren rock using 1992 NLCD', 'LU92BARE');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (392, 'PROTECTED', 'Percent of area of protected Federal and State owned land', 'Percent of area of protected Federal and State owned land');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (391, 'BSLDEM30FT', 'Mean basin slope, based on slope percent grid', 'Mean basin slope');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (390, 'ADJCOEFF', 'Coefficient to adjust estimates for percentage of carbonate rock in Western Maryland', 'Adjustment Coefficient');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (389, 'CSL100', 'Longest flow path slope in feet per miles, using DEM', 'Longest flow path slope in feet per miles');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (388, 'PRDECFEB10', 'PRISM Precip Mean Winter (dec,jan,feb) (inches)', 'PRISM Precip Mean Winter (dec,jan,feb) (inches)');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (387, 'SSURGPDRN', 'SSURGO Percent Poor Drainage', 'SSURGO Percent Poor Drainage ');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (386, 'SSURGSAND', 'SSURGO percent sand', 'SSURGO Percent Sand');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (385, 'SD_SNDHLS', 'Percent Sand Hills setting from Sando and others (2008)', 'Percent Sand Hills setting');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (384, 'SD_BHEXT', 'Percent Black Hills Exterior from Sando and others (2008)', 'Percent Black Hills Exterior');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (383, 'SD_ASLZ', 'Percent Loss Zone/Artesian Spring from Sando and others (2008)', 'Percent Loss Zone/Artesian Spring');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (395, 'LU92PLANT', 'Percent of area in cultivation using 1992 NLCD', 'LU92PLANT');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (234, 'FSSURGDC78', 'Fraction of land area that is in very poorly drained and unknown likely water drainage classes 7 and 8  from SSURGO', 'Fraction_SSURGO_Drainage_Classes_7_and_8');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (35, 'DESMOIN', 'Area underlain by Des Moines Lobe', 'Des Moines Lobe');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (232, 'URBTHE2010', 'Fraction of drainage area that is in urban classes 7 to 10 from Theobald 2010', 'Fraction_of_Urban_Land_Theobald_2010');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (84, 'PII_SD', 'Maximum 24-hour precipitation that occurs on average once in 2 years minus 1.5 inches', 'S Dakota Precipitation Intensity Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (83, 'APRAVPRE', 'Mean April Precipitation', 'Mean April Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (82, 'CELVBLUE', 'Average of outlet elevation and the elevation at the upstream extent of the mapped stream', 'Average Channel Elevation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (81, 'POPDENS', 'Basin Population Density', 'Basin Population Density');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (80, 'STORAGE', 'Percentage of area of storage (lakes ponds reservoirs wetlands)', 'Percent Storage');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (79, 'STRDEN', 'Stream Density -- total length of streams divided by drainage area', 'Stream Density');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (78, 'AVMXSS', 'Average Maximum Soil Slope', 'Average Maximum Soil Slope');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (77, 'COMPRAT', 'A measure of basin shape related to basin perimeter and drainage area', 'Compactness Ratio');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (76, 'LSTPERM', 'Permeability of least permeable layer', 'Least Permeable Layer Permeability');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (75, 'RELRELF', 'Basin relief divided by basin perimeter', 'Relative Relief');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (74, 'DRNFREQ', 'Number of first order streams per square mile of drainage area', 'Drainage Frequency');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (73, 'BSLOPCM', 'Mean basin slope determined by summing lengths of all contours in basin mulitplying by contour interval and dividing product by drainage area', 'Mean Basin Slope ft per mi');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (72, 'WATCAP', 'Available water capacity of the top 60 inches of soil - determined from STATSGO data', 'Available Water Capacity');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (71, 'CLIMFAC2YR', 'Two-year climate factor from Lichy and Karlinger (1990)', 'Tennessee Climate Factor 2 Year');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (70, 'DAUNREG', 'Unregulated drainage area used in OK regulated equations', 'Unregulated Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (69, 'ELEV1000', 'Elevation in Thousands', 'Elevation in Thousands');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (68, 'LENGTH', 'Length along the main channel from the measuring location extended to the basin divide', 'Main Channel Length');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (67, 'CSL10_85fm', 'Change in elevation between points 10 and 85 percent of length along main channel to basin divide divided by length between points ft per mi', 'Stream Slope 10 and 85 Method ft per mi');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (66, 'LOGDA', 'Logarithm base 10 of drainage area', 'Log of Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (65, 'GENRO', 'Generalized mean annual runoff in Minnesota 1951-85', 'Generalized Runoff');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (64, 'CRSTILL', 'Percentage of area of coarse till', 'Percent Coarse Till');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (63, 'TILROCK', 'Percentage of area of thin glacial till over bedrock', 'Percent Thin Till over Bedrock');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (62, 'PCLAY', 'Percentage of lacustrine clay and silt', 'Percent Lacustrine Clay and Silt');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (61, 'MEDTILL', 'Percentage of area of medium-textured glacial till', 'Percent Medium Till');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (60, 'MORTILL', 'Percentage of End Moraines of Fine-Textured Till', 'End Moraines of Fine Textured Till');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (85, 'GUTTER', 'Length of gutters per square mile of drainage area', 'Gutter Length');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (59, 'MUCK', 'Percentage of area of peat and muck', 'Percent Peat and Muck');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (86, 'JANMAXTMP', 'Mean Maximum January Temperature', 'Mean Maximum January Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (88, 'ROCKDEP', 'Depth to rock', 'Depth to Rock');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (113, 'FORESTp1', 'Percent Forest plus 1 (ID ROI Parm)', 'Percent Forest add 1 ID ROI Parm');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (112, 'LAKESp1', 'Percent Lakes plus 1 (MN ROI Parm)', 'Percent Lakes add 1 MN ROI Parm');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (111, 'STORAGEp1', 'Percent Storage plus 1 (MN ROI Parm)', 'Percent Storage add 1 MN ROI Parm');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (110, 'PRECless35', 'Mean Annual Precip - 35 (LA ROI Parm)', 'Mean Annual Precip less 35 LA ROI Parm');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (109, 'I2H2Y', 'Maximum 2-hour precipitation that occurs on average once in 2 years', '2 Hour 2 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (108, 'JANAVPRE', 'Mean January Precipitation', 'Mean January Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (107, 'LAT_OUT', 'Latitude of Basin Outlet', 'Latitude of Basin Outlet');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (106, 'MARAVPRE', 'Mean March Precipitation', 'Mean March Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (105, 'SOILINDEX', 'Mean STATSGO Hydrologic Soils Index (from PL. 2 WRIR 03-4107 for WY)', 'Mean Basin Hydrologic Soils Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (104, 'SNOFALL', 'Mean Annual Snowfall', 'Mean Annual Snowfall');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (103, 'LU92HRBN', 'Percent Natural  Herbaceous Upland from NLCD1992', 'Percent Nat Herb Upland from NLCD1992');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (102, 'ILREG7', 'Indicator variable for IL region 7, enter 1 if site is in region 7 else 0', 'Region 7 Indicator  enter 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (101, 'ILREG6', 'Indicator variable for IL region 6, enter 1 if site is in region 6 else 0', 'Region 6 Indicator  enter 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (100, 'ILREG5', 'Indicator variable for IL region 5, enter 1 if site is in region 5 else 0', 'Region 5 Indicator  enter 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (99, 'OmegaEM', 'Generalized regression residual as defined in TX SIR 2009-5087', 'OmegaEM residual from 2009 5087');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (98, 'CSLBlue_ff', 'Change in elevation of the longest blue-line stream divided by stream length', 'Main Channel Slope ft per ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (97, 'CONVEY', 'Conveyance of main stream channel at bank-full conditions', 'Bank Full Channel Conveyance');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (96, 'TXURBINDEX', 'Urbanization index defined in WRIR 82-18', 'Texas Urbanization Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (95, 'CSLBlue', 'Change in elevation of the longest blue-line stream (not extended to the boundary) divided by stream length', 'Stream Slope Blue Line Method');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (94, 'LNG_GAGE', 'Longitude', 'Longitude');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (93, 'EVAPAN', 'Mean Annual Pan Evaporation', 'Mean Annual Pan Evaporation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (92, 'BASLENAH', 'Basin length from outlet to basin divide determined using the method in the ArcHydro Toolset', 'Basin Length ArcHydro Method');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (91, 'ILREG3', 'Indicator variable for IL region 3, enter 1 if site is in region 3 else 0', 'Region 3 Indicator  enter 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (90, 'WATWET', 'Percent open water and herbaceous wetland from NLCD', 'Percent Open Water & Herb Wetland');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (89, 'LONG_OUT', 'Longitude of Basin Outlet', 'Longitude of Basin Outlet');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (87, 'CARBON', 'Percentage of area of carbonate rock', 'Percent Carbonate');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (58, 'OUTWASH', 'Percentage of area of outwash', 'Percent Outwash');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (57, 'SLENRAT', 'Main channel length - squared - divided by the contributing drainage area', 'Slenderness Ratio');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (56, 'PLCHSWAMP', 'Percentage of the main channel length that flows through swamps', 'Percent Length of Main Channel Swamps');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (24, 'BDF', 'Urbanization index described by Sauer and others (1981) - Also called Urbanization Index in Texas', 'Basin Development Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (23, 'AIMPERV', 'Impervious area', 'Area of Impervious Surfaces');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (22, 'IMPNLCD01', 'Percentage of impervious area determined from NLCD 2001 impervious dataset', 'Percent Impervious NLCD2001');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (21, 'STORNHD', 'Percent storage (wetlands and waterbodies) determined from 1:24K NHD', 'Percent Storage from NHD');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (20, 'SOILA', 'Percentage of area of Hydrologic Soil Type A', 'Percent Hydrologic Soil Type A');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (19, 'I24H100Y', 'Maximum 24-hour precipitation that occurs on average once in 100 years', '24 Hour 100 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (18, 'I24H50Y', 'Maximum 24-hour precipitation that occurs on average once in 50 years', '24 Hour 50 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (17, 'I24H25Y', 'Maximum 24-hour precipitation that occurs on average once in 25 years', '24 Hour 25 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (16, 'I24H10Y', 'Maximum 24-hour precipitation that occurs on average once in 10 years', '24 Hour 10 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (15, 'I24H2Y', 'Maximum 24-hour precipitation that occurs on average once in 2 years - Equivalent to precipitation intensity index', '24 Hour 2 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (14, 'I6H100Y', '6-hour precipitation that is expected to occur on average once in 100 years', '6 Hour 100 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (13, 'EL7500', 'Percent of area above 7500 ft', 'Percent above 7500 ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (12, 'BSLDEM10M', 'Mean basin slope computed from 10 m DEM', 'Mean Basin Slope from 10m DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (11, 'LAT_GAGE', 'Latitude', 'Latitude');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (10, 'BSLDEM30M', 'Mean basin slope computed from 30 m DEM', 'Mean Basin Slope from 30m DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (9, 'BSHAPE', 'Basin Shape Factor for Area', 'Basin Shape Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (8, 'CSL10_85', 'Change in elevation divided by length between points 10 and 85 percent of distance along main channel to basin divide - main channel method not known', 'Stream Slope 10 and 85 Method');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (7, 'CONTDA', 'Area that contributes flow to a point on a stream', 'Contributing Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (6, 'ELEV', 'Mean Basin Elevation', 'Mean Basin Elevation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (5, 'FOREST', 'Percentage of area covered by forest', 'Percent Forest');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (4, 'JANMINTMP', 'Mean Minimum January Temperature', 'Mean Min January Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (3, 'PRECIP', 'Mean Annual Precipitation', 'Mean Annual Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (2, 'LAKEAREA', 'Percentage of Lakes and Ponds', 'Percent Lakes and Ponds');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (1, 'DRNAREA', 'Area that drains to a point on a stream', 'Drainage Area');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (467, 'JANAVPRE2K', 'Mean January Precipitation', 'Mean January Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (25, 'ADETEN', 'Lake and Detention Basin Area', 'Area of Lake and Detention Basin');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (26, 'LC92STOR', 'Percentage of water bodies and wetlands determined from the NLCD', 'Percent Storage from NLCD1992');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (27, 'PCTREG1', 'Percentage of drainage area located in Region 1', 'Percent Area in Region 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (28, 'PCTREG2', 'Percentage of drainage area located in Region 2', 'Percent Area in Region 2');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (55, 'WBANKFULL', 'Width of channel at bankfull', 'Width Of Bankfull Channel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (54, 'FOREST_MD', 'Percent forest from Maryland 2010 land-use data', 'Percent forest from MD 2010 land use');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (53, 'LIME', 'Percentage of area of limestone geology', 'Percent Limestone');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (52, 'IMPERV', 'Percentage of impervious area', 'Percent Impervious');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (51, 'SOILCorD', 'Percentage of area of Hydrologic Soil Type C or D from SSURGO', 'Percent SSURGO Soil Type C or D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (50, 'BSLDEM10ff', 'Mean basin slope computed from 10 m DEM in feet per foot', 'Mean Basin Slope from 10m DEM ft per ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (49, 'SSURGOA', 'Percentage of area of Hydrologic Soil Type A from SSURGO', 'SSURGO Percent Hydrologic Soil Type A');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (48, 'WETLAND', 'Percentage of Wetlands', 'Percent Wetlands');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (47, 'URBAN', 'Percentage of basin with urban development', 'Percent Urban');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (46, 'WACTCH', 'Width of active channel', 'Width Of Active Channel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (45, 'ILREG1', 'Indicator variable for IL region 1, enter 1 if site is in region 1 else 0', 'Region 1 Indicator  enter 1');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (44, 'SOILPERM', 'Average Soil Permeability', 'Average Soil Permeability');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (114, 'PSFSLp1', 'Pct. South Facing Slopes plus 1 (ID ROI Parm)', 'Pct South Facing Slopes add 1 ID ROI');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (43, 'NFSL30_30M', 'Percent area with north-facing slopes greater than 30 percent from 30-meter DEM.', 'N Facing Slopes gt 30pct from 30m DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (41, 'OHREGA', 'Ohio Region A Indicator', 'Ohio Region A Indicator 1 if in A else 0');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (40, 'OHREGC', 'Ohio Region C Indicator', 'Ohio Region C Indicator 1 if in C else 0');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (39, 'PRECPRIS10', 'Basin average mean annual precipitation for 1981 to 2010 from PRISM', 'Mean Annual Precip PRISM 1981 2010');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (38, 'LC06STOR', 'Percentage of water bodies and wetlands determined from the NLCD 2006', 'Percent Storage from NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (37, 'STORNWI', 'Percentage of strorage (combined water bodies and wetlands) from the Nationa Wetlands Inventory', 'Percentage of Storage from NWI');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (36, 'SSURGOKSAT', 'Saturated hydraulic conductivity in micrometers per second from NRCS SSURGO database', 'SSURGO Saturated Hydraulic Conductivity');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (34, 'CCM', 'Constant of channel maintenance computed as drainage area divided by total stream length', 'Constant of Channel Maintenance');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (33, 'LC06DEV', 'Percentage of land-use from NLCD 2006 classes 21-24', 'Percent Developed from NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (32, 'LC06IMP', 'Percentage of impervious area determined from NLCD 2006 impervious dataset', 'Percent Impervious NLCD2006');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (31, 'PCTREG5', 'Percentage of drainage area located in Region 5', 'Percent Area in Region 5');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (30, 'PCTREG4', 'Percentage of drainage area located in Region 4', 'Percent Area in Region 4');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (29, 'PCTREG3', 'Percentage of drainage area located in Region 3', 'Percent Area in Region 3');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (42, 'CSL1085LFP', 'Change in elevation divided by length between points 10 and 85 percent of distance along the longest flow path to the basin divide, LFP from 2D grid', 'Stream Slope 10 and 85 Longest Flow Path');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (115, 'CHANCOND', 'Condition between points 100- 75- 50- and 25-percent along main channel - 2 if entirely paved - 1 if unpaved', 'Average Channel Condition');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (116, 'SLOP30_30M', 'Percent area with slopes greater than 30 percent from 30-meter DEM.', 'Slopes gt 30pct from 30m DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (117, 'EL6000', 'Percent of area above 6000 ft', 'Percent above 6000 ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (201, 'RSD', 'Relative stream density first defined in SIR 2012_5171', 'Relative Stream Density');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (200, 'BFI', 'Proportion of mean annual flow that is from ground water (base flow)', 'Base Flow Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (199, 'OH_INF_IND', 'Index of relative infiltration from Koltun(1986)', 'Ohio Infiltration Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (198, 'PREMARAPR', 'Precipitation March-April basin average, mean monthly as defined in SIR 2008-5065', 'Basin Ave Rainfall Mar Apr');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (197, 'PRENOVDEC', 'Precipitation November-December basin average, mean monthly as defined in SIR 2008-5065', 'Basin Ave Rainfall Nov Dec');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (196, 'TAU_WIN', 'Tau, Average base-flow recession time constant determined from daily values for November through December as defined in SIR 2008-5065', 'Tau Nov Dec');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (195, 'TAU_SPR', 'Tau, Average base-flow recession time constant determined from daily values for March through April as defined in SIR 2008-5065', 'Tau Mar Apr');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (194, 'TAU_ANN_G', 'Tau, Average annual base-flow recession time constant as defined in SIR 2008-5065', 'Tau Annual from Grid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (193, 'PRNOVAPR90', 'Precipitation November-April basin average, mean seasonal from PRISM 1961-1990', 'Basin Ave Rainfall Nov Apr PRISM 1990');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (192, 'PRNOVAPR00', 'Precipitation November-April basin average, mean seasonal from PRISM 1971-2000', 'Basin Ave Rainfall Nov Apr PRISM 2000');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (191, 'ORDOMISS', 'Percent Surficial Geology as Ordovician and Mississippian Rocks', 'Percent SurficialGeology Ordo and Miss');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (190, 'ELV10_85', 'Average of channel elevations at points 10- and 85- percent above gage - Equivalent to Altitude Index', 'Elevation of 10 and 85 points');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (189, 'MINBELEV', 'Minimum basin elevation', 'Minimum Basin Elevation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (188, 'OR_HIPERMG', 'Percent basin surface area containing high permeability geologic units as defined in SIR 2008-5126', 'OR Percent HighPerm Geologic');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (187, 'MAXTEMP', 'Mean annual maximum air temperature over basin area from PRISM 1971-2000 800-m grid', 'Mean Annual Max Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (186, 'DRNDENSITY', 'Basin drainage density defined as total stream length divided by drainage area.', 'Basin Drainage Density');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (185, 'MAXBSLOPD', 'Maximum basin slope, in degrees, using ArcInfo Grid with NHDPlus 30-m resolution elevation data.', 'Maximum Basin Slope in deg');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (184, 'OR_HIPERMA', 'Percent basin surface area containing high permeability aquifer units as defined in SIR 2008-5126', 'OR Percent HighPerm Aquifer');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (183, 'MINBSLOPD', 'Minimum basin slope, in degrees, using ArcInfo Grid with NHDPlus 30-m resolution elevation data.', 'Minimum Basin Slope in deg');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (182, 'MINTEMP', 'Mean annual minimum air temperature over basin surface area as defined in SIR 2008-5126', 'Mean Annual Min Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (181, 'PRCWINTER', 'Mean annual precipitation for December through February', 'Mean Annual Winter Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (180, 'CRSDFT', 'Percentage of area of coarse-grained stratified drift', 'Percent Coarse Stratified Drift');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (179, 'PRECIPOUT', 'Mean annual precip at the stream outlet (based on annual PRISM precip data in inches from 1971-2000)', 'Mean Annual Precip at Gage');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (178, 'CANOPY_PCT', 'Percentage of drainage area covered by canopy as described in OK SIR 2009_5267', 'Percent Area Under Canopy');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (177, 'PREG_11_05', 'Mean monthly precipitation for November through May at the stream outlet', 'Nov to May Gage Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (202, 'SSURGOB', 'Percentage of area of Hydrologic Soil Type B from SSURGO', 'SSURGO Percent Hydrologic Soil Type B');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (203, 'SSURGOC', 'Percentage of area of Hydrologic Soil Type C from SSURGO', 'SSURGO Percent Hydrologic Soil Type C');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (204, 'SOILD', 'Percentage of area of  Hydrologic Soil Type D', 'Percent Hydrologic Soil Type D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (205, 'SSURGOD', 'Percentage of area of Hydrologic Soil Type D from SSURGO', 'SSURGO Percent Hydrologic Soil Type D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (231, 'STATSPERM', 'Area-weighted average soil permeability from NRCS STATSGO database', 'Average_Soil_Permeability_from_STATSGO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (230, 'QSSPERMTHK', 'Index of the permeability of surficial Quaternary sediments computed as in SIR 2014-5177', 'Permeability_Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (229, 'WATCAPINIL', 'Available water capacity  from Miller and White 1998 in cm per 100 cm', 'Available_Water_Capacity_for_IN_and_IL');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (228, 'DRAININD', 'Drainage index from STATSGO soil properties computed as in SIR 2014-5177', 'Drainage_Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (227, 'DETEN', 'Percentage of area of lakes and detention basins', 'Percent_Lake_and_Detention_Basin');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (226, 'STATSCLAY', 'Percentage of clay soils from STATSGO', 'STATSGO Percentage of Clay Soils');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (225, 'TAU_WIN_G', 'Tau, Average base-flow recession time constant for November through December as defined in SIR 2008-5065, estimated from a grid', 'Tau Nov Dec from Grid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (224, 'TAU_SPR_G', 'Tau, Average base-flow recession time constant for March through April as defined in SIR 2008-5065, estimated from a grid', 'Tau Mar Apr from Grid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (223, 'BSLDEM30ff', 'Mean basin slope computed from 30 m DEM in feet per foot', 'Mean Basin Slope from 30m DEM ft/ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (222, 'PRECPRIS00', 'Basin average mean annual precipitation for 1971 to 2000 from PRISM', 'Mean Annual Precip PRISM 1971 2000');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (221, 'STRMTOT', 'total length of all mapped streams (1:24,000-scale) in the basin', 'Stream Length Total');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (220, 'TRUN0711', 'Mean annual dry season total runoff, July through November', 'Dry Season Total Runoff');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (176, 'OUTLETELEV', 'Elevation of the stream outlet in thousands of feet above NAVD88.', 'Elevation of Gage');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (219, 'KYVARIND93', 'Mapped streamflow variability index as defined in WRIR 92-4173', 'KY Streamflow Variability Index 1993');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (217, 'AWETSG', 'Area of lakes and wetlands underlain by sand and gravel deposits', 'Wetland Area Underlain By Sand Gravel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (216, 'MARCFSM', 'Mean annual runoff for the period of record in cubic feet persecond', 'Mean Annual Runoff in cfsm');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (215, 'ATILL', 'Surface area covered by till deposits', 'Area of Till');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (214, 'ASANDGRAV', 'Area of land surface underlain by sand and gravel deposits', 'Area Underlain By Sand And Gravel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (213, 'PREBC_1112', 'Mean annual precipitation of basin centroid for November 1 to December 31 period', 'Nov to Dec Basin Centroid Precip');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (212, 'MINTEMP_W', 'Mean winter minimum air temperature over basin surface area', 'Mean Winter Min Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (211, 'PRECIPCENT', 'Mean Annual Precip at Basin Centroid', 'Mean Annual Precip at Basin Centroid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (210, 'LFPLENGTH', 'Length of longest flow path', 'LFP length');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (209, 'STRDENED', 'Stream Density -- total length of streams divided by drainage area, edited from NHD', 'Stream Density Edited');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (208, 'PERMSSUR', 'Area-weighted average soil permeability from NRCS SSURGO database', 'Average Soil Permeability from SSURGO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (207, 'STATSGORO', 'Drainage runoff number from STATSGO database where 1 is well and 7 is poor', 'Drainage Runoff Number STATSGO');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (206, 'HYSEP', 'Median percentage of baseflow to annual streamflow', 'Hydrograph separation percent');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (218, 'NY_UNDFLOW', 'Rate of underflow downvalley through sand and gravel according to NY 2010-5063', 'NY Underflow Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (233, 'FLC11DVLMH', 'Fraction of drainage area that is in low to high developed land-use classes 22-24 from NLCD 2011', 'Frac_Lo_Med_Hi_Developed_from_NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (175, 'TNSOILFAC', 'Tennessee soil factor, percentage of area underlain by a soil permeability greater than or equal to 2 inches per hour', 'Tennessee Soil Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (173, 'SEPAVPRE', 'Mean September Precipitation', 'Mean September Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (142, 'ELEVMAX', 'Maximum basin elevation', 'Maximum Basin Elevation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (141, 'GLACIER', 'Percentage of area of Glaciers', 'Percent Glaciers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (140, 'PERMGTE2IN', 'Percent of area underlain by soils with permeability greater than or equal to 2 inches per hour', 'Percent permeability gte 2 in per hr');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (139, 'RECESS', 'Number of days required for streamflow to recede one order of magnitude when hydrograph is plotted on logarithmic scale', 'Recession Index');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (138, 'NCMR', 'North Carolina mean annual runoff', 'North Carolina Mean Annual Runoff');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (137, 'STATSGOD', 'Percentage of area of Hydrologic Soil Type D from STATSGO', 'STATSGO Percent Hydrologic Soil Type D');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (136, 'STATSGOA', 'Percentage of area of Hydrologic Soil Type A from STATSGO', 'STATSGO Percent Hydrologic Soil Type A');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (135, 'FDRATIO', '20-percent flow duration divided by 90-percent flow duration', 'Flow Duration Ratio');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (134, 'KYVARIND10', 'Mapped streamflow-variability index as defined in SIR 2010-5217', 'KY Streamflow Variability Index 2010');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (133, 'MAREGION', 'Region of Massachusetts 0 for Eastern 1 for Western', 'Massachusetts Region');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (132, 'DRFTPERSTR', 'Area of stratified drift per unit of stream length', 'Stratified Drift per Stream Length');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (131, 'BSLDEM250', 'Mean basin slope computed from 1:250K DEM', 'Mean Basin Slope from 250K DEM');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (130, 'SLOP50', 'Slopes Greater Than 50 Percent as percent of drainage area', 'Slopes Greater Than 50 Percent');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (129, 'VOLCANIC', 'Percent of drainage area as surficial volcanic rocks as defined in SIR 2006-5035', 'Percent Volcanic');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (128, 'RELIEF', 'Maximum - minimum elevation', 'Relief');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (127, 'I48H5Y', 'Maximum 48-hour precipitation that occurs on average once in 5 years', '48 Hour 5 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (126, 'I48H2Y', 'Maximum 48-hour precipitation that occurs on average once in 2 years', '48 Hour 2 Year Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (125, 'EL1200', 'Percentage of basin at or above 1200 ft elevation', 'Percentage of Basin Above 1200 ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (124, 'SLOPERATIO', 'Ratio of main channel slope to basin slope as defined in SIR 2006-5112', 'Slope Ratio NY');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (123, 'MXSNO', '50th percentile of seasonal maximum snow depth from Northeast Regional Climate Center atlas by Cember and Wilks, 1993', 'Median Seasonal Maximum Snow Depth');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (122, 'MAR', 'Mean annual runoff for the period of record in inches', 'Mean Annual Runoff in inches');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (121, 'LAGFACTOR', 'Lag Factor as defined in SIR 2006-5112', 'Lag Factor');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (120, 'LC11DEV', 'Percentage of developed (urban) land from NLCD 2011 classes 21-24', 'Percent Developed from NLCD2011');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (119, 'DEVNLCD01', 'Percentage of land-use categories 21-24 from NLCD 2001', 'Percent developed from NLCD2001');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (118, 'HOMEDENS', 'Average homes per acre in watershed', 'Housing Density');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (143, 'HIELONGRAT', 'Ratio of (1)diameter of circle with equal area as basin to (2)basin length, as defined in SIR 2004-5262', 'Hawaii Elongation Ratio');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (144, 'HIMARRATE', 'Mean annual rainfall rate determined based on the method described in SIR 2004-5262', 'Hawaii Mean Rainfall Rate');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (145, 'PCTSNDGRV', 'Percentage of land surface underlain by sand and gravel deposits', 'Percent Underlain By Sand And Gravel');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (146, 'SANDGRAVAF', 'Fraction of land surface underlain by sand and gravel aquifers', 'Fraction of Sand and Gravel Aquifers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (172, 'NOVAVPRE', 'Mean November Precipitation', 'Mean November Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (171, 'AUGAVPRE', 'Mean August Precipitation', 'Mean August Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (170, 'SLOP30_10M', 'Percent area with slopes greater than 30 percent from 10-meter NED', 'Slopes gt 30pct from 10m NED');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (169, 'DECAVPRE', 'Mean December Precipitation', 'Mean December Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (168, 'JUNAVPRE', 'Mean June Precipitation', 'Mean June Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (167, 'FEBAVPRE', 'Mean February Precipitation', 'Mean February Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (166, 'AG_OF_DA', 'Agricultural Land in Percentage of Drainage Area (Idaho Logistic Regression Equations SIR 2006-5035', 'Ag Land Percentage');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (165, 'DV_OF_DA', 'Developed Land in Percentage of Drainage Area (Idaho Logistic Regression Equations SIR 2006-5035', 'Dev Land percentage');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (164, 'BSLOPGM', 'Mean basin slope determined using the grid-sampling method', 'Mean Basin Slope ft per ft');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (163, 'BASINPERIM', 'Perimeter of the drainage basin as defined in SIR 2004-5262', 'Basin Perimeter');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (162, 'GLACIATED', 'Percentage of basin area that was historically covered by glaciers', 'Percent of Glaciation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (161, 'BSLOPD', 'Mean basin slope measured in degrees', 'Mean Basin Slope degrees');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (174, 'OCTAVPRE', 'Mean October Precipitation', 'Mean October Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (160, 'LONG_CENT', 'Longitude Basin Centroid', 'Longitude of Basin Centroid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (158, 'STREAM_VARG', 'Streamflow variability index as defined in WRIR 02-4068, computed from regional grid', 'Streamflow Variability Index from Grid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (157, 'PREC10to4', 'Mean precipitation for winter period defined as October to April', 'Mean Oct to Apr Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (156, 'PREG_03_05', 'Mean precipitation at gaging station location for March 16 to May 31 spring period', 'Mar to May Gage Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (155, 'MIXFOR', 'Percentage of land area covered by mixed deciduous and coniferous forest', 'Percent Mixed Forest');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (154, 'PREG_06_10', 'Mean precipitation at gaging station location for June to October summer period', 'Jun to Oct Gage Precipitation');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (153, 'TEMP_06_10', 'Basinwide average temperature for June to October summer period', 'Jun to Oct Mean Basinwide Temp');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (152, 'TEMP', 'Mean Annual Temperature', 'Mean Annual Temperature');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (151, 'PREBC0103', 'Mean annual precipitation of basin centroid for January 1 to March 15 winter period', 'Jan to Mar Basin Centroid Precip');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (150, 'CONIF', 'Percentaqe of land surface covered by coniferous forest', 'Percent Coniferous Forest');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (149, 'PRDECFEB90', 'Basin average mean precipitation for December to  February from PRISM 1961-1990', 'Basin Ave Precip Dec Feb PRISM 1990');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (148, 'COASTDIST', 'Shortest distance from the coastline to the basin centroid', 'Distance From Coast To Basin Centroid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (147, 'SANDGRAVAP', 'Percentage of land surface underlain by sand and gravel aquifers', 'Percentage of Sand and Gravel Aquifers');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (159, 'LAT_CENT', 'Latitude of Basin Centroid', 'Latitude of Basin Centroid');
INSERT INTO shared."VariableType" ("ID", "Code", "Description", "Name")
VALUES (468, 'JULAVPRE2K', 'Mean July Average Precipitation', 'Mean July Precipitation');

INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (2, 'km^2', 'square kilometers', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (42, 'in/hr', 'inches per hour', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (43, 'mi/mi', 'miles per mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (44, 'ft^3/s', 'cubic feet per second', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (45, 'ft/ft', 'foot per foot', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (46, '1-O str/mi^2', '1st-order streams per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (47, 'mi/mi^2', 'miles per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (48, 'persons/mi^2', 'persons per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (49, 'mi^2/mi^2', 'square mile per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (50, 'in/in', 'inch per inch', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (51, 'in/day', 'in per day', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (52, 'ft/day', 'ft per day', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (53, 'mi^2/mi', 'square mile per mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (41, 'ft', 'feet', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (54, '/mi^2', 'per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (56, 'ft^3/s/mi^2', 'cubic feet per second per square mile', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (57, 'ft^2', 'square feet', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (60, 'ft^2/day', 'square feet per day', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (1, 'dim', 'dimensionless', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (11, '%', 'percent', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (12, 'dec fract', 'decimal fraction', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (18, 'dec deg', 'decimal degrees', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (25, 'hrs', 'hours', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (26, 'yrs', 'years', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (27, 'Log 10', 'Log base 10', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (29, 'days/log cycle', 'days per log cycle', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (30, 'days', 'days', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (55, 'homes/acre', 'homes per acre', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (33, '(log 10)^2', 'Log base 10 squared', 3);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (40, 'mi', 'miles', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (38, '1000 ft', 'thousand feet', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (3, 'mm', 'millimeters', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (4, 'deg C', 'degrees C', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (5, 'km', 'kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (6, 'm/km', 'meters per kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (8, 'm', 'meters', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (9, 'mm/hr', 'millimeters per hr', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (10, 'km/km', 'kilometer per kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (13, 'm^3/s', 'cubic meter per second', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (14, 'm/m', 'meter per meter', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (15, '1-O str/km^2', '1st-order streams per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (16, 'km/km^2', 'kilometer per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (17, 'persons/km^2', 'persons per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (39, 'ft/mi', 'feet per mi', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (19, 'km^2/km^2', 'square kilometer per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (21, 'mm/day', 'millimeter per day', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (22, 'm/day', 'meter per day', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (23, 'km^2/km', 'square kilometer per kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (24, '/km^2', 'per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (28, 'homes/km^2', 'homes per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (31, 'm^3/s/km^2', 'cubic meters per second per square kilometer', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (32, 'm^2', 'square meters', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (34, 'um/s', 'micrometers per second', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (59, 'm^2/day', 'square meters per day', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (35, 'mi^2', 'square miles', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (36, 'in', 'inches', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (37, 'deg F', 'degrees F', 2);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (20, 'mm/mm', 'millimeter per millimeter', 1);
INSERT INTO shared."UnitType" ("ID", "Abbreviation", "Name", "UnitSystemTypeID")
VALUES (58, '°', 'degrees', 3);

INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (1, 2.5899999999999999, 35, 2);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (14, 0.3861, 48, 17);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (37, 2.5900025900000001, 17, 48);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (15, 1.0, 49, 19);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (38, 1.0, 19, 49);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (16, 1.0, 50, 20);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (39, 1.0, 20, 50);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (17, 25.399999999999999, 51, 21);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (40, 0.039370079000000002, 21, 51);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (18, 0.30480000000000002, 52, 22);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (36, 1.6097875079999999, 16, 47);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (41, 3.2808398950000002, 22, 52);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (42, 0.62150404000000004, 23, 53);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (20, 0.3861, 54, 24);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (43, 2.5900025900000001, 24, 54);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (21, 0.0040470000000000002, 55, 28);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (44, 247.0966148, 28, 55);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (22, 0.010933999999999999, 56, 31);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (45, 91.457837940000005, 31, 56);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (23, 0.092902999999999999, 57, 32);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (46, 10.76391505, 32, 57);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (19, 1.609, 53, 23);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (13, 0.62119999999999997, 47, 16);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (35, 2.5899891739999998, 15, 46);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (12, 0.386102, 46, 15);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (24, 0.38610038600000002, 2, 35);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (2, 25.399999999999999, 36, 3);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (25, 0.039370079000000002, 3, 36);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (3, 0.55549999999999999, 37, 4);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (26, 1.800180018, 4, 37);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (4, 0.30480000000000002, 38, 5);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (27, 3.2808398950000002, 5, 38);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (5, 0.18940000000000001, 39, 6);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (28, 5.2798310449999999, 6, 39);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (6, 1.609, 40, 5);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (29, 0.62150404000000004, 5, 40);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (7, 0.30480000000000002, 41, 8);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (30, 3.2808398950000002, 8, 41);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (8, 25.399999999999999, 42, 9);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (31, 0.039370079000000002, 9, 42);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (9, 1.0, 43, 10);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (32, 1.0, 10, 43);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (10, 0.028320000000000001, 44, 13);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (33, 35.310734459999999, 13, 44);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (11, 1.0, 45, 14);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (34, 1.0, 14, 45);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (47, 0.092902999999999999, 60, 59);
INSERT INTO shared."UnitConversionFactor" ("ID", "Factor", "UnitTypeInID", "UnitTypeOutID")
VALUES (48, 10.76391505118, 59, 60);

CREATE UNIQUE INDEX "IX_ErrorType_Code" ON shared."ErrorType" ("Code");

CREATE UNIQUE INDEX "IX_RegressionType_Code" ON shared."RegressionType" ("Code");

CREATE UNIQUE INDEX "IX_StatisticGroupType_Code" ON shared."StatisticGroupType" ("Code");

CREATE INDEX "IX_UnitConversionFactor_UnitTypeInID" ON shared."UnitConversionFactor" ("UnitTypeInID");

CREATE INDEX "IX_UnitConversionFactor_UnitTypeOutID" ON shared."UnitConversionFactor" ("UnitTypeOutID");

CREATE UNIQUE INDEX "IX_UnitSystemType_UnitSystem" ON shared."UnitSystemType" ("UnitSystem");

CREATE UNIQUE INDEX "IX_UnitType_Abbreviation" ON shared."UnitType" ("Abbreviation");

CREATE UNIQUE INDEX "IX_UnitType_Name" ON shared."UnitType" ("Name");

CREATE INDEX "IX_UnitType_UnitSystemTypeID" ON shared."UnitType" ("UnitSystemTypeID");

CREATE UNIQUE INDEX "IX_VariableType_Code" ON shared."VariableType" ("Code");

INSERT INTO shared."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20181019185249_init', '3.1.3');

ALTER TABLE shared."VariableType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."VariableType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."VariableType_ID_seq" RENAME TO "VariableType_ID_old_seq";
ALTER TABLE shared."VariableType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."VariableType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."VariableType_ID_seq"', nextval('shared."VariableType_ID_old_seq"'), false);
DROP SEQUENCE shared."VariableType_ID_old_seq";

ALTER TABLE shared."VariableType" ADD "EnglishUnitTypeID" integer NULL;

ALTER TABLE shared."VariableType" ADD "MetricUnitTypeID" integer NULL;

ALTER TABLE shared."VariableType" ADD "StatisticGroupTypeID" integer NULL;

ALTER TABLE shared."UnitType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."UnitType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."UnitType_ID_seq" RENAME TO "UnitType_ID_old_seq";
ALTER TABLE shared."UnitType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."UnitType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."UnitType_ID_seq"', nextval('shared."UnitType_ID_old_seq"'), false);
DROP SEQUENCE shared."UnitType_ID_old_seq";

ALTER TABLE shared."UnitSystemType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."UnitSystemType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."UnitSystemType_ID_seq" RENAME TO "UnitSystemType_ID_old_seq";
ALTER TABLE shared."UnitSystemType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."UnitSystemType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."UnitSystemType_ID_seq"', nextval('shared."UnitSystemType_ID_old_seq"'), false);
DROP SEQUENCE shared."UnitSystemType_ID_old_seq";

ALTER TABLE shared."UnitConversionFactor" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."UnitConversionFactor" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."UnitConversionFactor_ID_seq" RENAME TO "UnitConversionFactor_ID_old_seq";
ALTER TABLE shared."UnitConversionFactor" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."UnitConversionFactor" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."UnitConversionFactor_ID_seq"', nextval('shared."UnitConversionFactor_ID_old_seq"'), false);
DROP SEQUENCE shared."UnitConversionFactor_ID_old_seq";

ALTER TABLE shared."StatisticGroupType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."StatisticGroupType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."StatisticGroupType_ID_seq" RENAME TO "StatisticGroupType_ID_old_seq";
ALTER TABLE shared."StatisticGroupType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."StatisticGroupType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."StatisticGroupType_ID_seq"', nextval('shared."StatisticGroupType_ID_old_seq"'), false);
DROP SEQUENCE shared."StatisticGroupType_ID_old_seq";

ALTER TABLE shared."StatisticGroupType" ADD "DefType" text NULL;

ALTER TABLE shared."RegressionType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."RegressionType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."RegressionType_ID_seq" RENAME TO "RegressionType_ID_old_seq";
ALTER TABLE shared."RegressionType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."RegressionType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."RegressionType_ID_seq"', nextval('shared."RegressionType_ID_old_seq"'), false);
DROP SEQUENCE shared."RegressionType_ID_old_seq";

ALTER TABLE shared."ErrorType" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE shared."ErrorType" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE shared."ErrorType_ID_seq" RENAME TO "ErrorType_ID_old_seq";
ALTER TABLE shared."ErrorType" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE shared."ErrorType" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('shared."ErrorType_ID_seq"', nextval('shared."ErrorType_ID_old_seq"'), false);
DROP SEQUENCE shared."ErrorType_ID_old_seq";

CREATE INDEX "IX_VariableType_EnglishUnitTypeID" ON shared."VariableType" ("EnglishUnitTypeID");

CREATE INDEX "IX_VariableType_MetricUnitTypeID" ON shared."VariableType" ("MetricUnitTypeID");

CREATE INDEX "IX_VariableType_StatisticGroupTypeID" ON shared."VariableType" ("StatisticGroupTypeID");

ALTER TABLE shared."VariableType" ADD CONSTRAINT "FK_VariableType_UnitType_EnglishUnitTypeID" FOREIGN KEY ("EnglishUnitTypeID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT;

ALTER TABLE shared."VariableType" ADD CONSTRAINT "FK_VariableType_UnitType_MetricUnitTypeID" FOREIGN KEY ("MetricUnitTypeID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT;

ALTER TABLE shared."VariableType" ADD CONSTRAINT "FK_VariableType_StatisticGroupType_StatisticGroupTypeID" FOREIGN KEY ("StatisticGroupTypeID") REFERENCES shared."StatisticGroupType" ("ID") ON DELETE RESTRICT;

INSERT INTO shared."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201026160418_addVariableUnitTypes', '3.1.3');

ALTER TABLE shared."VariableType" ALTER COLUMN "StatisticGroupTypeID" TYPE integer;
ALTER TABLE shared."VariableType" ALTER COLUMN "StatisticGroupTypeID" DROP NOT NULL;
ALTER TABLE shared."VariableType" ALTER COLUMN "StatisticGroupTypeID" DROP DEFAULT;

ALTER TABLE shared."VariableType" ALTER COLUMN "MetricUnitTypeID" TYPE integer;
ALTER TABLE shared."VariableType" ALTER COLUMN "MetricUnitTypeID" DROP NOT NULL;
ALTER TABLE shared."VariableType" ALTER COLUMN "MetricUnitTypeID" DROP DEFAULT;

ALTER TABLE shared."VariableType" ALTER COLUMN "EnglishUnitTypeID" TYPE integer;
ALTER TABLE shared."VariableType" ALTER COLUMN "EnglishUnitTypeID" DROP NOT NULL;
ALTER TABLE shared."VariableType" ALTER COLUMN "EnglishUnitTypeID" DROP DEFAULT;

ALTER TABLE shared."RegressionType" ADD "EnglishUnitTypeID" integer NULL;

ALTER TABLE shared."RegressionType" ADD "MetricUnitTypeID" integer NULL;

ALTER TABLE shared."RegressionType" ADD "StatisticGroupTypeID" integer NULL;

CREATE INDEX "IX_RegressionType_EnglishUnitTypeID" ON shared."RegressionType" ("EnglishUnitTypeID");

CREATE INDEX "IX_RegressionType_MetricUnitTypeID" ON shared."RegressionType" ("MetricUnitTypeID");

CREATE INDEX "IX_RegressionType_StatisticGroupTypeID" ON shared."RegressionType" ("StatisticGroupTypeID");

ALTER TABLE shared."RegressionType" ADD CONSTRAINT "FK_RegressionType_UnitType_EnglishUnitTypeID" FOREIGN KEY ("EnglishUnitTypeID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT;

ALTER TABLE shared."RegressionType" ADD CONSTRAINT "FK_RegressionType_UnitType_MetricUnitTypeID" FOREIGN KEY ("MetricUnitTypeID") REFERENCES shared."UnitType" ("ID") ON DELETE RESTRICT;

ALTER TABLE shared."RegressionType" ADD CONSTRAINT "FK_RegressionType_StatisticGroupType_StatisticGroupTypeID" FOREIGN KEY ("StatisticGroupTypeID") REFERENCES shared."StatisticGroupType" ("ID") ON DELETE RESTRICT;

INSERT INTO shared."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210813142650_addUnitTypeStatGroups', '3.1.3');

