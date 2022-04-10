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

        public class StartingPlayersCount
        {
            public const int gk = 1;
            public const int df = 4;
            public const int mf = 4;
            public const int st = 2;
        }

        public class FreeAgents
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




    }
}
