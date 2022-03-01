namespace ASP.NET_FootballManager.Services.League
{
    using NET_FootballManager.Data.DataModels;
    public interface ILeagueService
    {

        List<VirtualTeam> CurrentGameTeams(Game currentGame);



    }
}
