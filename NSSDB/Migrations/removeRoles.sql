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
VALUES ('20190313184751_init', '2.2.4-servicing-10062');

ALTER TABLE nss."RegressionRegions" DROP CONSTRAINT "FK_RegressionRegions_Citations_CitationID";

ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" TYPE integer;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" DROP NOT NULL;
ALTER TABLE nss."RegressionRegions" ALTER COLUMN "CitationID" DROP DEFAULT;

ALTER TABLE nss."RegressionRegions" ADD CONSTRAINT "FK_RegressionRegions_Citations_CitationID" FOREIGN KEY ("CitationID") REFERENCES nss."Citations" ("ID") ON DELETE RESTRICT;

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190319150055_dbgenerated', '2.2.4-servicing-10062');

ALTER TABLE nss."Variables" DROP CONSTRAINT "FK_Variables_Equations_EquationID";

ALTER TABLE nss."Variables" ADD CONSTRAINT "FK_Variables_Equations_EquationID" FOREIGN KEY ("EquationID") REFERENCES nss."Equations" ("ID") ON DELETE CASCADE;

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190517134523_cascadeDeleteVariables', '2.2.4-servicing-10062');

ALTER TABLE nss."Managers" DROP CONSTRAINT "FK_Managers_Roles_RoleID";

DROP TABLE nss."Roles";

DROP INDEX nss."IX_Managers_RoleID";

ALTER TABLE nss."Managers" DROP COLUMN "RoleID";

ALTER TABLE nss."Managers" ADD "Role" text NOT NULL DEFAULT '';

INSERT INTO nss."_EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190531165646_removeRoles', '2.2.4-servicing-10062');

