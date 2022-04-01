using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddBoolEuroAndCupParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCupParticipant",
                table: "VirtualTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEuroParticipant",
                table: "VirtualTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCupParticipant",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEuroParticipant",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCupParticipant",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "IsEuroParticipant",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "IsCupParticipant",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsEuroParticipant",
                table: "Teams");
        }
    }
}
