namespace FootballManager.Core.Models.GameOptions
{
    using ASP.NET_FootballManager.Data.Constant;

    public class GameOptionsViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TimeInterval { get; set; } = DataConstants.Match.Timespan;
        public int StartingCoins { get; set; } = DataConstants.Prize.StartingCoins;
        public int WinCoins { get; set; } = DataConstants.Prize.WinCoins;
        public int DrawCoins { get; set; } = DataConstants.Prize.DrawCoins;
        public int FirstPlaceCoins { get; set; } = DataConstants.Prize.FirstPlaceCoins;
        public int SecondPlaceCoins { get; set; } = DataConstants.Prize.SecondPlaceCoins;
        public int ThirdPlaceCoins { get; set; } = DataConstants.Prize.ThirdPlaceCoins;
        public int PlayerMinimumAge { get; set; } = DataConstants.Age.minAge;
        public int PlayerMaximumAge { get; set; } = DataConstants.Age.maxAge;
    }
}
