using Microsoft.EntityFrameworkCore.Migrations;

namespace NSSDB.Migrations
{
    public partial class cascadeDeleteVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variables_Equations_EquationID",
                schema: "nss",
                table: "Variables");

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Roles",
            //    keyColumn: "ID",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Roles",
            //    keyColumn: "ID",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Status",
            //    keyColumn: "ID",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Status",
            //    keyColumn: "ID",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Status",
            //    keyColumn: "ID",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    schema: "nss",
            //    table: "Status",
            //    keyColumn: "ID",
            //    keyValue: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Variables_Equations_EquationID",
                schema: "nss",
                table: "Variables",
                column: "EquationID",
                principalSchema: "nss",
                principalTable: "Equations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variables_Equations_EquationID",
                schema: "nss",
                table: "Variables");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Variables_Equations_EquationID",
                schema: "nss",
                table: "Variables",
                column: "EquationID",
                principalSchema: "nss",
                principalTable: "Equations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
