using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SharedDB.Migrations
{
    public partial class addVariableUnitTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "VariableType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitSystemType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitConversionFactor",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "StatisticGroupType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "DefType",
                schema: "shared",
                table: "StatisticGroupType",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "RegressionType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "ErrorType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            //migrationBuilder.CreateTable(
            //    name: "Managers",
            //    schema: "shared",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        FirstName = table.Column<string>(nullable: false),
            //        LastName = table.Column<string>(nullable: false),
            //        Username = table.Column<string>(nullable: false),
            //        Email = table.Column<string>(nullable: false),
            //        PrimaryPhone = table.Column<string>(nullable: true),
            //        SecondaryPhone = table.Column<string>(nullable: true),
            //        Role = table.Column<string>(nullable: false),
            //        OtherInfo = table.Column<string>(nullable: true),
            //        Password = table.Column<string>(nullable: false),
            //        Salt = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Managers", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Regions",
            //    schema: "shared",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(nullable: false),
            //        Code = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Regions", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegionManager",
            //    schema: "shared",
            //    columns: table => new
            //    {
            //        RegionID = table.Column<int>(nullable: false),
            //        ManagerID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RegionManager", x => new { x.ManagerID, x.RegionID });
            //        table.ForeignKey(
            //            name: "FK_RegionManager_Managers_ManagerID",
            //            column: x => x.ManagerID,
            //            principalSchema: "shared",
            //            principalTable: "Managers",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RegionManager_Regions_RegionID",
            //            column: x => x.RegionID,
            //            principalSchema: "shared",
            //            principalTable: "Regions",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_VariableType_EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType",
                column: "EnglishUnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VariableType_MetricUnitTypeID",
                schema: "shared",
                table: "VariableType",
                column: "MetricUnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VariableType_StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType",
                column: "StatisticGroupTypeID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Managers_Username",
            //    schema: "shared",
            //    table: "Managers",
            //    column: "Username",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegionManager_RegionID",
            //    schema: "shared",
            //    table: "RegionManager",
            //    column: "RegionID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Regions_Code",
            //    schema: "shared",
            //    table: "Regions",
            //    column: "Code",
            //    unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VariableType_UnitType_EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType",
                column: "EnglishUnitTypeID",
                principalSchema: "shared",
                principalTable: "UnitType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariableType_UnitType_MetricUnitTypeID",
                schema: "shared",
                table: "VariableType",
                column: "MetricUnitTypeID",
                principalSchema: "shared",
                principalTable: "UnitType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VariableType_StatisticGroupType_StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType",
                column: "StatisticGroupTypeID",
                principalSchema: "shared",
                principalTable: "StatisticGroupType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariableType_UnitType_EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropForeignKey(
                name: "FK_VariableType_UnitType_MetricUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropForeignKey(
                name: "FK_VariableType_StatisticGroupType_StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropTable(
                name: "RegionManager",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "shared");

            migrationBuilder.DropIndex(
                name: "IX_VariableType_EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropIndex(
                name: "IX_VariableType_MetricUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropIndex(
                name: "IX_VariableType_StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropColumn(
                name: "EnglishUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropColumn(
                name: "MetricUnitTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropColumn(
                name: "StatisticGroupTypeID",
                schema: "shared",
                table: "VariableType");

            migrationBuilder.DropColumn(
                name: "DefType",
                schema: "shared",
                table: "StatisticGroupType");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "VariableType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitSystemType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "UnitConversionFactor",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "StatisticGroupType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "RegressionType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                schema: "shared",
                table: "ErrorType",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
