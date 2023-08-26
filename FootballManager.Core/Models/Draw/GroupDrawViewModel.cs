namespace FootballManager.Core.Models.Draw
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class GroupDrawViewModel
    {
        public int DrawId { get; set; }
        public int? NumberOfGroups { get; set; }
        public int? TeamsPerGroup { get; set; }
        public int TeamsCount { get; set; }
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>() { };
        public List<League> Leagues { get; set; } = new List<League>() { };
        public List<VirtualTeam> RemainingTeams { get; set; } = new List<VirtualTeam>() { };
        public bool IsDrawStarted { get; set; } = false;
    }
}
