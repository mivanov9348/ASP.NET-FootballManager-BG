using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddLeaguesToDraw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rounds",
                table: "Leagues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Leagues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Leagues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DrawId",
                table: "Leagues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_DrawId",
                table: "Leagues",
                column: "DrawId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Draws_DrawId",
                table: "Leagues",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Draws_DrawId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_DrawId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "DrawId",
                table: "Leagues");

            migrationBuilder.AlterColumn<int>(
                name: "Rounds",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
