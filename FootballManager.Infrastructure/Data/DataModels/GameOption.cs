namespace FootballManager.Infrastructure.Data.DataModels
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class GameOption
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int TimeInterval { get; set; }
        public int StartingCoins { get; set; }
        public int WinCoins { get; set; }
        public int DrawCoins { get; set; }
        public int FirstPlaceCoins { get; set; }
        public int SecondPlaceCoins { get; set; }
        public int ThirdPlaceCoins { get; set; }
        public int PlayerMinimumAge { get; set; }
        public int PlayerMaximumAge { get; set; }



    }
}
