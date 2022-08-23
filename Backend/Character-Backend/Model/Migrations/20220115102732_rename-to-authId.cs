using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class renametoauthId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OcName",
                table: "Users",
                newName: "AuthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthId",
                table: "Users",
                newName: "OcName");
        }
    }
}
