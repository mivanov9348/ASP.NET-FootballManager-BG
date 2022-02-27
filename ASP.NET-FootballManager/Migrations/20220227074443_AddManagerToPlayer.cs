using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddManagerToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Managers_TeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Managers_TeamId",
                table: "VirtualTeams");

            migrationBuilder.RenameColumn(
                name: "GamePlayerID",
                table: "VirtualTeams",
                newName: "ManagerId");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_ManagerId",
                table: "VirtualTeams",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ManagerId",
                table: "Players",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ManagerId",
                table: "Games",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Managers_ManagerId",
                table: "Games",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Managers_ManagerId",
                table: "Players",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Managers_ManagerId",
                table: "VirtualTeams",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Managers_ManagerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Managers_ManagerId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Managers_ManagerId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_ManagerId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_Players_ManagerId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Games_ManagerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "VirtualTeams",
                newName: "GamePlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Managers_TeamId",
                table: "Games",
                column: "TeamId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Managers_TeamId",
                table: "VirtualTeams",
                column: "TeamId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
