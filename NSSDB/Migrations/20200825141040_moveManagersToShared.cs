using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    public partial class moveManagersToShared : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<double>(
                name: "DegreesOfFreedom",
                schema: "nss",
                table: "PredictionIntervals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "shared",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    OtherInfo = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PrimaryPhone = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    SecondaryPhone = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "shared",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RegionManager",
                schema: "shared",
                columns: table => new
                {
                    ManagerID = table.Column<int>(type: "integer", nullable: false),
                    RegionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionManager", x => new { x.ManagerID, x.RegionID });
                    table.ForeignKey(
                        name: "FK_RegionManager_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalSchema: "shared",
                        principalTable: "Managers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionManager_Regions_RegionID",
                        column: x => x.RegionID,
                        principalSchema: "shared",
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Username",
                schema: "shared",
                table: "Managers",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_RegionManager_RegionID",
                schema: "shared",
                table: "RegionManager",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Code",
                schema: "shared",
                table: "Regions",
                column: "Code");

            migrationBuilder.DropForeignKey(
                name: "FK_RegionRegressionRegions_Regions_RegionID",
                schema: "nss",
                table: "RegionRegressionRegions");

            migrationBuilder.AddForeignKey(
                name: "FK_RegionRegressionRegions_Regions_RegionID",
                schema: "nss",
                table: "RegionRegressionRegions",
                column: "RegionID",
                principalTable: "Regions",
                principalSchema: "shared",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DegreesOfFreedom",
                schema: "nss",
                table: "PredictionIntervals");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "RegionManager",
                schema: "shared");

            migrationBuilder.DropForeignKey(
               name: "FK_RegionRegressionRegions_Regions_RegionID",
               schema: "nss",
               table: "RegionRegressionRegions");

            migrationBuilder.AddForeignKey(
                name: "FK_RegionRegressionRegions_Regions_RegionID",
                schema: "nss",
                table: "RegionRegressionRegions",
                column: "RegionID",
                principalTable: "Regions",
                principalSchema: "nss",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
