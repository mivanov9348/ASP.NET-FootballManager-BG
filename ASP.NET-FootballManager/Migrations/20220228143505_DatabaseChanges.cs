using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class DatabaseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Managers_ManagerId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Managers_ManagerId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_Players_ManagerId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Players",
                newName: "Speed");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "VirtualTeams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VirtualTeams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Overall",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_GameId",
                table: "VirtualTeams",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_LeagueId",
                table: "VirtualTeams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_VirtualTeams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "VirtualTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Games_GameId",
                table: "VirtualTeams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Leagues_LeagueId",
                table: "VirtualTeams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Managers_ManagerId",
                table: "VirtualTeams",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_VirtualTeams_TeamId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Games_GameId",
                table: "VirtualTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Leagues_LeagueId",
                table: "VirtualTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Managers_ManagerId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_GameId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_LeagueId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Overall",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "Players",
                newName: "ManagerId");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "VirtualTeams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ManagerId",
                table: "Players",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Managers_ManagerId",
                table: "Players",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
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
    }
}
