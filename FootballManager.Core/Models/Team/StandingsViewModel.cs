namespace FootballManager.Core.Models.Team
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;

    public class StandingsViewModel
    {
        public int LeagueId { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();
        public MenuViewModel MenuViewModel { get; set; }




    }
}
