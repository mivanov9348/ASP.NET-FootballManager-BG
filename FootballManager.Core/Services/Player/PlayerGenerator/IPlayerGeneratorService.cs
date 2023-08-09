using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

namespace FootballManager.Core.Services.Player.PlayerGenerator
{
    public interface IPlayerGeneratorService
    {
        void GeneratePlayers(Game game, VirtualTeam team);
        void FillPlayersByPosition(int count, Game game, VirtualTeam team, int positionOrder);
        void CreateFreeAgents(Game game, int gk, int df, int mf, int st);
        void FillFreeAgents(int positionOrder, VirtualTeam team);
    }
}
