using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class DayOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentDay",
                table: "Days",
                newName: "WeekDayOrder");

            migrationBuilder.AddColumn<int>(
                name: "DayOrder",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOrder",
                table: "Days");

            migrationBuilder.RenameColumn(
                name: "WeekDayOrder",
                table: "Days",
                newName: "CurrentDay");
        }
    }
}
