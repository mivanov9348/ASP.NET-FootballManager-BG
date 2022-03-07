namespace ASP.NET_FootballManager.Services.Team
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface  ITeamService
    {
        List<VirtualTeam> GenerateTeams(Game game);

        List<VirtualTeam> CurrentGameTeams(Game currentGame);

    }
}
