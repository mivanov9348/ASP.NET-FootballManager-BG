using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class EuroCupAndCup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CupId",
                table: "VirtualTeams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EuropeanCupId",
                table: "VirtualTeams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_CupId",
                table: "VirtualTeams",
                column: "CupId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_EuropeanCupId",
                table: "VirtualTeams",
                column: "EuropeanCupId");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Cups_CupId",
                table: "VirtualTeams",
                column: "CupId",
                principalTable: "Cups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_EuropeanCups_EuropeanCupId",
                table: "VirtualTeams",
                column: "EuropeanCupId",
                principalTable: "EuropeanCups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Cups_CupId",
                table: "VirtualTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_EuropeanCups_EuropeanCupId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_CupId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_EuropeanCupId",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "CupId",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "EuropeanCupId",
                table: "VirtualTeams");
        }
    }
}
