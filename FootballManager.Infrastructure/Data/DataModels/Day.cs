namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{    
    using System.ComponentModel.DataAnnotations;
    public class Day
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public int Year { get; set; }

        [Required]
        public int CurrentDay { get; set; }
        public bool isMatchDay { get; set; }
        public bool isEuroCupDay { get; set; }
        public bool isCupDay { get; set; }
        public bool isLeagueDay { get; set; }
        public bool IsPlayed { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();

    }
}

