using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedDB.Migrations
{
    public partial class addUnitTypeStatGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegressionType_EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "EnglishUnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionType_MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "MetricUnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RegressionType_StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "StatisticGroupTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionType_UnitType_EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "EnglishUnitTypeID",
                principalSchema: "shared",
                principalTable: "UnitType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionType_UnitType_MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "MetricUnitTypeID",
                principalSchema: "shared",
                principalTable: "UnitType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegressionType_StatisticGroupType_StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType",
                column: "StatisticGroupTypeID",
                principalSchema: "shared",
                principalTable: "StatisticGroupType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegressionType_UnitType_EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropForeignKey(
                name: "FK_RegressionType_UnitType_MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropForeignKey(
                name: "FK_RegressionType_StatisticGroupType_StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropIndex(
                name: "IX_RegressionType_EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropIndex(
                name: "IX_RegressionType_MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropIndex(
                name: "IX_RegressionType_StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropColumn(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropColumn(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.DropColumn(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "RegressionType");

            migrationBuilder.AlterColumn<int>(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "VariableType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
