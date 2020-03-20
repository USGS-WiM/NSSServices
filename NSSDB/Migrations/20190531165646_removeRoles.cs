using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NSSDB.Migrations
{
    public partial class removeRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Roles_RoleID",
                schema: "nss",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "nss");

            migrationBuilder.DropIndex(
                name: "IX_Managers_RoleID",
                schema: "nss",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RoleID",
                schema: "nss",
                table: "Managers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "nss",
                table: "Managers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "nss",
                table: "Managers");

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                schema: "nss",
                table: "Managers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "nss",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_RoleID",
                schema: "nss",
                table: "Managers",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Roles_RoleID",
                schema: "nss",
                table: "Managers",
                column: "RoleID",
                principalSchema: "nss",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
