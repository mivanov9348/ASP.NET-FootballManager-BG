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
            public const int StartingCoins = 10;
            public const int WinCoins = 3;
            public const int DrawCoins = 1;
            public const int FirstPlaceCoins = 30;
            public const int SecondPlaceCoins = 20;
            public const int ThirdPlaceCoins = 10;
            public const int PromotionCoins = 20;
        }
        public class StartingPlayersCount
        {
            public const int gk = 1;
            public const int df = 4;
            public const int mf = 4;
            public const int st = 2;
        }
        public class FreeAgentsEachClub
        {
            public const int gk = 20;
            public const int df = 40;
            public const int mf = 40;
            public const int st = 50;
        }

        public class Age
        {
            public const int minAge = 16;
            public const int maxAge = 35;
        }
        public class PositionOrder
        {
            public const int Goalkeeper = 1;
            public const int Defender = 2;
            public const int Midlefielder = 3;
            public const int Forward = 4;
        }
        public class LeagueLevels
        {
            public const int FirstLevel = 1;
            public const int SecondLevel = 2;
            public const int ThirdLevel = 3;
            public const int FourthLevel = 4;
        }
        public class YearStats
        {
            public const int MonthsCount = 12;
            public const int DaysInWeek = 7;
            public const int JanuaryDays = 31;
            public const int FebruaryDays = 28;
            public const int MarchDays = 31;
            public const int AprilDays = 30;
            public const int MayDays = 31;
            public const int JuneDays = 30;
            public const int JulyDays = 31;
            public const int AugustDays = 31;
            public const int SeptemberDays = 30;
            public const int OctoberDays = 31;
            public const int NovemberDays = 30;
            public const int DecemberDays = 31;
        }
        public class ChampionsCup
        {
            public const string Name = "Champions Cup";
            public const int Rank = 1;
            public const int Participants = 32;
            public const int Rounds = 5;
        }
        public class EuroCup
        {
            public const string Name = "Euro Cup";
            public const int Rank = 2;
            public const int Participants = 32;
            public const int Rounds = 5;
        }
    }
}
