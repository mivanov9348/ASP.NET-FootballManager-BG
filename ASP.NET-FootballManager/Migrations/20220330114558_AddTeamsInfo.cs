    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddTeamsInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChampionsCup",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cups",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChampionsCup",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "Cups",
                table: "VirtualTeams");
        }
    }
}
