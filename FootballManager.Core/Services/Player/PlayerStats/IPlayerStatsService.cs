namespace FootballManager.Core.Services.Player.PlayerStats
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface IPlayerStatsService
    {
        (string firstName, string lastName) getPlayerNames(VirtualTeam team);
        void GetProfileImage(Player player);
        (City city, int age, Nation nation) getPlayerInfo(VirtualTeam team);
        void ResetPlayerStats(Game CurrentGame);
        void Substitution(int playerId, string action);
        void CalculatingPlayersPrice(Game CurrentGame);
    }
}
