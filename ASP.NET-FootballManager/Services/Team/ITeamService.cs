namespace ASP.NET_FootballManager.Services.Team
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;

    public interface ITeamService
    {
        List<VirtualTeam> GenerateTeams(Game game);
        List<VirtualTeam> CurrentGameTeams(Game currentGame);
        void CalculateTeamOverall(VirtualTeam team);
        TeamViewModel GetTeamViewModel(List<Player> currPlayers, VirtualTeam currentTeam);

    }
}
