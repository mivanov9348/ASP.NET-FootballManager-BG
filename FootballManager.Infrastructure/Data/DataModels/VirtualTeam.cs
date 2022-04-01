namespace ASP.NET_FootballManager.Data.DataModels
{   
    public class VirtualTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int? LeagueId { get; set; }
        public League League { get; set; }
        public int? CupId { get; set; }
        public Cup Cup { get; set; }
        public int? EuropeanCupId { get; set; }
        public EuropeanCup EuropeanCup { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalScored { get; set; }
        public int GoalAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public int Titles { get; set; }
        public int Cups { get; set; }
        public int ChampionsCup { get; set; }
        public int EuroCups { get; set; }
        public int Budget { get; set; }
        public int Overall { get; set; }
        public bool IsEuroParticipant { get; set; }
        public bool IsCupParticipant { get; set; }
        public bool IsPlayable { get; set; }
        public ICollection<Player> Players { get; set; } = new HashSet<Player>();
        public virtual ICollection<Fixture> HomeMatches { get; set; } = new HashSet<Fixture>();
        public virtual ICollection<Fixture> AwayMatches { get; set; } = new HashSet<Fixture>();
    }
}
