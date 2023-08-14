using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrawId1",
                table: "VirtualTeams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTeams_DrawId1",
                table: "VirtualTeams",
                column: "DrawId1");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId1",
                table: "VirtualTeams",
                column: "DrawId1",
                principalTable: "Draws",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualTeams_Draws_DrawId1",
                table: "VirtualTeams");

            migrationBuilder.DropIndex(
                name: "IX_VirtualTeams_DrawId1",
                table: "VirtualTeams");

            migrationBuilder.DropColumn(
                name: "DrawId1",
                table: "VirtualTeams");
        }
    }
}
