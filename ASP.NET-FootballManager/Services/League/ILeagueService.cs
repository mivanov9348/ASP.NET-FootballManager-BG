namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Models;
    using NET_FootballManager.Data.DataModels;
    public interface ILeagueService
    {

        void GenerateFixtures(Game game);

        LeagueViewModel GetLeague(int id);

        List<League> GetAllLeagues();

        void Shuffle(List<VirtualTeam> currl);

        List<VirtualTeam> GetStandingsByLeague(int id);


    }
}
