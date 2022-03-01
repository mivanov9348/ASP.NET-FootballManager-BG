namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IGameService
    {
        void StartNewGame(Manager currentManager);
        Game CreateNewGame(Manager manager);
        void GeneratePlayers(Game game,VirtualTeam team);
        void GenerateFixtures();        
        Game GetCurrentGame(int id);
        bool isExistGame(string UserId);

    }
}
