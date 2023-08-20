using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class RemoveNationfrommanager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Nations_NationId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_NationId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "NationId",
                table: "Managers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NationId",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_NationId",
                table: "Managers",
                column: "NationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Nations_NationId",
                table: "Managers",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
