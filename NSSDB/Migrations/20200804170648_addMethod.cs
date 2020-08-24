using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    public partial class addMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MethodID",
                schema: "nss",
                table: "RegressionRegions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Methods",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegressionRegions_MethodID",
                schema: "nss",
                table: "RegressionRegions",
                column: "MethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionRegions_Methods_MethodID",
                schema: "nss",
                table: "RegressionRegions",
                column: "MethodID",
                principalSchema: "nss",
                principalTable: "Methods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.InsertData(
                schema: "nss",
                table: "Methods",
                columns: new[] { "ID", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "GLS", "Generalized least squares" },
                    { 2, "WLS", "Weighted least squares" },
                    { 3, "OLS", "Ordinary least squares" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegressionRegions_Methods_MethodID",
                schema: "nss",
                table: "RegressionRegions");

            migrationBuilder.DropTable(
                name: "Methods",
                schema: "nss");

            migrationBuilder.DropIndex(
                name: "IX_RegressionRegions_MethodID",
                schema: "nss",
                table: "RegressionRegions");

            migrationBuilder.DropColumn(
                name: "MethodID",
                schema: "nss",
                table: "RegressionRegions");
        }
    }
}
