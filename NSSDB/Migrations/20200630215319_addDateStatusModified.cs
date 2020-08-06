using Microsoft.EntityFrameworkCore.Migrations;

namespace NSSDB.Migrations
{
    public partial class addDateStatusModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateStatusModified",
                schema: "nss",
                table: "RegressionRegions",
                nullable: false,
                defaultValueSql: "NOW()");

            //custom sql
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION ""nss"".""trigger_set_datestatusmodified""()
                    RETURNS TRIGGER AS $$
                    BEGIN
                    if TG_OP = 'INSERT' OR OLD.""StatusID"" IS DISTINCT FROM NEW.""StatusID"" then
                      NEW.""DateStatusModified"" = NOW();
                    end if;
                    RETURN NEW;
                    END;
                    $$ LANGUAGE plpgsql;
                ");
            migrationBuilder.Sql(@"
                CREATE TRIGGER laststatusupdate BEFORE INSERT OR UPDATE ON  ""RegressionRegions"" FOR EACH ROW EXECUTE PROCEDURE  ""nss"".""trigger_set_datestatusmodified""();
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "DateStatusModified",
               schema: "nss",
               table: "RegressionRegions");

            migrationBuilder.Sql(@"
                DROP TRIGGER IF EXISTS laststatusupdate ON ""RegressionRegions"";
                ");
        }
    }
}
