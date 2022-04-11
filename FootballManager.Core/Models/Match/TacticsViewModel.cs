namespace FootballManager.Core.Models.Match
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class TacticsViewModel
    {
        public int PlayerId { get; set; }
        public VirtualTeam CurrentTeam { get; set; }
        public List<Player> StartingEleven { get; set; } = new List<Player>();
        public List<Player> Substitutes { get; set; } = new List<Player>();
        public List<Position> Positions { get; set; } = new List<Position>();

    }
}
