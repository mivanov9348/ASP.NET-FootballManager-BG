using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddCleanSheats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saves",
                table: "Players",
                newName: "CleanSheets");

            migrationBuilder.AddColumn<int>(
                name: "Overall",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isEnd",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overall",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "isEnd",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "CleanSheets",
                table: "Players",
                newName: "Saves");
        }
    }
}
