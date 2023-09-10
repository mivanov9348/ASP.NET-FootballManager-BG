using FootballManager.Infrastructure.Data.DataModels;

namespace FootballManager.Core.Models.Draw
{
    public class DrawViewModel
    {
        public VirtualTeam currentDrawedTeam { get; set; }
        public bool IsDrawStarted { get; set; } = false;
        public int NumberOfTeams { get; set; }
        public int CurrentDrawId { get; set; }
        public bool IsChampionsCupDraw { get; set; } 
        public bool IsEuropeanCupDraw { get; set; } 
        public bool IsCupDraw { get; set; } 
        public List<VirtualTeam> RemainingTeams { get; set; } = new List<VirtualTeam>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Fixture> AllFixtures { get; set; } = new List<Fixture>();
        public Fixture currentFixture { get; set; }
    }
}
