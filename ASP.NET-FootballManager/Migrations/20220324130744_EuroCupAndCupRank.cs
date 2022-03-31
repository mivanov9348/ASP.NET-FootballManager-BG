using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class EuroCupAndCupRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "EuropeanCups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "EuropeanCups");
        }
    }
}
