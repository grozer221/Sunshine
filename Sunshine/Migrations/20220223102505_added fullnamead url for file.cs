using Microsoft.EntityFrameworkCore.Migrations;

namespace Sunshine.Migrations
{
    public partial class addedfullnameadurlforfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Files",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Files",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Files");
        }
    }
}
