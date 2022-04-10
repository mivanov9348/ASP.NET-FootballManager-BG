namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
        public class EndSeasonViewModel
    {
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();
        public List<EuropeanCup> EuroCups { get; set; } = new List<EuropeanCup>();
        public Player GoalScorer { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
        public int CupId { get; set; }
        public Cup Cup { get; set; }
        public int EuroCupId { get; set; }
        public EuropeanCup EuroCup { get; set; }
        public VirtualTeam EuroCupWinner { get; set; }
        public VirtualTeam ChampionsCupWinner { get; set; }
        public VirtualTeam CupWinner { get; set; }

    }
}
