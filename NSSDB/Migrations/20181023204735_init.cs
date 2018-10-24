using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nss");

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

            //migrationBuilder.CreateTable(
            //    name: "ErrorType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Code = table.Column<string>(nullable: false),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ErrorType_view", x => x.ID);
            //    });

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

            //migrationBuilder.CreateTable(
            //    name: "RegressionType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Code = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegressionType_view", x => x.ID);
            //    });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            //migrationBuilder.CreateTable(
            //    name: "StatisticGroupType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Code = table.Column<string>(nullable: false),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StatisticGroupType_view", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UnitSystemType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        UnitSystem = table.Column<string>(nullable: false),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitSystemType_view", x => x.ID);
            //    });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    User = table.Column<string>(nullable: false),
                    UnitSystemID = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.ID);
                });

            //migrationBuilder.CreateTable(
            //    name: "VariableType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Code = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: true),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VariableType_view", x => x.ID);
            //    });

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

            //migrationBuilder.CreateTable(
            //    name: "UnitType_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Abbreviation = table.Column<string>(nullable: false),
            //        UnitSystemTypeID = table.Column<int>(nullable: false),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitType_view", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_UnitType_view_UnitSystemType_view_UnitSystemTypeID",
            //            column: x => x.UnitSystemTypeID,
            //            principalSchema: "nss",
            //            principalTable: "UnitSystemType_view",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

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
                name: "RegressionRegionCoefficients",
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
                    table.PrimaryKey("PK_RegressionRegionCoefficients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegressionRegionCoefficients_RegressionRegions_RegressionRe~",
                        column: x => x.RegressionRegionID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    //table.ForeignKey(
                    //    name: "FK_Equations_RegressionType_view_RegressionTypeID",
                    //    column: x => x.RegressionTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "RegressionType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_Equations_StatisticGroupType_view_StatisticGroupTypeID",
                    //    column: x => x.StatisticGroupTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "StatisticGroupType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_Equations_UnitType_view_UnitTypeID",
                    //    column: x => x.UnitTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "UnitType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateTable(
            //    name: "UnitConversionFactor_view",
            //    schema: "nss",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        UnitTypeInID = table.Column<int>(nullable: false),
            //        UnitTypeOutID = table.Column<int>(nullable: false),
            //        Factor = table.Column<double>(nullable: false),
            //        LastModified = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitConversionFactor_view", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_UnitConversionFactor_view_UnitType_view_UnitTypeInID",
            //            column: x => x.UnitTypeInID,
            //            principalSchema: "nss",
            //            principalTable: "UnitType_view",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UnitConversionFactor_view_UnitType_view_UnitTypeOutID",
            //            column: x => x.UnitTypeOutID,
            //            principalSchema: "nss",
            //            principalTable: "UnitType_view",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

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
                    //table.ForeignKey(
                    //    name: "FK_EquationErrors_ErrorType_view_ErrorTypeID",
                    //    column: x => x.ErrorTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "ErrorType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Restrict);
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
                    //table.ForeignKey(
                    //    name: "FK_EquationUnitTypes_UnitType_view_UnitTypeID",
                    //    column: x => x.UnitTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "UnitType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Cascade);
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
                    RegressionRegionCoefficientID = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.ID);
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
                    table.ForeignKey(
                        name: "FK_Variables_RegressionRegionCoefficients_RegressionRegionCoef~",
                        column: x => x.RegressionRegionCoefficientID,
                        principalSchema: "nss",
                        principalTable: "RegressionRegionCoefficients",
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
                    //table.PrimaryKey("PK_VariableUnitTypes", x => new { x.VariableID, x.UnitTypeID });
                    //table.ForeignKey(
                    //    name: "FK_VariableUnitTypes_UnitType_view_UnitTypeID",
                    //    column: x => x.UnitTypeID,
                    //    principalSchema: "nss",
                    //    principalTable: "UnitType_view",
                    //    principalColumn: "ID",
                    //    onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariableUnitTypes_Variables_VariableID",
                        column: x => x.VariableID,
                        principalSchema: "nss",
                        principalTable: "Variables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquationErrors_EquationID",
                schema: "nss",
                table: "EquationErrors",
                column: "EquationID");

            migrationBuilder.CreateIndex(
                name: "IX_EquationErrors_ErrorTypeID",
                schema: "nss",
                table: "EquationErrors",
                column: "ErrorTypeID");

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
                name: "IX_Equations_RegressionTypeID",
                schema: "nss",
                table: "Equations",
                column: "RegressionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Equations_StatisticGroupTypeID",
                schema: "nss",
                table: "Equations",
                column: "StatisticGroupTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Equations_UnitTypeID",
                schema: "nss",
                table: "Equations",
                column: "UnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EquationUnitTypes_UnitTypeID",
                schema: "nss",
                table: "EquationUnitTypes",
                column: "UnitTypeID");

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
                name: "IX_RegressionRegionCoefficients_RegressionRegionID",
                schema: "nss",
                table: "RegressionRegionCoefficients",
                column: "RegressionRegionID");

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

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitConversionFactor_view_UnitTypeInID",
            //    schema: "nss",
            //    table: "UnitConversionFactor_view",
            //    column: "UnitTypeInID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitConversionFactor_view_UnitTypeOutID",
            //    schema: "nss",
            //    table: "UnitConversionFactor_view",
            //    column: "UnitTypeOutID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitType_view_UnitSystemTypeID",
            //    schema: "nss",
            //    table: "UnitType_view",
            //    column: "UnitSystemTypeID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Variables_RegressionRegionCoefficientID",
                schema: "nss",
                table: "Variables",
                column: "RegressionRegionCoefficientID");

            migrationBuilder.CreateIndex(
                name: "IX_VariableUnitTypes_UnitTypeID",
                schema: "nss",
                table: "VariableUnitTypes",
                column: "UnitTypeID");

            //custom sql
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION ""trigger_set_lastmodified""()
                    RETURNS TRIGGER AS $$
                    BEGIN
                      NEW.""LastModified"" = NOW();
                      RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Citations"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Equations"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Limitations"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Managers"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""PredictionIntervals"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Regions"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""RegressionRegions"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""RegressionRegionCoefficients"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Roles"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();                
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""UserTypes"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
                CREATE TRIGGER lastupdate BEFORE INSERT OR UPDATE ON ""Variables"" FOR EACH ROW EXECUTE PROCEDURE ""nss"".""trigger_set_lastmodified""();
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

            //migrationBuilder.DropTable(
            //    name: "UnitConversionFactor_view",
            //    schema: "nss");

            migrationBuilder.DropTable(
                name: "UserTypes",
                schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "VariableType_view",
            //    schema: "nss");

            migrationBuilder.DropTable(
                name: "VariableUnitTypes",
                schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "ErrorType_view",
            //    schema: "nss");

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
                name: "Equations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "Limitations",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "RegressionRegionCoefficients",
                schema: "nss");

            migrationBuilder.DropTable(
                name: "PredictionIntervals",
                schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "RegressionType_view",
            //    schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "StatisticGroupType_view",
            //    schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "UnitType_view",
            //    schema: "nss");

            migrationBuilder.DropTable(
                name: "RegressionRegions",
                schema: "nss");

            //migrationBuilder.DropTable(
            //    name: "UnitSystemType_view",
            //    schema: "nss");

            migrationBuilder.DropTable(
                name: "Citations",
                schema: "nss");
        }
    }
}
