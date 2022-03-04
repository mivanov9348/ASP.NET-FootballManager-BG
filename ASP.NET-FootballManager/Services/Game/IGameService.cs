namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IGameService
    {
        List<VirtualTeam> CurrentGameTeams(Game currentGame);
        void StartNewGame(Manager currentManager);
        Game CreateNewGame(Manager manager);    
        Game GetCurrentGame(int id);
        bool isExistGame(string UserId);

    }
}
