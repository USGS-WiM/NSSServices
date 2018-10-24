using Microsoft.EntityFrameworkCore.Migrations;

namespace NSSDB.Migrations
{
    public partial class fixVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitSystemTypeID",
                schema: "nss",
                table: "UserTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variables_RegressionTypeID",
                schema: "nss",
                table: "Variables",
                column: "RegressionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_UnitTypeID",
                schema: "nss",
                table: "Variables",
                column: "UnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_VariableTypeID",
                schema: "nss",
                table: "Variables",
                column: "VariableTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_UnitSystemTypeID",
                schema: "nss",
                table: "UserTypes",
                column: "UnitSystemTypeID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserTypes_UnitSystemType_view_UnitSystemTypeID",
            //    schema: "nss",
            //    table: "UserTypes",
            //    column: "UnitSystemTypeID",
            //    principalSchema: "nss",
            //    principalTable: "UnitSystemType_view",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Variables_RegressionType_view_RegressionTypeID",
            //    schema: "nss",
            //    table: "Variables",
            //    column: "RegressionTypeID",
            //    principalSchema: "nss",
            //    principalTable: "RegressionType_view",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Variables_UnitType_view_UnitTypeID",
            //    schema: "nss",
            //    table: "Variables",
            //    column: "UnitTypeID",
            //    principalSchema: "nss",
            //    principalTable: "UnitType_view",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Variables_VariableType_view_VariableTypeID",
            //    schema: "nss",
            //    table: "Variables",
            //    column: "VariableTypeID",
            //    principalSchema: "nss",
            //    principalTable: "VariableType_view",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserTypes_UnitSystemType_view_UnitSystemTypeID",
            //    schema: "nss",
            //    table: "UserTypes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Variables_RegressionType_view_RegressionTypeID",
            //    schema: "nss",
            //    table: "Variables");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Variables_UnitType_view_UnitTypeID",
            //    schema: "nss",
            //    table: "Variables");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Variables_VariableType_view_VariableTypeID",
            //    schema: "nss",
            //    table: "Variables");

            migrationBuilder.DropIndex(
                name: "IX_Variables_RegressionTypeID",
                schema: "nss",
                table: "Variables");

            migrationBuilder.DropIndex(
                name: "IX_Variables_UnitTypeID",
                schema: "nss",
                table: "Variables");

            migrationBuilder.DropIndex(
                name: "IX_Variables_VariableTypeID",
                schema: "nss",
                table: "Variables");

            migrationBuilder.DropIndex(
                name: "IX_UserTypes_UnitSystemTypeID",
                schema: "nss",
                table: "UserTypes");

            migrationBuilder.DropColumn(
                name: "UnitSystemTypeID",
                schema: "nss",
                table: "UserTypes");
        }
    }
}
