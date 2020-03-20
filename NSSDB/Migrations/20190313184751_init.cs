using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nss");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Citations",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    CitationURL = table.Column<string>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Geometry = table.Column<Geometry>(nullable: false),
                    AssociatedCodes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PredictionIntervals",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BiasCorrectionFactor = table.Column<double>(nullable: true),
                    Student_T_Statistic = table.Column<double>(nullable: true),
                    Variance = table.Column<double>(nullable: true),
                    XIRowVector = table.Column<string>(nullable: true),
                    CovarianceMatrix = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionIntervals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PrimaryPhone = table.Column<string>(nullable: true),
                    SecondaryPhone = table.Column<string>(nullable: true),
                    RoleID = table.Column<int>(nullable: false),
                    OtherInfo = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Managers_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "nss",
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegressionRegions",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CitationID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: true),
                    LocationID = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegressionRegions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegressionRegions_Citations_CitationID",
                        column: x => x.CitationID,
                        principalSchema: "nss",
                        principalTable: "Citations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegressionRegions_Locations_LocationID",
                        column: x => x.LocationID,
                        principalSchema: "nss",
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegressionRegions_Status_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "nss",
                        principalTable: "Status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionManager",
                schema: "nss",
                columns: table => new
                {
                    RegionID = table.Column<int>(nullable: false),
                    ManagerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionManager", x => new { x.ManagerID, x.RegionID });
                    table.ForeignKey(
                        name: "FK_RegionManager_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalSchema: "nss",
                        principalTable: "Managers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionManager_Regions_RegionID",
                        column: x => x.RegionID,
                        principalSchema: "nss",
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coefficients",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RegressionRegionID = table.Column<int>(nullable: false),
                    Criteria = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coefficients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Coefficients_RegressionRegions_RegressionRegionID",
                        column: x => x.RegressionRegionID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equations",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RegressionRegionID = table.Column<int>(nullable: false),
                    PredictionIntervalID = table.Column<int>(nullable: true),
                    UnitTypeID = table.Column<int>(nullable: false),
                    Expression = table.Column<string>(nullable: false),
                    DA_Exponent = table.Column<double>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: true),
                    RegressionTypeID = table.Column<int>(nullable: false),
                    StatisticGroupTypeID = table.Column<int>(nullable: false),
                    EquivalentYears = table.Column<double>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Equations_PredictionIntervals_PredictionIntervalID",
                        column: x => x.PredictionIntervalID,
                        principalSchema: "nss",
                        principalTable: "PredictionIntervals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equations_RegressionRegions_RegressionRegionID",
                        column: x => x.RegressionRegionID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Limitations",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Criteria = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RegressionRegionID = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limitations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Limitations_RegressionRegions_RegressionRegionID",
                        column: x => x.RegressionRegionID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionRegressionRegions",
                schema: "nss",
                columns: table => new
                {
                    RegionID = table.Column<int>(nullable: false),
                    RegressionRegionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionRegressionRegions", x => new { x.RegionID, x.RegressionRegionID });
                    table.ForeignKey(
                        name: "FK_RegionRegressionRegions_Regions_RegionID",
                        column: x => x.RegionID,
                        principalSchema: "nss",
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionRegressionRegions_RegressionRegions_RegressionRegionID",
                        column: x => x.RegressionRegionID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquationErrors",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EquationID = table.Column<int>(nullable: false),
                    ErrorTypeID = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquationErrors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EquationErrors_Equations_EquationID",
                        column: x => x.EquationID,
                        principalSchema: "nss",
                        principalTable: "Equations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquationUnitTypes",
                schema: "nss",
                columns: table => new
                {
                    EquationID = table.Column<int>(nullable: false),
                    UnitTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquationUnitTypes", x => new { x.EquationID, x.UnitTypeID });
                    table.ForeignKey(
                        name: "FK_EquationUnitTypes_Equations_EquationID",
                        column: x => x.EquationID,
                        principalSchema: "nss",
                        principalTable: "Equations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EquationID = table.Column<int>(nullable: true),
                    VariableTypeID = table.Column<int>(nullable: false),
                    RegressionTypeID = table.Column<int>(nullable: true),
                    UnitTypeID = table.Column<int>(nullable: false),
                    MinValue = table.Column<double>(nullable: true),
                    MaxValue = table.Column<double>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    LimitationID = table.Column<int>(nullable: true),
                    CoefficientID = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Variables_Coefficients_CoefficientID",
                        column: x => x.CoefficientID,
                        principalSchema: "nss",
                        principalTable: "Coefficients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variables_Equations_EquationID",
                        column: x => x.EquationID,
                        principalSchema: "nss",
                        principalTable: "Equations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variables_Limitations_LimitationID",
                        column: x => x.LimitationID,
                        principalSchema: "nss",
                        principalTable: "Limitations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VariableUnitTypes",
                schema: "nss",
                columns: table => new
                {
                    VariableID = table.Column<int>(nullable: false),
                    UnitTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableUnitTypes", x => new { x.VariableID, x.UnitTypeID });
                    table.ForeignKey(
                        name: "FK_VariableUnitTypes_Variables_VariableID",
                        column: x => x.VariableID,
                        principalSchema: "nss",
                        principalTable: "Variables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "nss",
                table: "Roles",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "System Administrator", "Admin" },
                    { 2, "Region Manager", "Manager" }
                });

            migrationBuilder.InsertData(
                schema: "nss",
                table: "Status",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Working and disabled for all public users", "Work/Disabled" },
                    { 2, "Reviewing and disabled for all public users", "Review" },
                    { 3, "Approved and enabled for public NSS users", "Approved" },
                    { 4, "Approved and enabled for public StreamStats users", "SS Approved" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coefficients_RegressionRegionID",
                schema: "nss",
                table: "Coefficients",
                column: "RegressionRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_EquationErrors_EquationID",
                schema: "nss",
                table: "EquationErrors",
                column: "EquationID");

            migrationBuilder.CreateIndex(
                name: "IX_Equations_PredictionIntervalID",
                schema: "nss",
                table: "Equations",
                column: "PredictionIntervalID");

            migrationBuilder.CreateIndex(
                name: "IX_Equations_RegressionRegionID",
                schema: "nss",
                table: "Equations",
                column: "RegressionRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Limitations_RegressionRegionID",
                schema: "nss",
                table: "Limitations",
                column: "RegressionRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_RoleID",
                schema: "nss",
                table: "Managers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Username",
                schema: "nss",
                table: "Managers",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_RegionManager_RegionID",
                schema: "nss",
                table: "RegionManager",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionRegressionRegions_RegressionRegionID",
                schema: "nss",
                table: "RegionRegressionRegions",
                column: "RegressionRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                schema: "nss",
                table: "Regions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionRegions_CitationID",
                schema: "nss",
                table: "RegressionRegions",
                column: "CitationID");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionRegions_Code",
                schema: "nss",
                table: "RegressionRegions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionRegions_LocationID",
                schema: "nss",
                table: "RegressionRegions",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionRegions_StatusID",
                schema: "nss",
                table: "RegressionRegions",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_CoefficientID",
                schema: "nss",
                table: "Variables",
                column: "CoefficientID");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_EquationID",
                schema: "nss",
                table: "Variables",
                column: "EquationID");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_LimitationID",
                schema: "nss",
                table: "Variables",
                column: "LimitationID");

            //custom sql
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION ""nss"".""trigger_set_lastmodified""()
                    RETURNS TRIGGER AS $$
                    BEGIN
                      NEW.""LastModified"" = NOW();
                      RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                ");
            migrationBuilder.Sql(@"
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Citations""  FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Equations""  FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  ""Limitations"" FOR EACH ROW EXECUTE PROCEDURE  ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Managers""  FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  ""PredictionIntervals"" FOR EACH ROW EXECUTE PROCEDURE  ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Regions""  FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  ""RegressionRegions"" FOR EACH ROW EXECUTE PROCEDURE  ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON  ""Coefficients"" FOR EACH ROW EXECUTE PROCEDURE  ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Variables""  FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquationErrors",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "EquationUnitTypes",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "RegionManager",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "RegionRegressionRegions",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "VariableUnitTypes",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Variables",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Coefficients",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Equations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Limitations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "PredictionIntervals",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "RegressionRegions",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Citations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "nss");
        }
    }
}
