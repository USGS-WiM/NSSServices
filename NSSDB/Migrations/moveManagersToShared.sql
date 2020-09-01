CREATE SCHEMA IF NOT EXISTS nss;
CREATE TABLE IF NOT EXISTS nss."_EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK__EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS nss;

CREATE EXTENSION IF NOT EXISTS postgis;

CREATE TABLE nss."Citations" (
    "ID" serial NOT NULL,
    "Title" text NOT NULL,
    "Author" text NOT NULL,
    "CitationURL" text NOT NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Citations" PRIMARY KEY ("ID")
);

CREATE TABLE nss."Locations" (
    "ID" serial NOT NULL,
    "Geometry" geometry NOT NULL,
    "AssociatedCodes" text NULL,
    CONSTRAINT "PK_Locations" PRIMARY KEY ("ID")
);

CREATE TABLE nss."PredictionIntervals" (
    "ID" serial NOT NULL,
    "BiasCorrectionFactor" double precision NULL,
    "Student_T_Statistic" double precision NULL,
    "Variance" double precision NULL,
    "XIRowVector" text NULL,
    "CovarianceMatrix" text NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_PredictionIntervals" PRIMARY KEY ("ID")
);

CREATE TABLE nss."Regions" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "Description" text NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Regions" PRIMARY KEY ("ID")
);

CREATE TABLE nss."Roles" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("ID")
);

CREATE TABLE nss."Status" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Description" text NULL,
    CONSTRAINT "PK_Status" PRIMARY KEY ("ID")
);

CREATE TABLE nss."Managers" (
    "ID" serial NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Username" text NOT NULL,
    "Email" text NOT NULL,
    "PrimaryPhone" text NULL,
    "SecondaryPhone" text NULL,
    "RoleID" integer NOT NULL,
    "OtherInfo" text NULL,
    "Password" text NOT NULL,
    "Salt" text NOT NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Managers" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Managers_Roles_RoleID" FOREIGN KEY ("RoleID") REFERENCES nss."Roles" ("ID") ON DELETE RESTRICT
);

CREATE TABLE nss."RegressionRegions" (
    "ID" serial NOT NULL,
    "Name" text NOT NULL,
    "Code" text NOT NULL,
    "Description" text NULL,
    "CitationID" integer NOT NULL,
    "StatusID" integer NULL,
    "LocationID" integer NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_RegressionRegions" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_RegressionRegions_Citations_CitationID" FOREIGN KEY ("CitationID") REFERENCES nss."Citations" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_RegressionRegions_Locations_LocationID" FOREIGN KEY ("LocationID") REFERENCES nss."Locations" ("ID") ON DELETE RESTRICT,
    CONSTRAINT "FK_RegressionRegions_Status_StatusID" FOREIGN KEY ("StatusID") REFERENCES nss."Status" ("ID") ON DELETE RESTRICT
);

