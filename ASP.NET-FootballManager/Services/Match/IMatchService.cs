namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IMatchService
    {

        List<Fixture> GetFixturesByDay(Game CurrentGame);
        Fixture GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame);
        List<Player> GetUserPlayers(VirtualTeam managerTeam);
        List<Player> GetPcTeamPlayers(VirtualTeam managerTeam, Fixture currentFixture);
        List<Player> GetStarting11(int teamId);

    }
}
