namespace FootballManager.Infrastructure.Data.DataModels
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    public class Fixture
    {
        public int Id { get; set; }
        public int? GameId { get; set; }
        public int Round { get; set; }
        public string CompetitionName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int? LeagueId { get; set; }
        public League League { get; set; }
        public int? DayId { get; set; }
        public Day Day { get; set; }
        public int? CupId { get; set; }
        public Cup Cup { get; set; }        
        public int? EuropeanCupId { get; set; }
        public EuropeanCup EuropeanCup { get; set; }
        public int? DrawId { get; set; }
        public Draw Draw { get; set; }
        public virtual VirtualTeam HomeTeam { get; set; }
        public virtual VirtualTeam AwayTeam { get; set; }
        public int HomeTeamGoal { get; set; }
        public int AwayTeamGoal { get; set; }
        public bool IsPlayed { get; set; }
        public int? WinnerTeamId { get; set; }     
        public ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}
