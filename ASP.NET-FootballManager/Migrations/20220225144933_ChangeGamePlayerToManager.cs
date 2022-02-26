using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class ChangeGamePlayerToManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_GamePlayers_TeamId",
                table: "VirtualTeams");

            migrationBuilder.DropTable(
                name: "GamePlayers");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationId = table.Column<int>(type: "int", nullable: false),
                    CurrentTeamId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Managers_Nations_NationId",
                        column: x => x.NationId,
                        principalTable: "Nations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Managers_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CurrentTeamId",
                table: "Managers",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_NationId",
                table: "Managers",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId1",
                table: "Managers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Managers_TeamId",
                table: "VirtualTeams",
                column: "TeamId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Managers_TeamId",
                table: "VirtualTeams");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.CreateTable(
                name: "GamePlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentTeamId = table.Column<int>(type: "int", nullable: true),
                    NationId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlayers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayers_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GamePlayers_Nations_NationId",
                        column: x => x.NationId,
                        principalTable: "Nations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamePlayers_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_CurrentTeamId",
                table: "GamePlayers",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_NationId",
                table: "GamePlayers",
                column: "NationId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_UserId",
                table: "GamePlayers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayers_UserId1",
                table: "GamePlayers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_GamePlayers_TeamId",
                table: "VirtualTeams",
                column: "TeamId",
                principalTable: "GamePlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
