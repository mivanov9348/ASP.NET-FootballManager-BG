using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isMatchDay",
                table: "Days",
                newName: "IsMatchDay");

            migrationBuilder.RenameColumn(
                name: "isLeagueDay",
                table: "Days",
                newName: "IsLeagueDay");

            migrationBuilder.RenameColumn(
                name: "isEuroCupDay",
                table: "Days",
                newName: "IsEuroCupDay");

            migrationBuilder.RenameColumn(
                name: "isCupDay",
                table: "Days",
                newName: "IsCupDay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMatchDay",
                table: "Days",
                newName: "isMatchDay");

            migrationBuilder.RenameColumn(
                name: "IsLeagueDay",
                table: "Days",
                newName: "isLeagueDay");

            migrationBuilder.RenameColumn(
                name: "IsEuroCupDay",
                table: "Days",
                newName: "isEuroCupDay");

            migrationBuilder.RenameColumn(
                name: "IsCupDay",
                table: "Days",
                newName: "isCupDay");
        }
    }
}
