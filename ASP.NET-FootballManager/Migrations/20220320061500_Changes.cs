using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Inboxes",
                newName: "MessageReview");

            migrationBuilder.AddColumn<string>(
                name: "FullMessage",
                table: "Inboxes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullMessage",
                table: "Inboxes");

            migrationBuilder.RenameColumn(
                name: "MessageReview",
                table: "Inboxes",
                newName: "Message");
        }
    }
}
