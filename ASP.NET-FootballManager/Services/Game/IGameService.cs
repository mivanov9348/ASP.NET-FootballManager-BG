namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IGameService
    {

        void CreateNewGame(Manager manager);

        void GenerateNames();


    }
}
