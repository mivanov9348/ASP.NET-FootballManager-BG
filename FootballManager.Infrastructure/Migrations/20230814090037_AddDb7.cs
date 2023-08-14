using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddDb7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Draws_DrawId",
                table: "Fixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Draws_DrawId",
                table: "Fixtures",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Draws_DrawId",
                table: "Fixtures");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Draws_DrawId",
                table: "Fixtures",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id");
        }
    }
}
