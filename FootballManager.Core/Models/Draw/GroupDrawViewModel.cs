namespace FootballManager.Core.Models.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    public class GroupDrawViewModel
    {
        public int DrawId { get; set; }
        public int? NumberOfGroups { get; set; }
        public int? TeamsPerGroup { get; set; }
        public int TeamsCount { get; set; }
        public string DrawedTeamName { get; set; }
        public string DrawedGroupName { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>() { };
        public List<Nation> Nations { get; set; } = new List<Nation>() { };
        public List<League> Leagues { get; set; } = new List<League>() { };
        public List<VirtualTeam> RemainingTeams { get; set; } = new List<VirtualTeam>() { };
        public bool IsDrawStarted { get; set; } = false;
    }
}
