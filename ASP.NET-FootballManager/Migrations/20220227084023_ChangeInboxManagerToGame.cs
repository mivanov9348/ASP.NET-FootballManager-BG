using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class ChangeInboxManagerToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inboxes_Managers_ManagerId",
                table: "Inboxes");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Inboxes",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Inboxes_ManagerId",
                table: "Inboxes",
                newName: "IX_Inboxes_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inboxes_Games_GameId",
                table: "Inboxes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inboxes_Games_GameId",
                table: "Inboxes");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Inboxes",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Inboxes_GameId",
                table: "Inboxes",
                newName: "IX_Inboxes_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inboxes_Managers_ManagerId",
                table: "Inboxes",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
