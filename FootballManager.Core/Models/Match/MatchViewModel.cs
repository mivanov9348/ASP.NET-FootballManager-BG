namespace FootballManager.Core.Models.Match
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class MatchViewModel
    {
        public string CurrentPlayerName { get; set; }
        public string SituationText  { get; set; }
        public Fixture CurrentFixture { get; set; }
        public Match CurrentMatch { get; set; }
        public VirtualTeam HomeTeam { get; set; }
        public VirtualTeam AwayTeam { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public List<Player> HomeTeamPlayers { get; set; } = new List<Player>();
        public List<Player> AwayTeamPlayers { get; set; } = new List<Player>();
        public List<Position> Positions { get; set; } = new List<Position>();


    }
}
