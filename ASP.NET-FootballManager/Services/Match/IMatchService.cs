namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;

    public interface IMatchService
    {

        List<Fixture> GetFixturesByDay(Game CurrentGame);
        Fixture GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame);
        List<Player> GetStarting11(int teamId);
        (bool isValid, string error) ValidateTactics(VirtualTeam currentTeam);
        Match CreateMatch(Fixture currentFixture, Game CurrentGame);
        Match GetCurrentMatch(int matchId);
        void PlayerAction(VirtualTeam team, Player player, Match match);
        void Time(Match match);
        void EndMatch(Match match);
        MatchViewModel GetMatchModel(Match match, Fixture fixture, Player player);
        List<Fixture> GetResults(Game currentGame);
    }
}
