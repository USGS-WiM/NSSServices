using Microsoft.EntityFrameworkCore.Migrations;

namespace NSSDB.Migrations
{
    public partial class dbgenerated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegressionRegions_Citations_CitationID",
                schema: "nss",
                table: "RegressionRegions");

            migrationBuilder.AlterColumn<int>(
                name: "CitationID",
                schema: "nss",
                table: "RegressionRegions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionRegions_Citations_CitationID",
                schema: "nss",
                table: "RegressionRegions",
                column: "CitationID",
                principalSchema: "nss",
                principalTable: "Citations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegressionRegions_Citations_CitationID",
                schema: "nss",
                table: "RegressionRegions");

            migrationBuilder.AlterColumn<int>(
                name: "CitationID",
                schema: "nss",
                table: "RegressionRegions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionRegions_Citations_CitationID",
                schema: "nss",
                table: "RegressionRegions",
                column: "CitationID",
                principalSchema: "nss",
                principalTable: "Citations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
