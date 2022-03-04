namespace ASP.NET_FootballManager.Data.DataModels
{
    public class Fixture
    {

        public int Id { get; set; }
        public int GameId { get; set; }

        public int Round { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int? LeagueId { get; set; }

        public League League { get; set; }

        public virtual VirtualTeam HomeTeam { get; set; }

        public virtual VirtualTeam AwayTeam { get; set; }

        public int HomeTeamGoal { get; set; }

        public int AwayTeamGoal { get; set; }

        public bool IsPlayed { get; set; }

        public int WinnerTeamId { get; set; }

        public int Day { get; set; }

        public int Year { get; set; }


    }
}
