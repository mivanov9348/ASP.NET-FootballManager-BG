using ASP.NET_FootballManager.Data.DataModels;

namespace ASP.NET_FootballManager.Data.Constant
{
    public class DataConstants
    {

        public class Match
        {
            public const int Timespan = 20;

        }

        public class Prize
        {
            public const int WinCoins = 50;
            public const int DrawCoins = 20;
            public const int FirstPlaceCoins = 300;
            public const int SecondPlaceCoins = 200;
            public const int ThirdPlaceCoins = 100;
            public const int PromotionCoins = 200;
        }

        public class FreeAgents
        {
            public const int gk = 20;
            public const int df = 40;
            public const int mf = 40;
            public const int st = 50;
        }




    }
}
