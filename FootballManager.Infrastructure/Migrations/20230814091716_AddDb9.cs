using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddDb9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId",
                table: "VirtualTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId1",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_DrawId",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_DrawId1",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "DrawId",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "DrawId1",
                table: "VirtualTeams");

            migrationBuilder.CreateTable(
                name: "DrawVirtualTeam",
                columns: table => new
                {
                    AllDrawsId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawVirtualTeam", x => new { x.AllDrawsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_DrawVirtualTeam_Draws_AllDrawsId",
                        column: x => x.AllDrawsId,
                        principalTable: "Draws",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawVirtualTeam_VirtualTeams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "VirtualTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawVirtualTeam_TeamsId",
                table: "DrawVirtualTeam",
                column: "TeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawVirtualTeam");

            migrationBuilder.AddColumn<int>(
                name: "DrawId",
                table: "VirtualTeams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrawId1",
                table: "VirtualTeams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_DrawId",
                table: "VirtualTeams",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_DrawId1",
                table: "VirtualTeams",
                column: "DrawId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId",
                table: "VirtualTeams",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId1",
                table: "VirtualTeams",
                column: "DrawId1",
                principalTable: "Draws",
                principalColumn: "Id");
        }
    }
}
