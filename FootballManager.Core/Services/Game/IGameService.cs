namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IGameService
    {
        Game CreateNewGame(Manager manager);
        Game GetCurrentGame(int id);
        bool isExistGame(string UserId);
        void NextDay(Game currentGame);
        void ResetGame(Game CurrentGame);

        void ResetSave(string UserId);
    }
}
