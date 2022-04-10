namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class Cup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MinLength(8)]
        public int Participants { get; set; }
        [MinLength(1)]
        public int Rounds { get; set; }
        [Required]
        public int NationId { get; set; }
        public Nation Nation { get; set; }
        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public ICollection<VirtualTeam> VirtualTeams { get; set; } = new HashSet<VirtualTeam>();
        public ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();

    }
}
