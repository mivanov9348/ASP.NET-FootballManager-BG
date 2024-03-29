namespace FootballManager.Infrastructure.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContinentalCup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MinLength(8)]
        public int Participants { get; set; }
        [MinLength(1)]
        public int Rounds { get; set; }
        public int Rank { get; set; }
        public int Year { get; set; }      
        public Game Game { get; set; }
        public int GameId { get; set; }
        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public ICollection<VirtualTeam> VirtualTeams { get; set; } = new HashSet<VirtualTeam>();
        public ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();
        public ICollection<Draw> Draws { get; set; } = new HashSet<Draw>();

    }
}
