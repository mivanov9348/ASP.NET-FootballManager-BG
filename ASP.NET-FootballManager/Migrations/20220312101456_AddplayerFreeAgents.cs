using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddplayerFreeAgents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FreeAgent",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeAgent",
                table: "Players");
        }
    }
}
