using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace NSSDB.Migrations
{
    public partial class add_spatial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", "'postgis', '', ''");

            migrationBuilder.AddColumn<Polygon>(
                name: "Location",
                schema: "nss",
                table: "RegressionRegions",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "nss",
                table: "RegressionRegions");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", "'postgis', '', ''");
        }
    }
}
