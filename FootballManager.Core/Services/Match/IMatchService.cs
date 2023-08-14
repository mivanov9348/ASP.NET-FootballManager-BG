namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Match;

    public interface IMatchService
    {
        Task<List<Fixture>> GetFixturesByDay(Game CurrentGame);
        Task<Fixture> GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame);
        Task<List<Player>> GetStarting11(int? teamId);
        (bool isValid, string error) ValidateTactics(VirtualTeam currentTeam);
        Match CreateMatch(Fixture currentFixture, Game CurrentGame);
        Task<Match> GetCurrentMatch(int matchId);
        void PlayerAction(VirtualTeam team, Player player, Match match);
        void Time(Match match);
        void EndMatch(Match match);
        Task<MatchViewModel> GetMatchModel(Match match, Fixture fixture, Player player);
        Task<List<Fixture>> GetResults(Game currentGame);

        void DeleteMatches(Game CurrentGame);
    }
}
