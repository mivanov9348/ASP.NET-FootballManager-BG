namespace FootballManager.Infrastructure.Data.DataModels
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Draw
    {
        public int Id { get; set; }
        public bool IsDrawStarted { get; set; }
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();

    }
}
