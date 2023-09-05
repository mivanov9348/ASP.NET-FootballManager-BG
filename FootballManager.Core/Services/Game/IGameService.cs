namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface IGameService
    {
        Game CreateNewGame(Manager manager, string userId);
        Game GetCurrentGame(string userId);
        bool isExistGame(string UserId);
        void ResetGame(Game CurrentGame);
        void ResetSave(string UserId);
    }
}
