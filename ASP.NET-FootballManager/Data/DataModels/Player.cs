namespace ASP.NET_FootballManager.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class Player
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(16, 35)]
        public int Age { get; set; }
        public int Matches { get; set; }
        public int Goals { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Overall { get; set; }
        public int Saves { get; set; }
        public int Price { get; set; }
        public bool IsStarting11 { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int NationId { get; set; }
        public Nation Nation { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int TeamId { get; set; }
        public VirtualTeam Team { get; set; }
        public int? LeagueId { get; set; }
        public League League { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}
