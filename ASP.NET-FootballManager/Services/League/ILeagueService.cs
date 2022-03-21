namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Models;
    using NET_FootballManager.Data.DataModels;
    public interface ILeagueService
    {
     
        League GetLeague(int id);
        List<League> GetAllLeagues();    
        List<VirtualTeam> GetStandingsByLeague(int id);
        void CalculateOtherMatches(List<Fixture> fixtures, Fixture fixture);
        void CheckWinner(int homeGoals, int awayGoals, Fixture currentFixt);
        void PromotedRelegated(Game CurrentGame);

       
   
     
    }
}
