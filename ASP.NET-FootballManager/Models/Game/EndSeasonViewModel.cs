using ASP.NET_FootballManager.Data.DataModels;

namespace ASP.NET_FootballManager.Models
{
    public class EndSeasonViewModel
    {
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();
        public Player GoalScorer { get; set; }
        public League League { get; set; }

        public int LeagueId { get; set; }

    }
}
