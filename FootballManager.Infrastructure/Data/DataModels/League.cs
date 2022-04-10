namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Rounds { get; set; }
        public int NationId { get; set; }
        public Nation Nation { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public virtual ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();

    }
}
