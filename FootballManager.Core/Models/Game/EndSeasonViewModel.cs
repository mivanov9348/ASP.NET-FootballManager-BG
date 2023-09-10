namespace FootballManager.Core.Models.Game
{
    using FootballManager.Infrastructure.Data.DataModels;
    public class EndSeasonViewModel
    {
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();
        public List<ContinentalCup> EuroCups { get; set; } = new List<ContinentalCup>();
        public Player GoalScorer { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
        public int CupId { get; set; }
        public Cup Cup { get; set; }
        public int EuroCupId { get; set; }
        public ContinentalCup EuroCup { get; set; }
        public VirtualTeam EuroCupWinner { get; set; }
        public VirtualTeam ChampionsCupWinner { get; set; }
        public VirtualTeam CupWinner { get; set; }

    }
}
