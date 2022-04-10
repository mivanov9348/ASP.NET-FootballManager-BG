namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface ILeagueService
    {

        Task<League> GetLeague(int id);
        Task<List<League>> GetAllLeagues();
        Task<List<VirtualTeam>> GetStandingsByLeague(int id, Game CurrentGame);
        void CalculateOtherMatches(List<Fixture> fixtures, Fixture fixture);
        void CheckWinner(int homeGoals, int awayGoals, Fixture currentFixt);
        Task PromotedRelegated(Game CurrentGame);


    }
}
