namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class MatchViewModel
    {

        public Fixture CurrentFixture { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public List<Player> HomeTeamPlayers { get; set; } = new List<Player>();
        public List<Player> AwayTeamPlayers { get; set; } = new List<Player>();


    }
}
