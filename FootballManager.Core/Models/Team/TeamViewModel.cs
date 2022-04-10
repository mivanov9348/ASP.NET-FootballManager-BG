namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class TeamViewModel
    {

        public Team Team { get; set; }
        public VirtualTeam CurrentTeam { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<League> Leagues { get; set; } = new List<League>();


    }
}
