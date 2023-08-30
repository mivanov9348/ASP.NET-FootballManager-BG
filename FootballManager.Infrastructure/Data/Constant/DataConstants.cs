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

        public class InboxMessages
        {
            public (string messageReview, string fullMessage) MatchNews(string homeTeamName, string awayTeamName, int homeTeamGoal, int awayTeamGoal, int currentRound)
            {
                var messageReview = "";
                var fullMessage = "";

                if (homeTeamGoal > awayTeamGoal)
                {
                    messageReview = $"{homeTeamName} - {awayTeamName} {homeTeamGoal}:{awayTeamGoal} ";

                    fullMessage = $"{homeTeamName} wins over {awayTeamName} with {homeTeamGoal}:{awayTeamGoal} in round {currentRound}.";
                }
                if (homeTeamGoal < awayTeamGoal)
                {
                    messageReview = $"{homeTeamName} - {awayTeamName} {homeTeamGoal}:{awayTeamGoal} ";

                    fullMessage = $"{awayTeamName} wins over {homeTeamName} with {awayTeamGoal}:{homeTeamGoal} in round {currentRound}.";
                }
                if (homeTeamGoal == awayTeamGoal)
                {
                    messageReview = $"{homeTeamName} - {awayTeamName} {homeTeamGoal}:{awayTeamGoal}";

                    fullMessage = $"{homeTeamName} finished draw with {awayTeamName} in round {currentRound}.";
                }

                return (messageReview, fullMessage);
            }
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
            public const int DaysInMonth = 30;
            public const int ThirtyOneDaysInMonth = 31;

        }

    }
}