CREATE TABLE nss."RegionManager" (
    "RegionID" integer NOT NULL,
    "ManagerID" integer NOT NULL,
    CONSTRAINT "PK_RegionManager" PRIMARY KEY ("ManagerID", "RegionID"),
    CONSTRAINT "FK_RegionManager_Managers_ManagerID" FOREIGN KEY ("ManagerID") REFERENCES nss."Managers" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_RegionManager_Regions_RegionID" FOREIGN KEY ("RegionID") REFERENCES nss."Regions" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."Coefficients" (
    "ID" serial NOT NULL,
    "RegressionRegionID" integer NOT NULL,
    "Criteria" text NOT NULL,
    "Description" text NULL,
    "Value" text NOT NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Coefficients" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Coefficients_RegressionRegions_RegressionRegionID" FOREIGN KEY ("RegressionRegionID") REFERENCES nss."RegressionRegions" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."Equations" (
    "ID" serial NOT NULL,
    "RegressionRegionID" integer NOT NULL,
    "PredictionIntervalID" integer NULL,
    "UnitTypeID" integer NOT NULL,
    "Expression" text NOT NULL,
    "DA_Exponent" double precision NULL,
    "OrderIndex" integer NULL,
    "RegressionTypeID" integer NOT NULL,
    "StatisticGroupTypeID" integer NOT NULL,
    "EquivalentYears" double precision NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Equations" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Equations_PredictionIntervals_PredictionIntervalID" FOREIGN KEY ("PredictionIntervalID") REFERENCES nss."PredictionIntervals" ("ID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Equations_RegressionRegions_RegressionRegionID" FOREIGN KEY ("RegressionRegionID") REFERENCES nss."RegressionRegions" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."Limitations" (
    "ID" serial NOT NULL,
    "Criteria" text NOT NULL,
    "Description" text NULL,
    "RegressionRegionID" integer NOT NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Limitations" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Limitations_RegressionRegions_RegressionRegionID" FOREIGN KEY ("RegressionRegionID") REFERENCES nss."RegressionRegions" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."RegionRegressionRegions" (
    "RegionID" integer NOT NULL,
    "RegressionRegionID" integer NOT NULL,
    CONSTRAINT "PK_RegionRegressionRegions" PRIMARY KEY ("RegionID", "RegressionRegionID"),
    CONSTRAINT "FK_RegionRegressionRegions_Regions_RegionID" FOREIGN KEY ("RegionID") REFERENCES nss."Regions" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_RegionRegressionRegions_RegressionRegions_RegressionRegionID" FOREIGN KEY ("RegressionRegionID") REFERENCES nss."RegressionRegions" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."EquationErrors" (
    "ID" serial NOT NULL,
    "EquationID" integer NOT NULL,
    "ErrorTypeID" integer NOT NULL,
    "Value" double precision NOT NULL,
    CONSTRAINT "PK_EquationErrors" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_EquationErrors_Equations_EquationID" FOREIGN KEY ("EquationID") REFERENCES nss."Equations" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."EquationUnitTypes" (
    "EquationID" integer NOT NULL,
    "UnitTypeID" integer NOT NULL,
    CONSTRAINT "PK_EquationUnitTypes" PRIMARY KEY ("EquationID", "UnitTypeID"),
    CONSTRAINT "FK_EquationUnitTypes_Equations_EquationID" FOREIGN KEY ("EquationID") REFERENCES nss."Equations" ("ID") ON DELETE CASCADE
);

CREATE TABLE nss."Variables" (
    "ID" serial NOT NULL,
    "EquationID" integer NULL,
    "VariableTypeID" integer NOT NULL,
    "RegressionTypeID" integer NULL,
    "UnitTypeID" integer NOT NULL,
    "MinValue" double precision NULL,
    "MaxValue" double precision NULL,
    "Comments" text NULL,
    "LimitationID" integer NULL,
    "CoefficientID" integer NULL,
    "LastModified" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Variables" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Variables_Coefficients_CoefficientID" FOREIGN KEY ("CoefficientID") REFERENCES nss."Coefficients" ("ID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Variables_Equations_EquationID" FOREIGN KEY ("EquationID") REFERENCES nss."Equations" ("ID") ON DELETE RESTRICT,
    CONSTRAINT "FK_Variables_Limitations_LimitationID" FOREIGN KEY ("LimitationID") REFERENCES nss."Limitations" ("ID") ON DELETE RESTRICT
);

CREATE TABLE nss."VariableUnitTypes" (
    "VariableID" integer NOT NULL,
    "UnitTypeID" integer NOT NULL,
    CONSTRAINT "PK_VariableUnitTypes" PRIMARY KEY ("VariableID", "UnitTypeID"),
    CONSTRAINT "FK_VariableUnitTypes_Variables_VariableID" FOREIGN KEY ("VariableID") REFERENCES nss."Variables" ("ID") ON DELETE CASCADE
);

INSERT INTO nss."Roles" ("ID", "Description", "Name")
VALUES (1, 'System Administrator', 'Admin');
INSERT INTO nss."Roles" ("ID", "Description", "Name")
VALUES (2, 'Region Manager', 'Manager');

INSERT INTO nss."Status" ("ID", "Description", "Name")
VALUES (1, 'Working and disabled for all public users', 'Work/Disabled');
INSERT INTO nss."Status" ("ID", "Description", "Name")
VALUES (2, 'Reviewing and disabled for all public users', 'Review');
INSERT INTO nss."Status" ("ID", "Description", "Name")
VALUES (3, 'Approved and enabled for public NSS users', 'Approved');
INSERT INTO nss."Status" ("ID", "Description", "Name")
VALUES (4, 'Approved and enabled for public StreamStats users', 'SS Approved');

CREATE INDEX "IX_Coefficients_RegressionRegionID" ON nss."Coefficients" ("RegressionRegionID");

CREATE INDEX "IX_EquationErrors_EquationID" ON nss."EquationErrors" ("EquationID");

CREATE INDEX "IX_Equations_PredictionIntervalID" ON nss."Equations" ("PredictionIntervalID");

CREATE INDEX "IX_Equations_RegressionRegionID" ON nss."Equations" ("RegressionRegionID");

CREATE INDEX "IX_Limitations_RegressionRegionID" ON nss."Limitations" ("RegressionRegionID");

CREATE INDEX "IX_Managers_RoleID" ON nss."Managers" ("RoleID");

CREATE INDEX "IX_Managers_Username" ON nss."Managers" ("Username");

CREATE INDEX "IX_RegionManager_RegionID" ON nss."RegionManager" ("RegionID");

CREATE INDEX "IX_RegionRegressionRegions_RegressionRegionID" ON nss."RegionRegressionRegions" ("RegressionRegionID");

CREATE INDEX "IX_Regions_Code" ON nss."Regions" ("Code");

CREATE INDEX "IX_RegressionRegions_CitationID" ON nss."RegressionRegions" ("CitationID");

CREATE INDEX "IX_RegressionRegions_Code" ON nss."RegressionRegions" ("Code");

CREATE INDEX "IX_RegressionRegions_LocationID" ON nss."RegressionRegions" ("LocationID");

CREATE INDEX "IX_RegressionRegions_StatusID" ON nss."RegressionRegions" ("StatusID");

CREATE INDEX "IX_Variables_CoefficientID" ON nss."Variables" ("CoefficientID");

CREATE INDEX "IX_Variables_EquationID" ON nss."Variables" ("EquationID");

CREATE INDEX "IX_Variables_LimitationID" ON nss."Variables" ("LimitationID");


                CREATE OR REPLACE FUNCTION "nss"."trigger_set_lastmodified"()
                    RETURNS TRIGGER AS $$
                    BEGIN
                      NEW."LastModified" = NOW();
                      RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                


                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON "Citations"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON "Equations"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  "Limitations" FOR EACH ROW EXECUTE PROCEDURE  "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON "Managers"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  "PredictionIntervals" FOR EACH ROW EXECUTE PROCEDURE  "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON "Regions"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  "RegressionRegions" FOR EACH ROW EXECUTE PROCEDURE  "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  "Coefficients" FOR EACH ROW EXECUTE PROCEDURE  "nss"."trigger_set_lastmodified"();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON "Variables"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_set_lastmodified"();
                

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190313184751_init', '3.1.3');

ALTER TABLE nss."RegressionRegions" DROP CONSTRAINT "FK_RegressionRegions_Citations_CitationID";

ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" TYPE integer;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" DROP NOT NULL;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" DROP DEFAULT;

ALTER TABLE nss."RegressionRegions" ADD CONSTRAINT "FK_RegressionRegions_Citations_CitationID" FOREIGN KEY ("CitationID") REFERENCES nss."Citations" ("ID") ON DELETE RESTRICT;

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190319150055_dbgenerated', '3.1.3');

ALTER TABLE nss."Variables" DROP CONSTRAINT "FK_Variables_Equations_EquationID";

ALTER TABLE nss."Variables" ADD CONSTRAINT "FK_Variables_Equations_EquationID" FOREIGN KEY ("EquationID") REFERENCES nss."Equations" ("ID") ON DELETE CASCADE;

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190517134523_cascadeDeleteVariables', '3.1.3');

ALTER TABLE nss."Managers" DROP CONSTRAINT "FK_Managers_Roles_RoleID";

DROP TABLE nss."Roles";

DROP INDEX nss."IX_Managers_RoleID";

ALTER TABLE nss."Managers" DROP COLUMN "RoleID";

ALTER TABLE nss."Managers" ADD "Role" text NOT NULL DEFAULT '';

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190531165646_removeRoles', '3.1.3');

ALTER TABLE nss."Variables" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Variables" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Variables_ID_seq" RENAME TO "Variables_ID_old_seq";
ALTER TABLE nss."Variables" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Variables" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Variables_ID_seq"', nextval('nss."Variables_ID_old_seq"'), false);
DROP SEQUENCE nss."Variables_ID_old_seq";

ALTER TABLE nss."Status" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Status" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Status_ID_seq" RENAME TO "Status_ID_old_seq";
ALTER TABLE nss."Status" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Status" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Status_ID_seq"', nextval('nss."Status_ID_old_seq"'), false);
DROP SEQUENCE nss."Status_ID_old_seq";

ALTER TABLE nss."RegressionRegions" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."RegressionRegions_ID_seq" RENAME TO "RegressionRegions_ID_old_seq";
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."RegressionRegions_ID_seq"', nextval('nss."RegressionRegions_ID_old_seq"'), false);
DROP SEQUENCE nss."RegressionRegions_ID_old_seq";

ALTER TABLE nss."Regions" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Regions" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Regions_ID_seq" RENAME TO "Regions_ID_old_seq";
ALTER TABLE nss."Regions" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Regions" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Regions_ID_seq"', nextval('nss."Regions_ID_old_seq"'), false);
DROP SEQUENCE nss."Regions_ID_old_seq";

ALTER TABLE nss."PredictionIntervals" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."PredictionIntervals" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."PredictionIntervals_ID_seq" RENAME TO "PredictionIntervals_ID_old_seq";
ALTER TABLE nss."PredictionIntervals" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."PredictionIntervals" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."PredictionIntervals_ID_seq"', nextval('nss."PredictionIntervals_ID_old_seq"'), false);
DROP SEQUENCE nss."PredictionIntervals_ID_old_seq";

ALTER TABLE nss."Managers" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Managers" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Managers_ID_seq" RENAME TO "Managers_ID_old_seq";
ALTER TABLE nss."Managers" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Managers" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Managers_ID_seq"', nextval('nss."Managers_ID_old_seq"'), false);
DROP SEQUENCE nss."Managers_ID_old_seq";

ALTER TABLE nss."Locations" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Locations" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Locations_ID_seq" RENAME TO "Locations_ID_old_seq";
ALTER TABLE nss."Locations" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Locations" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Locations_ID_seq"', nextval('nss."Locations_ID_old_seq"'), false);
DROP SEQUENCE nss."Locations_ID_old_seq";

ALTER TABLE nss."Limitations" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Limitations" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Limitations_ID_seq" RENAME TO "Limitations_ID_old_seq";
ALTER TABLE nss."Limitations" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Limitations" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Limitations_ID_seq"', nextval('nss."Limitations_ID_old_seq"'), false);
DROP SEQUENCE nss."Limitations_ID_old_seq";

ALTER TABLE nss."Equations" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Equations" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Equations_ID_seq" RENAME TO "Equations_ID_old_seq";
ALTER TABLE nss."Equations" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Equations" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Equations_ID_seq"', nextval('nss."Equations_ID_old_seq"'), false);
DROP SEQUENCE nss."Equations_ID_old_seq";

ALTER TABLE nss."EquationErrors" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."EquationErrors" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."EquationErrors_ID_seq" RENAME TO "EquationErrors_ID_old_seq";
ALTER TABLE nss."EquationErrors" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."EquationErrors" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."EquationErrors_ID_seq"', nextval('nss."EquationErrors_ID_old_seq"'), false);
DROP SEQUENCE nss."EquationErrors_ID_old_seq";

ALTER TABLE nss."Coefficients" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Coefficients" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Coefficients_ID_seq" RENAME TO "Coefficients_ID_old_seq";
ALTER TABLE nss."Coefficients" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Coefficients" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Coefficients_ID_seq"', nextval('nss."Coefficients_ID_old_seq"'), false);
DROP SEQUENCE nss."Coefficients_ID_old_seq";

ALTER TABLE nss."Citations" ALTER COLUMN "ID" TYPE integer;
ALTER TABLE nss."Citations" ALTER COLUMN "ID" SET NOT NULL;
ALTER SEQUENCE nss."Citations_ID_seq" RENAME TO "Citations_ID_old_seq";
ALTER TABLE nss."Citations" ALTER COLUMN "ID" DROP DEFAULT;
ALTER TABLE nss."Citations" ALTER COLUMN "ID" ADD GENERATED BY DEFAULT AS IDENTITY;
SELECT * FROM setval('nss."Citations_ID_seq"', nextval('nss."Citations_ID_old_seq"'), false);
DROP SEQUENCE nss."Citations_ID_old_seq";


                CREATE OR REPLACE FUNCTION "nss"."trigger_enforce_geometry"()
                    RETURNS TRIGGER AS $$
                    BEGIN
                      NEW."Geometry" = ST_Transform(NEW."Geometry", 102008);
                      RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                


                CREATE TRIGGER enforcegeometry BEFORE INSERT OR UPDATE ON "Locations"  FOR EACH ROW EXECUTE PROCEDURE "nss"."trigger_enforce_geometry"();
                

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200522173033_enforceProjection', '3.1.3');

ALTER TABLE nss."RegressionRegions" ADD "DateStatusModified" text NOT NULL DEFAULT (NOW());


                CREATE OR REPLACE FUNCTION "nss"."trigger_set_datestatusmodified"()
                    RETURNS TRIGGER AS $$
                    BEGIN
                    if TG_OP = 'INSERT' OR OLD."StatusID" IS DISTINCT FROM NEW."StatusID" then
                      NEW."DateStatusModified" = NOW();
                    end if;
                    RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                


                CREATE TRIGGER laststatusupdate BEFORE INSERT OR UPDATE ON  "RegressionRegions" FOR EACH ROW EXECUTE PROCEDURE  "nss"."trigger_set_datestatusmodified"();
                

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200630215319_addDateStatusModified', '3.1.3');

ALTER TABLE nss."RegressionRegions" ADD "MethodID" integer NULL;

CREATE TABLE nss."Methods" (
    "ID" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Code" text NULL,
    CONSTRAINT "PK_Methods" PRIMARY KEY ("ID")
);

CREATE INDEX "IX_RegressionRegions_MethodID" ON nss."RegressionRegions" ("MethodID");

ALTER TABLE nss."RegressionRegions" ADD CONSTRAINT "FK_RegressionRegions_Methods_MethodID" FOREIGN KEY ("MethodID") REFERENCES nss."Methods" ("ID") ON DELETE RESTRICT;

INSERT INTO nss."Methods" ("ID", "Code", "Name")
VALUES (1, 'GLS', 'Generalized least squares');
INSERT INTO nss."Methods" ("ID", "Code", "Name")
VALUES (2, 'WLS', 'Weighted least squares');
INSERT INTO nss."Methods" ("ID", "Code", "Name")
VALUES (3, 'OLS', 'Ordinary least squares');

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200804170648_addMethod', '3.1.3');

ALTER TABLE nss."PredictionIntervals" ADD "DegreesOfFreedom" double precision NULL;

CREATE TABLE shared."Managers" (
    "ID" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Email" text NOT NULL,
    "FirstName" text NOT NULL,
    "LastModified" timestamp without time zone NOT NULL,
    "LastName" text NOT NULL,
    "OtherInfo" text NULL,
    "Password" text NOT NULL,
    "PrimaryPhone" text NULL,
    "Role" text NOT NULL,
    "Salt" text NOT NULL,
    "SecondaryPhone" text NULL,
    "Username" text NOT NULL,
    CONSTRAINT "PK_Managers" PRIMARY KEY ("ID")
);

CREATE TABLE shared."Regions" (
    "ID" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    "Code" text NOT NULL,
    "Description" text NULL,
    "LastModified" timestamp without time zone NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_Regions" PRIMARY KEY ("ID")
);

CREATE TABLE shared."RegionManager" (
    "ManagerID" integer NOT NULL,
    "RegionID" integer NOT NULL,
    CONSTRAINT "PK_RegionManager" PRIMARY KEY ("ManagerID", "RegionID"),
    CONSTRAINT "FK_RegionManager_Managers_ManagerID" FOREIGN KEY ("ManagerID") REFERENCES shared."Managers" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_RegionManager_Regions_RegionID" FOREIGN KEY ("RegionID") REFERENCES shared."Regions" ("ID") ON DELETE CASCADE
);

CREATE INDEX "IX_Managers_Username" ON shared."Managers" ("Username");

CREATE INDEX "IX_RegionManager_RegionID" ON shared."RegionManager" ("RegionID");

CREATE INDEX "IX_Regions_Code" ON shared."Regions" ("Code");

INSERT INTO nss."Methods" ("ID", "Code", "Name")
VALUES (4, 'U', 'Undefined');

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200825141040_moveManagersToShared', '3.1.3');

