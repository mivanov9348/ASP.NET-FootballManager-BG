namespace FootballManager.Infrastructure.Data.DataModels
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
        public double Overall { get; set; }
        public double Price { get; set; }
        public bool IsStarting11 { get; set; }
        public string ProfileImage { get; set; }
        public bool FreeAgent { get; set; }
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
        public int PlayerAttributesId { get; set; }
        public PlayerAttribute PlayerAttributes { get; set; }
        public int PlayerStatsId { get; set; }
        public PlayerStats PlayerStats { get; set; }
    }
}
