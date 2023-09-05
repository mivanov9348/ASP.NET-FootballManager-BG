using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class ChangeGameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Games",
                newName: "CurrentYearOrder");

            migrationBuilder.RenameColumn(
                name: "Season",
                table: "Games",
                newName: "CurrentMonthOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentYearOrder",
                table: "Games",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "CurrentMonthOrder",
                table: "Games",
                newName: "Season");
        }
    }
}
