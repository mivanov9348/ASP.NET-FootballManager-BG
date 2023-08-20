using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballManager.Infrastructure.Migrations
{
    public partial class AddImageToManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Managers");
        }
    }
}
