namespace FootballManager.Infrastructure.Data.DataModels
{    
    using System.Collections.Generic;    

    public class Draw
    {
        public int Id { get; set; }
        public int? NumOfGroups { get; set; }
        public int? TeamsPergroup { get; set; }
        public bool IsDrawStarted { get; set; }
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();
        public List<League> Leagues { get; set; } = new List<League>();

    }
}
