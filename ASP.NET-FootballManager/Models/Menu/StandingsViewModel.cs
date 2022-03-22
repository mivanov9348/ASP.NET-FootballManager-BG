using ASP.NET_FootballManager.Data.DataModels;

namespace ASP.NET_FootballManager.Models
{
    public class StandingsViewModel
    {
        public int LeagueId { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();





    }
}
