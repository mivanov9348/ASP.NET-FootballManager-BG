namespace FootballManager.Core.Models.Menu
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class StandingsViewModel
    {
        public int LeagueId { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public List<League> Leagues { get; set; } = new List<League>();





    }
}
