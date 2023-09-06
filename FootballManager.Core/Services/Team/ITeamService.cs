namespace ASP.NET_FootballManager.Services.Team
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Team;

    public interface ITeamService
    {
        List<VirtualTeam> GenerateTeams(Game game);
        List<VirtualTeam> CurrentGameTeams(Game currentGame);
        void CalculateTeamOverall(VirtualTeam team);
        void ResetTeams(Game currentGame);
        Task<VirtualTeam> GetCurrentTeam(Game currentGame);
        Team GetManagerTeam(Manager currentManager);
        Task<Team> GetOriginalTeam(VirtualTeam currentVirtual, Game CurrentGame);
        Task<VirtualTeam> GetTeamById(int? teamId);
        Task<List<VirtualTeam>> GetAllVirtualTeams(Game currentGame);
        Task<List<Team>> GetAllTeams();
        Task<List<Team>> GetAllPlayableTeams();
    }
}
