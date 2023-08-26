using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddDrawOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfGroups",
                table: "Draws",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamsPergroup",
                table: "Draws",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfGroups",
                table: "Draws");

            migrationBuilder.DropColumn(
                name: "TeamsPergroup",
                table: "Draws");
        }
    }
}
