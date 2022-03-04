using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_FootballManager.Migrations
{
    public partial class AddFixturesGenerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayTeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: true),
                    HomeTeamGoal = table.Column<int>(type: "int", nullable: false),
                    AwayTeamGoal = table.Column<int>(type: "int", nullable: false),
                    IsPlayed = table.Column<bool>(type: "bit", nullable: false),
                    WinnerTeamId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixtures_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_VirtualTeams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "VirtualTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_VirtualTeams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "VirtualTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayTeamId",
                table: "Fixtures",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeTeamId",
                table: "Fixtures",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_LeagueId",
                table: "Fixtures",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures");
        }
    }
}
