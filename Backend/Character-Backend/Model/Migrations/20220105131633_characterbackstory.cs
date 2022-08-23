using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class characterbackstory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Backstory",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backstory",
                table: "Characters");
        }
    }
}
