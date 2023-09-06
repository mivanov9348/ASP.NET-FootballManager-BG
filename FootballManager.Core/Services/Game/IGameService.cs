namespace ASP.NET_FootballManager.Services.Game
{
    using FootballManager.Infrastructure.Data.DataModels;
    public interface IGameService
    {
        (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo(string userId);

        Game CreateNewGame(Manager manager, string userId);
        Game GetCurrentGame(string userId);
        bool isExistGame(string UserId);
        void ResetGame(Game CurrentGame);
        void ResetSave(string UserId);
    }
}
